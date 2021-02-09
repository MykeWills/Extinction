using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

   

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            Time.timeScale = 0.3f;

        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            Time.timeScale = 1.0f;

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            
            if (Time.timeScale == 1)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
	}
}
