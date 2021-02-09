using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {

    // speed is the rate at which the object will rotate
    //public float speed;
    public float LookSpeed;
    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = LookSpeed; // Set this to be the distance you want the object to be placed in front of the camera.
        this.transform.LookAt(Camera.main.ScreenToWorldPoint(temp));
    }
}
