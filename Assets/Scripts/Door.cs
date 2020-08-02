using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region variables
    public Trigger[] triggers;

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
        foreach (Trigger t in triggers) {
            if (Trigger.TriggerState.On == t.state)
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
}
