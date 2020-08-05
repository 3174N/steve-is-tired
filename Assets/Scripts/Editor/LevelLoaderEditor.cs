using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelLoader))]
public class LevelLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelLoader loader = (LevelLoader)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Next Level"))
        {
            loader.LoadNextLevel();
        }
        GUILayout.EndHorizontal();
    }
}
