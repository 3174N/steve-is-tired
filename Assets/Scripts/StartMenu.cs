using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void Launch()
    {
        FindObjectOfType<GameManager>().Load();
        FindObjectOfType<LevelLoader>().LoadStart();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
