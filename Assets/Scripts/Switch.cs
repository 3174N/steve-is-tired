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

    bool playerIsIn;

    PlayerController player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
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
    }

    public void StateSwitch()
    {
        if (state == SwitchState.Off)
            state = SwitchState.On;
        else
            state = SwitchState.Off;
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
