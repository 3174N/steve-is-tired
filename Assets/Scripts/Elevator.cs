using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            FindObjectOfType<GameManager>().level++;
            FindObjectOfType<GameManager>().Save();
            FindObjectOfType<LevelLoader>().LoadNextLevel();
        }
    }
}
