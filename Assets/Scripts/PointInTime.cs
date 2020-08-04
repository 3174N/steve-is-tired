using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{
    public Vector2 position;
    public Vector2 lookDirection;
    public Switch switchSwitched;

    public PointInTime(Vector2 position, Vector2 lookDirection, Switch switchesSwitched)
    {
        this.position = position;
        this.lookDirection = lookDirection;
        this.switchSwitched = switchesSwitched;
    }
}
