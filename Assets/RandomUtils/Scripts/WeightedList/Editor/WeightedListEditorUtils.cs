using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GG.Infrastructure.Utils;

public class WeightedListEditorUtils
{
    public static WeightedList<int> DrawListOfInts(WeightedList<int> list)
    {
        list = CreateIfNull(list);
        GUILayout.Label("Weighted List of Int");

        int indexToRemove = -1;
        for (int i = 0; i < list.Count; ++i)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("item:");
            list[i] = EditorGUILayout.IntField(list[i]);
            WeightField(list, i);

            if (GUILayout.Button("x"))
            {
                indexToRemove = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        list = ProcessButtons(list, indexToRemove);
        return list;
    }

    public static WeightedList<string> DrawListOfStrings(WeightedList<string> list)
    {
        list = CreateIfNull(list);
        GUILayout.Label("Weighted List of Strings");

        int indexToRemove = -1;
        for (int i = 0; i < list.Count; ++i)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("item:");
            list[i] = EditorGUILayout.TextField(list[i]);
            WeightField(list, i);

            if (GUILayout.Button("x"))
            {
                indexToRemove = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        list = ProcessButtons(list, indexToRemove);
        return list;
    }

    public static WeightedList<Vector3> DrawListOfVector3(WeightedList<Vector3> list)
    {
        list = CreateIfNull(list);
        GUILayout.Label("Weighted List of Strings");

        int indexToRemove = -1;
        for (int i = 0; i < list.Count; ++i)
        {
            list[i] = EditorGUILayout.Vector3Field("item:", list[i]);
            EditorGUILayout.BeginHorizontal();

            WeightField(list, i);

            if (GUILayout.Button("x"))
            {
                indexToRemove = i;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        list = ProcessButtons(list, indexToRemove);
        return list;
    }

    public static WeightedList<GameObject> DrawListOfGameObjects(WeightedList<GameObject> list)
    {
        list = CreateIfNull(list);
        GUILayout.Label("Weighted List of GameObjects");

        int indexToRemove = -1;
        for (int i = 0; i < list.Count; ++i)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("item:");
            list[i] = EditorGUILayout.ObjectField(list[i], typeof(GameObject), true) as GameObject;
            WeightField(list, i);

            if (GUILayout.Button("x"))
            {
                indexToRemove = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        list = ProcessButtons(list, indexToRemove);
        return list;
    }

    private static void WeightField<T>(WeightedList<T> list, int index)
    {
        GUILayout.Label("weight:");
        list.SetWeightAtIndex(index, EditorGUILayout.FloatField(list.GetWeightAtIndex(index)));
    }

    private static WeightedList<T> CreateIfNull<T>(WeightedList<T> list)
    {
        if (list == null)
        {
            list = new WeightedList<T>();
        }
        return list;
    }

    private static WeightedList<T> ProcessButtons<T>(WeightedList<T> list, int indexToRemove)
    {
        if (indexToRemove != -1)
        {
            list.RemoveAt(indexToRemove);
        }

        if (GUILayout.Button("Add"))
        {
            Debug.Log("Add " + default(T));
            list.Add(default(T), 0f);
        }
        return list;
    }

}
