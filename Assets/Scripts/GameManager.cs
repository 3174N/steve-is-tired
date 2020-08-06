using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int level = 1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    /// <summary>
    /// Saves the level
    /// </summary>
    public void Save()
    {
        SaveSystem.Save(this);
    }

    /// <summary>
    /// Loads save
    /// </summary>
    public void Load()
    {
        Data data = SaveSystem.Load();

        level = data.level;
    }
}
