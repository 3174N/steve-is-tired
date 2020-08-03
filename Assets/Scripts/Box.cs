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
                isHeld = !isHeld;
            }
        }
    }

    private void LateUpdate()
    {
        if (isHeld)
        {
            float offset = 1.2f;

            string state = CheckDirection(holder.lookDirection);
            lastState = state == "" ? lastState : state;
            if (lastState == "LEFT")
                rb.MovePosition(new Vector2(holder.transform.position.x + offset, holder.transform.position.y));
            else if (lastState == "RIGHT")
                rb.MovePosition(new Vector2(holder.transform.position.x - offset, holder.transform.position.y));
            else if (lastState == "UP")
                rb.MovePosition(new Vector2(holder.transform.position.x, holder.transform.position.y + offset));
            else if (lastState == "DOWN")
                rb.MovePosition(new Vector2(holder.transform.position.x, holder.transform.position.y - offset));
        }
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
            playerIsIn = false;
        }
    }

    private string CheckDirection(Vector2 dir)
    {
        if (dir == new Vector2(1, 0))
            return "LEFT";
        if (dir == new Vector2(-1, 0))
            return "RIGHT";
        if (dir == new Vector2(0, 1))
            return "UP";
        if (dir == new Vector2(0, -1))
            return "DOWN";

        else return "";
    }
}
