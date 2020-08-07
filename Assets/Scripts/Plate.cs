using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Plate : Trigger
{
    #region variables
    public Sprite sprNotPressed, sprPressed;
    #endregion

    public void Update()
    {
        // texture
        if (state == TriggerState.On)
        {
            GetComponent<SpriteRenderer>().sprite = sprPressed;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprNotPressed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "box" || collision.GetComponent<PlayerController>() || collision.GetComponent<EnemyController>())
        {
            state = TriggerState.On;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "box" || collision.GetComponent<PlayerController>() || collision.GetComponent<EnemyController>())
        {
            state = TriggerState.Off;
        }
    }
}
