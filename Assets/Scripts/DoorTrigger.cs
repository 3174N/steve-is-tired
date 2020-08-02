using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorTrigger
{
    public Trigger trigger;
    public Trigger.TriggerState state;

    public DoorTrigger(Trigger trigger, Trigger.TriggerState state)
    {
        this.trigger = trigger;
        this.state = state;
    }
}
