using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region variables
    public DoorTrigger[] triggers;

    bool shouldOpen = true;

    BoxCollider2D boxCollider;
    Animator animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            if (i == 0)
                shouldOpen = triggers[i].trigger.state == triggers[i].state;
            else
                shouldOpen = (triggers[i].trigger.state == triggers[i].state) && shouldOpen;
        }

        if (shouldOpen)
        {
            // Door is open
            boxCollider.isTrigger = true;
        }
        else
        {
            boxCollider.isTrigger = false;
        }

        animator.SetBool("IsOpen", shouldOpen);
    }
}
