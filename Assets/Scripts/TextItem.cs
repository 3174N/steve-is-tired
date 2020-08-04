using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextItem
{
    public TextMeshProUGUI text;
    public Color color;

    public TextItem(TextMeshProUGUI text, Color color)
    {
        this.text = text;
        this.color = color;
    }
}
