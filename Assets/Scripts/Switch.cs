using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Trigger
{
    #region variables
    
    public bool timePersistent = true;

    public bool hasTimeLimit;
    public float maxTimeLimit;
    float timeLimit;

    bool playerIsIn;

    PlayerController player;

    public Sprite sprOn, sprOff;
    SpriteRenderer sprRend;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        sprRend = GetComponent<SpriteRenderer>();

        timeLimit = maxTimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsIn)
        {
            if (Input.GetKeyDown(player.interactKey))
            {
                StateSwitch();
                if (!timePersistent)
                    player.switchInteractingWith = this;
            }
        }

        if (hasTimeLimit)
        {
            if (state == TriggerState.On)
                timeLimit -= Time.deltaTime;

            if (timeLimit <= 0)
            {
                state = TriggerState.Off;
                timeLimit = maxTimeLimit;
            }
        }
        if (state == TriggerState.On) sprRend.sprite = sprOn;
        else sprRend.sprite = sprOff;
    }

    public void StateSwitch()
    {
        if (state == TriggerState.Off)
        {
            state = TriggerState.On;
            timeLimit = maxTimeLimit;
        }
        else
        {
            state = TriggerState.Off;
            timeLimit = maxTimeLimit;
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
        playerIsIn = false;
    }
}
