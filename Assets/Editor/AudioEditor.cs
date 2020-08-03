using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
public class AudioEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AudioManager sound = (AudioManager)target;

        for (int i = 0; i < sound.sounds.Length; i++)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Play " + sound.sounds[i].name))
            {
                sound.Play(sound.sounds[i].name);
            }
            if (GUILayout.Button("Stop " + sound.sounds[i].name))
            {
                sound.Stop(sound.sounds[i].name);
            }
            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Stop All"))
        {
            sound.StopAll();
        }
    }
}
