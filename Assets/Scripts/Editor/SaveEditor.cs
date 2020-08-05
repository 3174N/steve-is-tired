using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class SaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager manager = (GameManager)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            manager.Save();
        }
        if (GUILayout.Button("Load"))
        {
            manager.Load();
        }
        GUILayout.EndHorizontal();
    }
}
