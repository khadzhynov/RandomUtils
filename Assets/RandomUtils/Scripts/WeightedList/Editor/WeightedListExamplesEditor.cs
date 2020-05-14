using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GG.Infrastructure.Utils;


/*
//[CustomEditor(typeof(WeightedListExamples))]
public class WeightedListExamplesEditor : Editor
{
    private WeightedListExamples _target;

    private void OnEnable()
    {
        _target = target as WeightedListExamples;
    }

    /*
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WeightedListEditorUtils.DrawListOfInts(_target.WeightedListOfInts);
        WeightedListEditorUtils.DrawListOfStrings(_target.WeightedListOfStrings);
        WeightedListEditorUtils.DrawListOfVector3(_target.WeightedListOfVector3);
        WeightedListEditorUtils.DrawListOfGameObjects(_target.WeightedListOfGameObjects);
        /*
        DrawListOfInts();
        DrawListOfStrings();
        DrawListOfVector3();
        DrawListOfGameObjects();
        */
        /*
        EditorUtility.SetDirty(_target);
    }
*/
/*
    private void DrawListOfInts()
    {
        if (_target.WeightedListOfInts == null)
        {
            _target.WeightedListOfInts = new GG.Infrastructure.Utils.WeightedList<int>();
        }

        GUILayout.Label("Weighted List of Int");
        int indexToRemove = -1;
        for (int i = 0; i < _target.WeightedListOfInts.Count; ++i)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("item:");
            _target.WeightedListOfInts[i] = EditorGUILayout.IntField(_target.WeightedListOfInts[i]);
            GUILayout.Label("weight:");
            _target.WeightedListOfInts.SetWeightAtIndex(i, EditorGUILayout.FloatField(_target.WeightedListOfInts.GetWeightAtIndex(i)));

            if (GUILayout.Button("x"))
            {
                indexToRemove = i;
            }

            EditorGUILayout.EndHorizontal();
        }

        if (indexToRemove != -1)
        {
            _target.WeightedListOfInts.RemoveAt(indexToRemove);
        }

        if (GUILayout.Button("Add"))
        {
            _target.WeightedListOfInts.Add(0, 0f);
        }
    }

    private void DrawListOfStrings()
    {
        if (_target.WeightedListOfStrings == null)
        {
            _target.WeightedListOfStrings = new GG.Infrastructure.Utils.WeightedList<string>();
        }

        GUILayout.Label("Weighted List of Strings");
        int indexToRemove = -1;
        for (int i = 0; i < _target.WeightedListOfStrings.Count; ++i)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("item:");
            _target.WeightedListOfStrings[i] = EditorGUILayout.TextField (_target.WeightedListOfStrings[i]);
            GUILayout.Label("weight:");
            _target.WeightedListOfStrings.SetWeightAtIndex(i, EditorGUILayout.FloatField(_target.WeightedListOfStrings.GetWeightAtIndex(i)));

            if (GUILayout.Button("x"))
            {
                indexToRemove = i;
            }

            EditorGUILayout.EndHorizontal();
        }

        if (indexToRemove != -1)
        {
            _target.WeightedListOfStrings.RemoveAt(indexToRemove);
        }

        if (GUILayout.Button("Add"))
        {
            _target.WeightedListOfStrings.Add(string.Empty, 0f);
        }
    }

    private void DrawListOfVector3()
    {
        if (_target.WeightedListOfVector3 == null)
        {
            _target.WeightedListOfVector3 = new GG.Infrastructure.Utils.WeightedList<Vector3>();
        }

        GUILayout.Label("Weighted List of Vector3");
        int indexToRemove = -1;
        for (int i = 0; i < _target.WeightedListOfVector3.Count; ++i)
        {
            _target.WeightedListOfVector3[i] = EditorGUILayout.Vector3Field("item:", _target.WeightedListOfVector3[i]);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("weight:");
            _target.WeightedListOfVector3.SetWeightAtIndex(i, EditorGUILayout.FloatField(_target.WeightedListOfVector3.GetWeightAtIndex(i)));

            if (GUILayout.Button("x"))
            {
                indexToRemove = i;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        if (indexToRemove != -1)
        {
            _target.WeightedListOfVector3.RemoveAt(indexToRemove);
        }

        if (GUILayout.Button("Add"))
        {
            _target.WeightedListOfVector3.Add(Vector3.zero, 0f);
        }
    }

    private void DrawListOfGameObjects()
    {
        if (_target.WeightedListOfGameObjects == null)
        {
            _target.WeightedListOfGameObjects = new WeightedList<GameObject>();
        }

        GUILayout.Label("Weighted List of GameObjects");
        int indexToRemove = -1;

        for (int i = 0; i < _target.WeightedListOfGameObjects.Count; ++i)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("item:");
            _target.WeightedListOfGameObjects[i] = EditorGUILayout.ObjectField(_target.WeightedListOfGameObjects[i], typeof(GameObject), true) as GameObject;
            GUILayout.Label("weight:");
            _target.WeightedListOfGameObjects.SetWeightAtIndex(i, EditorGUILayout.FloatField(_target.WeightedListOfGameObjects.GetWeightAtIndex(i)));

            if (GUILayout.Button("x"))
            {
                indexToRemove = i;
            }

            EditorGUILayout.EndHorizontal();
        }

        if (indexToRemove != -1)
        {
            _target.WeightedListOfGameObjects.RemoveAt(indexToRemove);
        }

        if (GUILayout.Button("Add"))
        {
            _target.WeightedListOfGameObjects.Add(null, 0f);
        }
    }
    *//*
}*/

