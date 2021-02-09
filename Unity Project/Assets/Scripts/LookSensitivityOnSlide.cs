using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookSensitivityOnSlide : MonoBehaviour {

   
    public Slider slider;
    public static float Sensitivity = 50;


    public void SetSensitivity()
    {
        Sensitivity = slider.value;
    }
}
