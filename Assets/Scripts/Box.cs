using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    #region variables
    public bool isHeld = false;
    bool playerIsIn = false;
    PlayerController holder;

    string lastState;

    Rigidbody2D rb;
    #endregion

    private void Start()
    {
        holder = FindObjectOfType<PlayerController>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerIsIn)
        {   
            if (Input.GetKeyDown(holder.interactKey))
            {
                isHeld = true;
                transform.parent = holder.transform;
                if (holder.lookDirection == holder.GetComponent<Rigidbody2D>().position - rb.position)
                {
                    Debug.Log("Push");
                }
                //rb.isKinematic = false;
            }
            if (Input.GetKeyUp(holder.interactKey))
            {
                isHeld = false;
                transform.parent = null;
                //rb.isKinematic = true;
            }
        }
    }

    private void LateUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            playerIsIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isHeld)
        {
            transform.parent = null;
            playerIsIn = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
