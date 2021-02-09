using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileGuidanceScript : MonoBehaviour {

    public float speed;
    public float thrust;

    Vector3 moveDirection;


    //LookRotation
    public float V;
    public float H;
    public float Z;
    public float totalV;
    public float totalH;
    public float totalZ;
    public Quaternion tempRotation;


    //MouseMovement
    public float MouseSpeedHorizontal = 2.0f;
    public float MouseSpeedVertical = 2.0f;

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {




        //H = MouseSpeedHorizontal * Input.GetAxis("Mouse X");
        //V = MouseSpeedVertical * Input.GetAxis("Mouse Y");

        //totalV = totalV -= V;

        //if (totalV <= -90f || totalV >= 90f)
        //{
        //    totalH = totalH -= H;
        //}
        //else
        //{
        //    totalH = totalH += H;
        //}

        //tempRotation = Quaternion.Euler(totalV, totalH, totalZ);
        //transform.rotation = tempRotation;


        if (Input.GetKey(KeyCode.I))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.K))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * -speed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * -speed);
        }
    }
   
}
