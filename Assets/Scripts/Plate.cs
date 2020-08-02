using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Trigger
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Box box = collision.GetComponent<Box>();
        if (box != null && !box.isHeld) state = TriggerState.On;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Box box = collision.GetComponent<Box>();
        if (box != null) state = TriggerState.Off;
    }
}
