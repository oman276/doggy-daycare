using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour
{
    public Slider slider;

    public void SetFill(int fill)
    {
        slider.value = fill;
    }

}
