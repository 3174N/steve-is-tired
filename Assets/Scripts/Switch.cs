using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    #region variables
    public enum SwitchState
    {
        On,
        Off
    }
    public SwitchState state = SwitchState.Off;

    public bool timePersistent = true;

    public bool hasTimeLimit;
    public float maxTimeLimit;
    float timeLimit;

    bool playerIsIn;

    PlayerController player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        timeLimit = maxTimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsIn)
        {
            if (Input.GetKeyDown(player.interactkey))
            {
                StateSwitch();
                if (!timePersistent)
                    player.switchInteractingWith = this;
            }
        }

        if (hasTimeLimit)
        {
            if (state == SwitchState.On)
                timeLimit -= Time.deltaTime;

            if (timeLimit <= 0)
            {
                state = SwitchState.Off;
                timeLimit = maxTimeLimit;
            }
        }
    }

    public void StateSwitch()
    {
        if (state == SwitchState.Off)
        {
            state = SwitchState.On;
            timeLimit = maxTimeLimit;
        }
        else
        {
            state = SwitchState.Off;
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
