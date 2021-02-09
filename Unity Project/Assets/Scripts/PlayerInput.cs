using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    ShipHandler sHand;

    Vector3 moveInput;
    Vector3 rotInput;
    bool CursorLocked;

    bool Powered = true;

    // Use this for initialization
    void Start()
    {
        sHand = GetComponent<ShipHandler>();
        Cursor.lockState = CursorLockMode.Locked;
        CursorLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        float H = Input.GetAxisRaw("Horizontal");
        float V = Input.GetAxisRaw("Vertical");
        float U = Input.GetAxisRaw("DirectionalX");

        float rH = Input.GetAxisRaw("Mouse X");
        float rV = Input.GetAxisRaw("Mouse Y");
        float rU = Input.GetAxisRaw("DirectionalY");

        moveInput = new Vector3(H, U, V);
        rotInput = new Vector3(rV, rH, rU);

        if (Input.GetKeyDown(KeyCode.P))
        {
            Powered = !Powered;
        }
        outOfLock();

    }
    void FixedUpdate()
    {
        sHand.MoveInput(moveInput, rotInput, Powered);
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
