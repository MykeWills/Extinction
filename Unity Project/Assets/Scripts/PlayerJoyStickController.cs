using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyStickController : MonoBehaviour
{
   
    Rigidbody rb;
    Quaternion tempRotation;

    // Joystick/GamePad Settings
    Vector3 StrafeAbscissaX;
    Vector3 StrafeOrdinateY;
    Vector3 StrafeApplicateZ;

    public float AbscissaXSpeed;
    public float OrdinateYSpeed;
    public float ApplicateZSpeed;

    public float LookSensitivityX;
    public float LookSensitivityY;

    private float LookX;
    private float LookY;

    float XTotal;
    float YTotal;

    public bool UpsideDown;
    public bool Inverted;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        UpsideDown = false;



    }

    void Update()
    {
         // JoyStick Movement
        float AbscissaMovement = Input.GetAxisRaw("LeftStickX");
        float OrdinateMovement = Input.GetAxisRaw("LeftStickY");
        float ApplicateMovement = Input.GetAxisRaw("DPadY");

        StrafeAbscissaX = (AbscissaMovement * transform.right);
        StrafeOrdinateY = (OrdinateMovement * transform.forward);
        StrafeApplicateZ = (ApplicateMovement * transform.up);

        LookX = Input.GetAxis("RightStickX") * LookSensitivityX *2f; 
        LookY = Input.GetAxis("RightStickY") * LookSensitivityY *2f;


        if (Inverted)
        {
            YTotal = YTotal -= LookY;
        }
        else
        {
            YTotal = YTotal += LookY;
        }

        if (UpsideDown)
        {
            XTotal = XTotal -= LookX;
        }
        else
        {
            XTotal = XTotal += LookX;
        }

    
        if (YTotal > 0f && YTotal < 90f || YTotal < 0f && YTotal > -90f)
        {
            UpsideDown = false;
        }
        else if (YTotal > 90f && YTotal < 180f || YTotal < -90f && YTotal > -180f)
        {
            UpsideDown = true;
        }
        else if (YTotal > 180f && YTotal < 270 || YTotal < -180f && YTotal > -270)
        {
            UpsideDown = true;
        }
        else if (YTotal > 270f && YTotal < 360 || YTotal < -270f && YTotal > -360)
        {
            UpsideDown = false;
        }


        if (XTotal < -360 || XTotal > 360)
        {
            XTotal = 0;
        }
        if (YTotal < -360 || YTotal > 360)
        {
            YTotal = 0;
        }

        tempRotation = Quaternion.Euler(YTotal, XTotal, 0);
        transform.rotation = tempRotation;

    }

    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        rb.AddForce(StrafeAbscissaX * AbscissaXSpeed * Time.deltaTime);
        rb.AddForce(StrafeOrdinateY * OrdinateYSpeed * Time.deltaTime);
        rb.AddForce(StrafeApplicateZ * ApplicateZSpeed * Time.deltaTime);


    }
}

