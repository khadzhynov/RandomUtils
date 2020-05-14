using UnityEngine;
using GG.Infrastructure.Utils;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(WeightedListOfInts))]
[CustomPropertyDrawer(typeof(WeightedListOfStrings))]
[CustomPropertyDrawer(typeof(WeightedListOfVector3))]
[CustomPropertyDrawer(typeof(WeightedListOfGameObjects))]
[CustomPropertyDrawer(typeof(WeightedListOfScriptableObjects))]
[CustomPropertyDrawer(typeof(WeightedListOfMonoBehaviours))]
public class WeightedListPropertyDrawer : PropertyDrawer
{
    private const float space = 5;

    private float height;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return height;
    }

    public override void OnGUI(Rect rect,
                               SerializedProperty property,
                               GUIContent label)
    {
        height = 0;

        var objectsProperty = property.FindPropertyRelative("_objects");

        Rect foldoutRect = new Rect(
            new Vector2(rect.position.x, rect.position.y),
            new Vector2(rect.width, EditorGUIUtility.singleLineHeight));

        objectsProperty.isExpanded = EditorGUI.Foldout(foldoutRect, objectsProperty.isExpanded, property.displayName);

        height += EditorGUIUtility.singleLineHeight;

        if (objectsProperty.isExpanded)
        {
            Rect listRect = new Rect(
                    new Vector2(rect.position.x, rect.position.y + EditorGUIUtility.singleLineHeight + space),
                    new Vector2(rect.width, rect.height));

            height += DrawProperty(listRect, objectsProperty, property.FindPropertyRelative("_weights"));


            height += EditorGUIUtility.singleLineHeight;
            
            if (GUI.Button(
                    new Rect(
                        new Vector2(rect.position.x + space, rect.position.y + height),
                        new Vector2(rect.width / 2 - space / 2, EditorGUIUtility.singleLineHeight)),
                    "Add"))
            {
                int index = objectsProperty.arraySize > 0 ? objectsProperty.arraySize - 1 : 0;
                objectsProperty.InsertArrayElementAtIndex(index);
                property.FindPropertyRelative("_weights").InsertArrayElementAtIndex(index);
            }
            
            if (GUI.Button(
                    new Rect(
                        new Vector2(rect.position.x + space * 1.5f + rect.width / 2, rect.position.y + height),
                        new Vector2(rect.width / 2 - space * 1.5f, EditorGUIUtility.singleLineHeight)),
                    "Normalize"))
            {
                var weightsProperty = property.FindPropertyRelative("_weights");

                List<float> normalizedWeights = new List<float>();

                float totalWeight = 0;

                for (int i = 0; i < weightsProperty.arraySize; ++i)
                {
                    totalWeight += weightsProperty.GetArrayElementAtIndex(i).floatValue;
                }

                for (int i = 0; i < weightsProperty.arraySize; ++i)
                {
                    float currentWeight = weightsProperty.GetArrayElementAtIndex(i).floatValue / totalWeight;
                    normalizedWeights.Add(currentWeight);
                }

                for (int i = 0; i < weightsProperty.arraySize; ++i)
                {
                    weightsProperty.GetArrayElementAtIndex(i).floatValue = normalizedWeights[i];
                }
            }
        }
        height += EditorGUIUtility.singleLineHeight;

        property.serializedObject.ApplyModifiedProperties();
    }
   

    private float DrawProperty(Rect rect, SerializedProperty propertyObjects, SerializedProperty propertyWeights)
    {
        float totalHeight = 0;

        SerializedProperty currentItem;
        SerializedProperty currentWeight;

        int indexToRemove = -1;

        float currentPositionY = rect.position.y;

        for (int i = 0; i < propertyObjects.arraySize; ++i)
        {
            currentItem = propertyObjects.GetArrayElementAtIndex(i);
            currentWeight = propertyWeights.GetArrayElementAtIndex(i);

            float itemHeight = EditorGUI.GetPropertyHeight(currentItem);

            float removeButtonWidth = rect.width / 7 - space;
            float sectionWidth = (rect.width - removeButtonWidth - space) / 2 - space;
            float labelWidth = sectionWidth / 3 - space;
            float fieldWidth = sectionWidth - labelWidth;

            float itemsLabelPositionX = rect.position.x;
            float itemsFieldPositionX = itemsLabelPositionX + labelWidth + space;

            float weightsLabelPositionX = itemsFieldPositionX + fieldWidth + space;
            float weightsFieldPositionX = weightsLabelPositionX + labelWidth + space;

            float removeButtonPositionX = weightsFieldPositionX + fieldWidth + space;

            removeButtonWidth -= space;

            Rect itemsLabelRect = new Rect(
                new Vector2(itemsLabelPositionX, currentPositionY), 
                new Vector2(labelWidth, itemHeight));

            Rect itemsFieldRect = new Rect(
                new Vector2(itemsFieldPositionX, currentPositionY),
                new Vector2(fieldWidth, itemHeight));

            Rect weightsLabelRect = new Rect(
                new Vector2(weightsLabelPositionX, currentPositionY),
                new Vector2(labelWidth, itemHeight));

            Rect weightsFieldRect = new Rect(
                new Vector2(weightsFieldPositionX, currentPositionY),
                new Vector2(fieldWidth, itemHeight));

            Rect removeButtonRect = new Rect(
                new Vector2(removeButtonPositionX, currentPositionY),
                new Vector2(removeButtonWidth, EditorGUIUtility.singleLineHeight));

            GUI.Label(itemsLabelRect, new GUIContent("Item"));
            EditorGUI.PropertyField(itemsFieldRect, currentItem, GUIContent.none, true);
            GUI.Label(weightsLabelRect, new GUIContent("Weight"));
            EditorGUI.PropertyField(weightsFieldRect, currentWeight, GUIContent.none, true);

            if(GUI.Button(removeButtonRect, "X"))
            {
                indexToRemove = i;
            }

            totalHeight += itemHeight;
            currentPositionY += itemHeight;

            currentItem.serializedObject.ApplyModifiedProperties();
        }

        if (indexToRemove != -1)
        {
            propertyObjects.DeleteArrayElementAtIndex(indexToRemove);
            propertyWeights.DeleteArrayElementAtIndex(indexToRemove);
        }

        return totalHeight;
    }
}
#endif