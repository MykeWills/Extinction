using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixDOFController : MonoBehaviour
{

    Rigidbody rb;
    Transform playerTransform;
    public bool Accelerate;
    public float Afterburn;

    //=======================Mouse & Keyboard=================================//
    public static float LookSensitivity;
    public float KeyRotateSpeed;
    public float RotateSpeed;
    public float SnappingSpeed;
   
    Vector3 MouseMoveDirection;
    Vector3 MouseStrafeDirection;
    
    public float MouseMoveSpeed;
    public float MouseStrafeSpeed;

    //=======================Gamepad=================================//

    Vector3 StrafeAbscissaX;
    Vector3 StrafeOrdinateY;
    Vector3 StrafeApplicateZ;

    public static float GamepadSensitivity;
    float SensitivityMultiplier = 75f;

    //=======================Settings=================================//

    public static bool Invert;
    public static bool EnabledGamepad;
    bool Snapping;
    bool Locked;

    void Start()
    {
        Snapping = true;
        rb = GetComponent<Rigidbody>();
        EnabledGamepad = true;
       
    }

    void Update()
    {
        Invert = LoadInvertOnClick.Invert;
        LookSensitivity = LookSensitivityOnSlide.Sensitivity;
        GamepadSensitivity = GamepadSensitivityOnSlide.PadSensitivity;
        EnabledGamepad = LoadGamepadOnClick.EnabledGamepad;

        // Mouse Look Movement ==============================================================================//

        float MouseY = Input.GetAxisRaw("Mouse Y");
        float MouseX = Input.GetAxisRaw("Mouse X");

        float LookX = Input.GetAxis("RightStickX");
        float LookY = Input.GetAxis("RightStickY");

        //=============================================Looking on X Axis ============================================//

        //Mouse
        if (MouseX > 0)
        {
            transform.Rotate(Vector3.up * MouseX * LookSensitivity * Time.deltaTime, Space.Self);
        }
        if (MouseX < 0)
        {
            transform.Rotate(Vector3.down * -MouseX * LookSensitivity * Time.deltaTime, Space.Self);
        }
        //Gamepad
        if (EnabledGamepad)
        {
            if (LookX > 0)
            {
                transform.Rotate(Vector3.up * LookX * GamepadSensitivity * SensitivityMultiplier * Time.deltaTime, Space.Self);
            }
            if (LookX < 0)
            {
                transform.Rotate(Vector3.down * -LookX * GamepadSensitivity * SensitivityMultiplier * Time.deltaTime, Space.Self);
            }
        }
        
        //=============================================Looking on Y Axis ============================================//


        //Inversion
        if (Invert)
        {
            // Mouse
            if (MouseY > 0)
            {
                transform.Rotate(Vector3.left * MouseY * LookSensitivity * Time.deltaTime, Space.Self);
            }
            if (MouseY < 0)
            {
                transform.Rotate(Vector3.right * -MouseY * LookSensitivity * Time.deltaTime, Space.Self);
            }

            // Gamepad
            if (EnabledGamepad)
            {
                if (LookY > 0)
                {
                    transform.Rotate(Vector3.left * LookY * GamepadSensitivity * SensitivityMultiplier * Time.deltaTime, Space.Self);
                }
                if (LookY < 0)
                {
                    transform.Rotate(Vector3.right * -LookY * GamepadSensitivity * SensitivityMultiplier * Time.deltaTime, Space.Self);
                }
            }
            
        }
        //Normal
        else
        {
            //Mouse
            if (MouseY > 0)
            {
                transform.Rotate(Vector3.left * -MouseY * LookSensitivity * Time.deltaTime, Space.Self);
            }
            if (MouseY < 0)
            {
                transform.Rotate(Vector3.right * MouseY * LookSensitivity * Time.deltaTime, Space.Self);
            }

            // Gamepad
            if (EnabledGamepad)
            {
                if (LookY > 0)
                {
                    transform.Rotate(Vector3.left * -LookY * GamepadSensitivity * SensitivityMultiplier * Time.deltaTime, Space.Self);
                }
                if (LookY < 0)
                {
                    transform.Rotate(Vector3.right * LookY * GamepadSensitivity * SensitivityMultiplier * Time.deltaTime, Space.Self);
                }
            }

        }

        // Keyboard Movement =====================================================================================//
        float HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float VerticalMovement = Input.GetAxisRaw("Vertical");

        MouseStrafeDirection = (HorizontalMovement * transform.right);
        MouseMoveDirection = (VerticalMovement * transform.forward);

        // Gamepad Movement =====================================================================================//
        if (EnabledGamepad)
        {
            float AbscissaMovement = Input.GetAxisRaw("LeftStickX");
            float OrdinateMovement = Input.GetAxisRaw("LeftStickY");
            float ApplicateMovement = Input.GetAxisRaw("DPadY");

            StrafeAbscissaX = (AbscissaMovement * transform.right);
            StrafeOrdinateY = (OrdinateMovement * transform.forward);
            StrafeApplicateZ = (ApplicateMovement * transform.up);
        }
        

        // Rotate Ship with Keys ==================================================================================

        //Keyboard
        if (Input.GetKey(KeyCode.Q) || Input.GetButton("RotationZRight"))
        {
            Snapping = false;
            transform.Rotate(Vector3.forward * KeyRotateSpeed * Time.deltaTime, Space.Self);
        }
        else if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("RotationZRight"))
        {
            Snapping = true;
        }

        if (Input.GetKey(KeyCode.E) || Input.GetButton("RotationZLeft"))
        {
            Snapping = false;
            transform.Rotate(Vector3.back * KeyRotateSpeed * Time.deltaTime, Space.Self);
        }
        else if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("RotationZLeft"))
        {
            Snapping = true;
        }

        // Z Rotation Snapping =========================================================================//


        if (Snapping)
        {
            float Axisz = transform.rotation.eulerAngles.z;
            // 0-45------------------------------------------------------------------------------------
            if (Axisz > 4f && Axisz < 41f)
            {
                transform.Rotate(Vector3.back * SnappingSpeed * Time.deltaTime, Space.Self);
                Locked = true;
            }

            // 45-90-----------------------------------------------------------------------------------
            if (Axisz > 49f && Axisz < 86f)
            {
                transform.Rotate(Vector3.forward * SnappingSpeed * Time.deltaTime, Space.Self);
                Locked = true;
            }

            // 90-135----------------------------------------------------------------------------------
            if (Axisz > 94f && Axisz < 131f)
            {
                transform.Rotate(Vector3.back * SnappingSpeed * Time.deltaTime, Space.Self);
                Locked = true;
            }

            // 135 - 180-------------------------------------------------------------------------------
            if (Axisz > 139f && Axisz < 176f)
            {
                transform.Rotate(Vector3.forward * SnappingSpeed * Time.deltaTime, Space.Self);
                Locked = true;
            }

            // 180-225---------------------------------------------------------------------------------
            if (Axisz > 184f && Axisz < 221f)
            {
                transform.Rotate(Vector3.back * SnappingSpeed * Time.deltaTime, Space.Self);
                Locked = true;
            }

            // 225-270---------------------------------------------------------------------------------
            if (Axisz > 229f && Axisz < 266f)
            {
                transform.Rotate(Vector3.forward * SnappingSpeed * Time.deltaTime, Space.Self);
                Locked = true;
            }

            // 270-315---------------------------------------------------------------------------------
            if (Axisz > 274f && Axisz < 311f)
            {
                transform.Rotate(Vector3.back * SnappingSpeed * Time.deltaTime, Space.Self);
                Locked = true;
            }

            // 315-360---------------------------------------------------------------------------------
            if (Axisz > 319f && Axisz < 356f)
            {
                transform.Rotate(Vector3.forward * SnappingSpeed * Time.deltaTime, Space.Self);
                Locked = true;
            }

            //if (Locked)
            //{
            //    StartCoroutine(FindPlayer());
            //    //==================0Degrees===================================//
            //    if (Axisz < 4)
            //    {
            //        playerTransform.localRotation = Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, 0);
            //        Locked = false;
            //    }
            //    else if (Axisz > 356)
            //    {
            //        playerTransform.localRotation = Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, 0);
            //        Locked = false;
            //    }
            //    //==================90Degrees===================================//
            //    if (Axisz < 94)
            //    {
            //        playerTransform.localRotation = Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, 90);
            //        Locked = false;
            //    }
            //    else if(Axisz > 86)
            //    {
            //        playerTransform.localRotation = Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, 90);
            //        Locked = false;
            //    }
            //    //==================180Degrees===================================//
            //    if(Axisz < 184)
            //    {
            //        playerTransform.localRotation = Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, 180);
            //        Locked = false;
            //    }
            //    else if(Axisz > 176)
            //    {
            //        playerTransform.localRotation = Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, 180);
            //        Locked = false;
            //    }
            //    //==================270Degrees===================================//
            //    if(Axisz < 274)
            //    {
            //        playerTransform.localRotation = Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, 270);
            //        Locked = false;
            //    }
            //    else if(Axisz > 266)
            //    {
            //        playerTransform.localRotation = Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, 270);
            //        Locked = false;
            //    }
            //}

        }
           
       

        // Afterburner
        if (Input.GetButtonDown("AfterBurner"))
        {
            Accelerate = true;
        }
        else if (Input.GetButtonUp("AfterBurner"))
        {
            Accelerate = false;
        }

    }

    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (Accelerate)
        {
            //Keyboard
            rb.AddForce(MouseMoveDirection * MouseMoveSpeed * Time.deltaTime * Afterburn);
            rb.AddForce(MouseStrafeDirection * MouseStrafeSpeed * Time.deltaTime);
        }
        else
        {
            //Keyboard
            rb.AddForce(MouseMoveDirection * MouseMoveSpeed * Time.deltaTime);
            rb.AddForce(MouseStrafeDirection * MouseStrafeSpeed * Time.deltaTime);
        }
        //Gamepad
        if (EnabledGamepad)
        {
            rb.AddForce(StrafeAbscissaX * MouseMoveSpeed * Time.deltaTime);
            rb.AddForce(StrafeOrdinateY * MouseStrafeSpeed * Time.deltaTime);
            rb.AddForce(StrafeApplicateZ * MouseStrafeSpeed * Time.deltaTime * 2.0f);
        }
       
        
    }
    IEnumerator FindPlayer()
    {
        playerTransform = GameObject.Find("/MainPlayer/Player/").transform;
        if(playerTransform == null)
        {
            yield return null;
        }
        else
        {
            playerTransform = GameObject.Find("/MainPlayer/Player/").transform;
        }
    }
    

   

}

