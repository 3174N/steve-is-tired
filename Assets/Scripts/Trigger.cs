using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public enum TriggerState
    {
        On,
        Off
    }

    public TriggerState state = TriggerState.Off;

    public bool isActivatingTextBox;
    public GameObject textBoxToActivate;

    public void ApplyTextBox()
    {
        if (isActivatingTextBox)
        {
            textBoxToActivate.transform.position = FindObjectOfType<PlayerController>().transform.position;
            textBoxToActivate.SetActive(true);
        }
    }
}
