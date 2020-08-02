using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindBar : MonoBehaviour
{
    #region variables
    Slider slider;
    #endregion

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxRewind(float amount)
    {
        slider.maxValue = amount;
    }

    public void SetRewind(float amount)
    {
        slider.value = amount;
    }
}
