using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAnimationScript : MonoBehaviour {

    //rotation
    public float rotateX;
    public float rotateY;
    public float rotateZ;

    //sine wave
    public float amplitudeY;
    public float omegaY;
    float index;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime);

        index += Time.deltaTime;
        float y = amplitudeY * Mathf.Cos(omegaY * index);
        transform.localPosition = new Vector3(0, y, 0);
    }
}
