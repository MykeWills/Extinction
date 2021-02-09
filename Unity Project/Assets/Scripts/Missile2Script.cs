using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile2Script : MonoBehaviour {


    //// Shooting-----------------------------------

    public AudioSource MissleShoot;
  
    public GameObject Bullet_Emitter_Middle;
   
    public GameObject Missle;

    public float Missle_Forward_Force;
    public float MissleFireRate;
    public float nextFire;

    public bool AmmoShot;

    void Start()
    {
        AmmoShot = true;
    }

    void Update()
    {
        //Shooting Secondary Weapon
        if (Input.GetButton("SecondaryFire2") && Time.time > nextFire && AmmoShot == true)
        {
            nextFire = Time.time + MissleFireRate;
            FireMissile();

        }
    }
    void FireMissile()
    {
        GameObject Temporary_Bullet_Handler_Middle;
        Rigidbody Temporary_RigidBody_Middle;
        MissleShoot.Play();

        Temporary_Bullet_Handler_Middle = Instantiate(Missle, Bullet_Emitter_Middle.transform.position, Bullet_Emitter_Middle.transform.rotation) as GameObject;
        Temporary_Bullet_Handler_Middle.transform.Rotate(Vector3.up);

        Temporary_RigidBody_Middle = Temporary_Bullet_Handler_Middle.GetComponent<Rigidbody>();
        Temporary_RigidBody_Middle.AddForce(transform.forward * Missle_Forward_Force);
        Destroy(Temporary_Bullet_Handler_Middle, 10.0f);
    }
        
}
