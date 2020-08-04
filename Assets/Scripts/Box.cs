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
        Vector2 direction = -(holder.GetComponent<Rigidbody2D>().position - rb.position);
        direction.x = Mathf.Round(direction.x);
        direction.y = Mathf.Round(direction.y);
        if (playerIsIn)
        {
            if (Input.GetKeyDown(holder.interactKey))
            {
                isHeld = true;
                transform.parent = holder.transform;
            }
            if (Input.GetKey(holder.interactKey))
            {
                if (holder.lookDirection == direction)
                {
                    if (direction.x > 0)
                    {
                        direction.Set(0.01f, 0);
                    }
                    if (direction.x < 0)
                    {
                        direction.Set(-0.01f, 0);
                    }
                    if (direction.y > 0)
                    {
                        direction.Set(0, 0.01f);
                    }
                    if (direction.y < 0)
                    {
                        direction.Set(0, -0.01f);
                    }
                    rb.position += direction;
                }
            }
            if (Input.GetKeyUp(holder.interactKey))
            {
                isHeld = false;
                transform.parent = null;
            }
        }

        if (direction.magnitude > 1.2)
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            playerIsIn = true;
        }
        else if (!collision.isTrigger)
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isHeld)
        {
            transform.parent = null;
            playerIsIn = false;
        }
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
