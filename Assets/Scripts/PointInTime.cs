using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{
    public Vector2 position;
    public Switch switchSwitched;

    public PointInTime(Vector2 position, Switch switchesSwitched)
    {
        this.position = position;
        this.switchSwitched = switchesSwitched;
    }
}
