using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void Launch()
    {
        FindObjectOfType<LevelLoader>().LoadStart();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
