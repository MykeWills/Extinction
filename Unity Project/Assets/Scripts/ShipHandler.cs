using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHandler : MonoBehaviour {

    Vector3 posInput;
    Vector3 rotInput;

    bool Powered = true;
    float Speed = 150;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveInput(Vector3 move, Vector3 rote, bool power)
    {
        posInput = move;
        rotInput = rote;
        Powered = power;

        ActuallyMove();
    }
    void ActuallyMove()
    {
        if (Powered)
        {
            Speed = 150;
            rb.drag = 10;
        }
        else
        {
            Speed = 0;
            rb.drag = 0;
        }

        rb.AddRelativeForce(posInput * Speed);
        rb.AddRelativeTorque(rotInput);

    }
}
