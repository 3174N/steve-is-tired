using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachine : MonoBehaviour
{
    #region variables
    public bool startRewind;
    public bool infiniteDrinks;

    public GameObject energyDrink;

    bool hasPressed = false;
    bool playerIsIn;

    public bool isActivatingTextBox;
    public GameObject textBoxToActivate;
    public bool isActivatingTextOnCan;
    public GameObject textBoxToActivateOnCan;

    public bool isSpawningCans;
    public Vector2[] spawnPoints;

    public GameObject rewindBar;
    #endregion

    private void Update()
    {
        if (playerIsIn)
        {
            if (Input.GetKeyDown(FindObjectOfType<PlayerController>().interactKey))
            {
                GameObject can = Instantiate(energyDrink,
                    new Vector3(FindObjectOfType<PlayerController>().transform.position.x, FindObjectOfType<PlayerController>().transform.position.y - 1, 0f),
                    Quaternion.identity);
                can.GetComponent<EnergyDrink>().isActivatingTextBox = isActivatingTextOnCan;
                can.GetComponent<EnergyDrink>().textBoxToActivate = textBoxToActivateOnCan;

                if (isSpawningCans) 
                {
                    for (int i = 0; i<spawnPoints.Length; i++)
                    {
                        Instantiate(energyDrink, (Vector3)spawnPoints[i], Quaternion.identity);
                    }
                }

                FindObjectOfType<AudioManager>().Play("VendingMachine");

                if (startRewind)
                {
                    FindObjectOfType<PlayerController>().canRewind = true;
                    rewindBar.SetActive(true);
                }
                if (!hasPressed)
                {
                    ApplyTextBox();
                    hasPressed = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            if (!hasPressed || infiniteDrinks)
            {
                playerIsIn = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            playerIsIn = false;
        }
    }

    void ApplyTextBox()
    {
        if (isActivatingTextBox)
        {
            textBoxToActivate.transform.position = FindObjectOfType<PlayerController>().transform.position;
            textBoxToActivate.SetActive(true);
        }
    }
}
