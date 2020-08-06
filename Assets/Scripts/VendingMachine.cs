using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    #region variables
    public bool infinitDrinks;

    public GameObject energyDrink;

    bool hasPressed = false;
    bool playerIsIn;
    #endregion

    private void Update()
    {
        if (playerIsIn)
        {
            if (Input.GetKeyDown(FindObjectOfType<PlayerController>().interactKey))
            {
                Instantiate(energyDrink,
                    new Vector3(transform.position.x, transform.position.y - 3, 0f),
                    Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            if (!hasPressed || infinitDrinks)
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
}