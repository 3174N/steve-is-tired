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
}
