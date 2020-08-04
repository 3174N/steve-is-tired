using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    #region variables
    public float transitionTime = 1;

    Animator transition;
    #endregion

    private void Awake()
    {
        transition = gameObject.GetComponent<Animator>();
    }

    public void Load(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    public void Load(string levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadStart()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadCurrentLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
    }

    IEnumerator LoadLevel(string levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
    }
}
