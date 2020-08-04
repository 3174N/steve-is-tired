using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class TextItem
{
    public string text;
    public Color color;
    public float duration;

    public TextItem(string text, Color color)
    {
        this.text = text;
        this.color = color;
    }
}
