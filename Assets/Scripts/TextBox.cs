using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    #region variables
    public TextItem[] texts;

    bool hasShown;
    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hasShown)
        {
            hasShown = true;
        }
    }
}
