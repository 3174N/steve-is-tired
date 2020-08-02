using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region variables
    public Trigger.TriggerState triggerState;
    public Trigger[] triggers;

    bool shouldOpen = true;

    BoxCollider2D boxCollider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            if (i == 0)
                shouldOpen = triggers[i].state == triggerState;
            else
                shouldOpen = (triggers[i].state == triggerState) && shouldOpen;
        }

        if (shouldOpen)
        {
            // Door is open
            boxCollider.isTrigger = true;

            // Add aniamtion?
            // Color change is temporary
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            boxCollider.isTrigger = false;

            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
