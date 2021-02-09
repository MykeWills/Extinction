using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotationwithShip : MonoBehaviour {

    public static float GamepadSensitivity;
    float SensitivityMultiplier = 75f;
    public static float LookSensitivity;
    public static bool Invert;
    public static bool EnabledGamepad;
    public float KeyRotateSpeed;
    public float RotateSpeed;
    public float SnappingSpeed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Invert = LoadInvertOnClick.Invert;
        LookSensitivity = LookSensitivityOnSlide.Sensitivity;
        GamepadSensitivity = GamepadSensitivityOnSlide.PadSensitivity;
        EnabledGamepad = LoadGamepadOnClick.EnabledGamepad;

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
        if (Input.GetKey(KeyCode.Q) || Input.GetButton("RotationZRight"))
        {
            transform.Rotate(Vector3.forward * KeyRotateSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.E) || Input.GetButton("RotationZLeft"))
        {
            transform.Rotate(Vector3.back * KeyRotateSpeed * Time.deltaTime, Space.Self);
        }

        // Z Rotation Snapping =========================================================================//

        //float Axisz = transform.rotation.eulerAngles.z;

        //// 0-45------------------------------------------------------------------------------------

        //if (Axisz > 4f && Axisz < 41f)
        //{
        //    transform.Rotate(Vector3.forward * SnappingSpeed * -Time.deltaTime, Space.Self);
        //}

        //// 45-90-----------------------------------------------------------------------------------
        //if (Axisz > 49f && Axisz < 86f)
        //{
        //    transform.Rotate(Vector3.forward * SnappingSpeed * Time.deltaTime, Space.Self);
        //}

        //// 90-135----------------------------------------------------------------------------------
        //if (Axisz > 94f && Axisz < 131f)
        //{
        //    transform.Rotate(Vector3.forward * SnappingSpeed * -Time.deltaTime, Space.Self);
        //}

        //// 135 - 180-------------------------------------------------------------------------------
        //if (Axisz > 139f && Axisz < 176f)
        //{
        //    transform.Rotate(Vector3.forward * SnappingSpeed * Time.deltaTime, Space.Self);
        //}

        //// 180-225---------------------------------------------------------------------------------
        //if (Axisz > 184f && Axisz < 221f)
        //{
        //    transform.Rotate(Vector3.forward * SnappingSpeed * -Time.deltaTime, Space.Self);
        //}

        //// 225-270---------------------------------------------------------------------------------
        //if (Axisz > 229f && Axisz < 266f)
        //{
        //    transform.Rotate(Vector3.forward * SnappingSpeed * Time.deltaTime, Space.Self);
        //}

        //// 270-315---------------------------------------------------------------------------------
        //if (Axisz > 274f && Axisz < 311f)
        //{
        //    transform.Rotate(Vector3.forward * SnappingSpeed * -Time.deltaTime, Space.Self);
        //}

        //// 315-360---------------------------------------------------------------------------------
        //if (Axisz > 319f && Axisz < 356f)
        //{
        //    transform.Rotate(Vector3.forward * SnappingSpeed * Time.deltaTime, Space.Self);
        //}
    }
}
