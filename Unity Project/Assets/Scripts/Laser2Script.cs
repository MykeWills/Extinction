using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser2Script : MonoBehaviour {

    //Quad Shooting
    public GameObject Bullet_Emitter_UpperLeft;
    public GameObject Bullet_Emitter_UpperRight;
    public bool QuadShot;

    // Shooting-----------------------------------
    public AudioSource LaserLVL1Shoot;
    public AudioSource LaserLVL2Shoot;
    public AudioSource LaserLVL3Shoot;
    public AudioSource LaserLVL4Shoot;
    public AudioSource LaserSelection;

    //Bullet Emitters & Laser Objects

    public GameObject Bullet_Emitter_Left;
    public GameObject Bullet_Emitter_Right;
    public GameObject LaserBulletLVL1;
    public GameObject LaserBulletLVL2;
    public GameObject LaserBulletLVL3;
    public GameObject LaserBulletLVL4;

    //Bullet Speed
    public float Bullet_Forward_Force;
    public float fireRate;
    public float nextFire;

    //Lasers Active
    public bool AmmoShot;
    public bool LaserLVLOne;
    public bool LaserLVLTwo;
    public bool LaserLVLThree;
    public bool LaserLVLFour;


    void Start()
    {
        AmmoShot = true;
        LaserLVLOne = true;
    }

    void Update()
    {
        // Debug Only================================================================================================================//
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("WeaponSelect2"))
        {
            LaserSelection.Play();
            if (LaserLVLOne)
            {
                LaserLVLOne = false; ;
                LaserLVLTwo = true;
                LaserLVLThree = false;
                LaserLVLFour = false;
            }
            else if (LaserLVLTwo)
            {
                LaserLVLOne = false;
                LaserLVLTwo = false;
                LaserLVLThree = true;
                LaserLVLFour = false;
            }
            else if (LaserLVLThree)
            {
                LaserLVLOne = false;
                LaserLVLTwo = false;
                LaserLVLThree = false;
                LaserLVLFour = true;
            }
            else if (LaserLVLFour)
            {
                LaserLVLOne = true;
                LaserLVLTwo = false;
                LaserLVLThree = false;
                LaserLVLFour = false;
            }

        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (QuadShot)
            {
                QuadShot = false;
            }
            else
            {
                QuadShot = true;
            }
        }
        //==========================================================================================================================//

        
        //Shooting Primary Weapon
        if (Input.GetButton("PrimaryFire2") && Time.time > nextFire && AmmoShot == true)
        {
            nextFire = Time.time + fireRate;
            FireLaser();
        }

    }

    void FireLaser()
    {
        GameObject Temporary_Bullet_Handler_Upper_Left;
        Rigidbody Temporary_RigidBody_Upper_Left;
        GameObject Temporary_Bullet_Handler_Upper_Right;
        Rigidbody Temporary_RigidBody_Upper_Right;
        GameObject Temporary_Bullet_Handler_Left;
        Rigidbody Temporary_RigidBody_Left;
        GameObject Temporary_Bullet_Handler_Right;
        Rigidbody Temporary_RigidBody_Right;

        if (LaserLVLOne)
        {
            LaserLVLTwo = false;
            LaserLVL1Shoot.Play();
            if (QuadShot)
            {
                Temporary_Bullet_Handler_Upper_Left = Instantiate(LaserBulletLVL1, Bullet_Emitter_UpperLeft.transform.position, Bullet_Emitter_UpperLeft.transform.rotation) as GameObject;
                Temporary_Bullet_Handler_Upper_Left.transform.Rotate(Vector3.left);
                Temporary_RigidBody_Upper_Left = Temporary_Bullet_Handler_Upper_Left.GetComponent<Rigidbody>();
                Temporary_RigidBody_Upper_Left.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler_Upper_Left, 5.0f);

                Temporary_Bullet_Handler_Upper_Right = Instantiate(LaserBulletLVL1, Bullet_Emitter_UpperRight.transform.position, Bullet_Emitter_UpperRight.transform.rotation) as GameObject;
                Temporary_Bullet_Handler_Upper_Right.transform.Rotate(Vector3.left);
                Temporary_RigidBody_Upper_Right = Temporary_Bullet_Handler_Upper_Right.GetComponent<Rigidbody>();
                Temporary_RigidBody_Upper_Right.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler_Upper_Right, 5.0f);
            }
            Temporary_Bullet_Handler_Left = Instantiate(LaserBulletLVL1, Bullet_Emitter_Left.transform.position, Bullet_Emitter_Left.transform.rotation) as GameObject;
            Temporary_Bullet_Handler_Left.transform.Rotate(Vector3.left);
            Temporary_RigidBody_Left = Temporary_Bullet_Handler_Left.GetComponent<Rigidbody>();
            Temporary_RigidBody_Left.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler_Left, 5.0f);

            Temporary_Bullet_Handler_Right = Instantiate(LaserBulletLVL1, Bullet_Emitter_Right.transform.position, Bullet_Emitter_Right.transform.rotation) as GameObject;
            Temporary_Bullet_Handler_Right.transform.Rotate(Vector3.left);
            Temporary_RigidBody_Right = Temporary_Bullet_Handler_Right.GetComponent<Rigidbody>();
            Temporary_RigidBody_Right.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler_Right, 5.0f);
        }
        if (LaserLVLTwo)
        {
            LaserLVL2Shoot.Play();

        if (QuadShot)
            {
                Temporary_Bullet_Handler_Upper_Left = Instantiate(LaserBulletLVL2, Bullet_Emitter_UpperLeft.transform.position, Bullet_Emitter_UpperLeft.transform.rotation) as GameObject;
                Temporary_Bullet_Handler_Upper_Left.transform.Rotate(Vector3.left);
                Temporary_RigidBody_Upper_Left = Temporary_Bullet_Handler_Upper_Left.GetComponent<Rigidbody>();
                Temporary_RigidBody_Upper_Left.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler_Upper_Left, 5.0f);

                Temporary_Bullet_Handler_Upper_Right = Instantiate(LaserBulletLVL2, Bullet_Emitter_UpperRight.transform.position, Bullet_Emitter_UpperRight.transform.rotation) as GameObject;
                Temporary_Bullet_Handler_Upper_Right.transform.Rotate(Vector3.left);
                Temporary_RigidBody_Upper_Right = Temporary_Bullet_Handler_Upper_Right.GetComponent<Rigidbody>();
                Temporary_RigidBody_Upper_Right.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler_Upper_Right, 5.0f);

            }
            Temporary_Bullet_Handler_Left = Instantiate(LaserBulletLVL2, Bullet_Emitter_Left.transform.position, Bullet_Emitter_Left.transform.rotation) as GameObject;
            Temporary_Bullet_Handler_Left.transform.Rotate(Vector3.left);
            Temporary_RigidBody_Left = Temporary_Bullet_Handler_Left.GetComponent<Rigidbody>();
            Temporary_RigidBody_Left.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler_Left, 5.0f);

            Temporary_Bullet_Handler_Right = Instantiate(LaserBulletLVL2, Bullet_Emitter_Right.transform.position, Bullet_Emitter_Right.transform.rotation) as GameObject;
            Temporary_Bullet_Handler_Right.transform.Rotate(Vector3.left);
            Temporary_RigidBody_Right = Temporary_Bullet_Handler_Right.GetComponent<Rigidbody>();
            Temporary_RigidBody_Right.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler_Right, 5.0f);
        }
        if (LaserLVLThree)
        {

            LaserLVL3Shoot.Play();
            if (QuadShot)
            {
                Temporary_Bullet_Handler_Upper_Left = Instantiate(LaserBulletLVL3, Bullet_Emitter_UpperLeft.transform.position, Bullet_Emitter_UpperLeft.transform.rotation) as GameObject;
                Temporary_Bullet_Handler_Upper_Left.transform.Rotate(Vector3.left);
                Temporary_RigidBody_Upper_Left = Temporary_Bullet_Handler_Upper_Left.GetComponent<Rigidbody>();
                Temporary_RigidBody_Upper_Left.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler_Upper_Left, 5.0f);

                Temporary_Bullet_Handler_Upper_Right = Instantiate(LaserBulletLVL3, Bullet_Emitter_UpperRight.transform.position, Bullet_Emitter_UpperRight.transform.rotation) as GameObject;
                Temporary_Bullet_Handler_Upper_Right.transform.Rotate(Vector3.left);
                Temporary_RigidBody_Upper_Right = Temporary_Bullet_Handler_Upper_Right.GetComponent<Rigidbody>();
                Temporary_RigidBody_Upper_Right.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler_Upper_Right, 5.0f);

            }
            Temporary_Bullet_Handler_Left = Instantiate(LaserBulletLVL3, Bullet_Emitter_Left.transform.position, Bullet_Emitter_Left.transform.rotation) as GameObject;
            Temporary_Bullet_Handler_Left.transform.Rotate(Vector3.left);
            Temporary_RigidBody_Left = Temporary_Bullet_Handler_Left.GetComponent<Rigidbody>();
            Temporary_RigidBody_Left.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler_Left, 5.0f);

            Temporary_Bullet_Handler_Right = Instantiate(LaserBulletLVL3, Bullet_Emitter_Right.transform.position, Bullet_Emitter_Right.transform.rotation) as GameObject;
            Temporary_Bullet_Handler_Right.transform.Rotate(Vector3.left);
            Temporary_RigidBody_Right = Temporary_Bullet_Handler_Right.GetComponent<Rigidbody>();
            Temporary_RigidBody_Right.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler_Right, 5.0f);
        }
        if (LaserLVLFour)
        {

            LaserLVL4Shoot.Play();
            if (QuadShot)
            {
                Temporary_Bullet_Handler_Upper_Left = Instantiate(LaserBulletLVL4, Bullet_Emitter_UpperLeft.transform.position, Bullet_Emitter_UpperLeft.transform.rotation) as GameObject;
                Temporary_Bullet_Handler_Upper_Left.transform.Rotate(Vector3.left);
                Temporary_RigidBody_Upper_Left = Temporary_Bullet_Handler_Upper_Left.GetComponent<Rigidbody>();
                Temporary_RigidBody_Upper_Left.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler_Upper_Left, 5.0f);

                Temporary_Bullet_Handler_Upper_Right = Instantiate(LaserBulletLVL4, Bullet_Emitter_UpperRight.transform.position, Bullet_Emitter_UpperRight.transform.rotation) as GameObject;
                Temporary_Bullet_Handler_Upper_Right.transform.Rotate(Vector3.left);
                Temporary_RigidBody_Upper_Right = Temporary_Bullet_Handler_Upper_Right.GetComponent<Rigidbody>();
                Temporary_RigidBody_Upper_Right.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler_Upper_Right, 5.0f);

            }
            Temporary_Bullet_Handler_Left = Instantiate(LaserBulletLVL4, Bullet_Emitter_Left.transform.position, Bullet_Emitter_Left.transform.rotation) as GameObject;
            Temporary_Bullet_Handler_Left.transform.Rotate(Vector3.left);
            Temporary_RigidBody_Left = Temporary_Bullet_Handler_Left.GetComponent<Rigidbody>();
            Temporary_RigidBody_Left.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler_Left, 5.0f);

            Temporary_Bullet_Handler_Right = Instantiate(LaserBulletLVL4, Bullet_Emitter_Right.transform.position, Bullet_Emitter_Right.transform.rotation) as GameObject;
            Temporary_Bullet_Handler_Right.transform.Rotate(Vector3.left);
            Temporary_RigidBody_Right = Temporary_Bullet_Handler_Right.GetComponent<Rigidbody>();
            Temporary_RigidBody_Right.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler_Right, 5.0f);
        }
    }

}


