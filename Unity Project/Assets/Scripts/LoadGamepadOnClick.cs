using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGamepadOnClick : MonoBehaviour
{
    public static bool EnabledGamepad;

    public void LoadGamepad(bool Enabled)
    {
        EnabledGamepad = Enabled;
    }


}