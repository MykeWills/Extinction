using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseController : MonoBehaviour
{
   
    Rigidbody rb;
    Quaternion tempRotation;

    // Mouse/Keyboard Settings
    Vector3 MouseMoveDirection;
    Vector3 MouseStrafeDirection;

    public float MouseMoveSpeed;
    public float MouseStrafeSpeed;

    public float MouseSpeedHorizontal;
    public float MouseSpeedVertical;

    private float MouseVerticalLook;
    private float MouseHorizontalLook;

    private float VerticalTotal;
    private float HorizontalTotal;

    // Optional Settings
    private bool CursorLocked;
    private bool UpsideDown;
    public bool Inverted;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        CursorLocked = true;
        UpsideDown = false;
    }

    void Update()
    {
        // MouseMovement
        float HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float VerticalMovement = Input.GetAxisRaw("Vertical");

        MouseHorizontalLook = MouseSpeedHorizontal * Input.GetAxis("Mouse X");
        MouseVerticalLook = MouseSpeedVertical * Input.GetAxis("Mouse Y");

        MouseStrafeDirection = (HorizontalMovement * transform.right);
        MouseMoveDirection = (VerticalMovement * transform.forward);

        if (Inverted)
        {
            VerticalTotal = VerticalTotal += MouseVerticalLook;
        }
        else
        {
            VerticalTotal = VerticalTotal -= MouseVerticalLook;
        }

        if (UpsideDown)
        {
            HorizontalTotal = HorizontalTotal -= MouseHorizontalLook;
        }
        else
        {
            HorizontalTotal = HorizontalTotal += MouseHorizontalLook;
        }

        if (VerticalTotal > 0f && VerticalTotal < 90f || VerticalTotal < 0f && VerticalTotal > -90f)
        {
            UpsideDown = false;
        }
        else if (VerticalTotal > 90f && VerticalTotal < 180f || VerticalTotal < -90f && VerticalTotal > -180f)
        {
            UpsideDown = true;
        }
        if (VerticalTotal > 180f && VerticalTotal < 270 || VerticalTotal < -180f && VerticalTotal > -270)
        {
            UpsideDown = true;
        }
        if (VerticalTotal > 270f && VerticalTotal < 360 || VerticalTotal < -270f && VerticalTotal > -360)
        {
            UpsideDown = false;
        }
        if (VerticalTotal < -360 || VerticalTotal > 360)
        {
            VerticalTotal = 0;
        }

        tempRotation = Quaternion.Euler(VerticalTotal, HorizontalTotal, 0 * Time.deltaTime) ;
        transform.rotation = tempRotation;
        outOfLock();
    }

    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        rb.AddForce(MouseMoveDirection * MouseMoveSpeed * Time.deltaTime);
        rb.AddForce(MouseStrafeDirection * MouseStrafeSpeed * Time.deltaTime);
    }
    private void outOfLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                CursorLocked = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                CursorLocked = true;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

