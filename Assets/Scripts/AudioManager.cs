using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Runtime.InteropServices;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            if (s.source == null)
            {
                AddSource(s);
            }
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    /// <summary>
    /// Plays the sound with the coresponding name
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }
        if (s.source != null)
            s.source.Play();
        else
        {
            AddSource(s);

            s.source.Play();
        }
    }

    /// <summary>
    /// Stops the sound with the coresponding name
    /// </summary>
    /// <param name="name"></param>
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }
        if (s.source != null)
            s.source.Stop();
        else
        {
            AddSource(s);

            s.source.Stop();
        }
    }

    public void StopAll()
    {
        foreach (Sound sound in sounds)
        {
            Stop(sound.name);
        }
    }

    void AddSource(Sound s)
    {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
    }

    void UpdateSources()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void OnValidate()
    {
        UpdateSources();
    }
}
