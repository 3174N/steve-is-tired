using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 
{
    public int level;

    public Data (GameManager manager)
    {
        level = manager.level;
    }
}
