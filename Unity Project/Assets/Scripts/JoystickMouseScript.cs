using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMouseScript : MonoBehaviour {

    public bool Mouse;
    public bool Joystick;

    private PlayerMouseController MouseScript;
    private PlayerJoyStickController JoyStickScript;


    // Use this for initialization
    void Start () {
       
        MouseScript = GetComponent<PlayerMouseController>();
        JoyStickScript = GetComponent<PlayerJoyStickController>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Mouse)
        {
            JoyStickScript.enabled = false;
            MouseScript.enabled = true;
        }
        else if (Joystick)
        {
            MouseScript.enabled = false;
            JoyStickScript.enabled = true;
        }
        else
        {
            JoyStickScript.enabled = false;
            MouseScript.enabled = true;
        }
		

	}
}
