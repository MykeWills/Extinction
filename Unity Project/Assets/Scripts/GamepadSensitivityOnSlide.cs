using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamepadSensitivityOnSlide : MonoBehaviour
{


    public Slider slider;
    public static float PadSensitivity = 20;


    public void SetSensitivity()
    {
        PadSensitivity = slider.value;
    }
}
