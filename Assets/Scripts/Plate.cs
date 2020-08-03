﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Plate : Trigger
{
    #region variables
    public Box box;
    public bool isTouchingBox = false;

    public Sprite sprNotPressed, sprPressed;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Box enterBox = collision.GetComponent<Box>();
        if (enterBox != null)
        {
            box = enterBox;
            isTouchingBox = true;
        }
    }

    public void Update()
    {
        if (box == null) return;
        if (isTouchingBox && !box.isHeld) state = TriggerState.On;
        else state = TriggerState.Off;

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        Box exitBox = collision.GetComponent<Box>();
        if (exitBox != null)
            isTouchingBox = false;
        if (exitBox == box) box = null;
    }
}
