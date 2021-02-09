using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileScript : MonoBehaviour {

    public Text MissileText;
    public Text MissleSelectionText;
    public Text UIText;
    public GameObject MissleImage;

    // UI Text Fade Timer
    public float FadeTime;
    public float FadeSpeed = 1f;

    //// Shooting-----------------------------------

    public AudioSource audioSrc;
    public AudioClip MissileShootSFX;
    public GameObject MissileEmitterLeft;
    public GameObject MissileEmitterRight;
    public GameObject Missile;

    public float Missle_Forward_Force;
    public float MissleFireRate;
    public float nextFire;

    public static int Missiles = 50;
    public bool MissileLeft;
    public bool MissileRight;
    public bool MissileShot;

    public bool GamePaused;

    GameObject Projectile;

    void Start()
    {
       
        MissileShot = true;
        MissleSelectionText.text = "Missiles";
        MissileLeft = true;
        SetCountMissiles();
    }

    void Update()
    {
        GamePaused = PausingScript.paused;
        //===============================================UI Text Timer to Fade===============================================//
        if (FadeTime > 0)
        {
            FadeTime -= Time.deltaTime * FadeSpeed;
        }
        if (FadeTime < 0)
        {
            FadeTime = 0;
            UIText.text = "";
        }
        if (FadeTime >= 5)
        {
            FadeTime = 5;
        }

        if (Missiles >= 100)
        {
            Missiles = 100;
            SetCountMissiles();
        }
        if (Missiles > 0)
        {
            MissileShot = true;
        }
        //Shooting Secondary Weapon
        if ((Input.GetAxis("JoystickSecondaryFire") == 1 || Input.GetButton("MouseSecondaryFire")) && Time.time > nextFire && !GamePaused)
        {
            if (MissileShot)
            {
                if (Missiles <= 0)
                {
                    MissileShot = false;
                    Missiles = 0;
                    SetCountMissiles();
                }
                else
                {
                    nextFire = Time.time + MissleFireRate;
                    FireMissile();
                    Missiles -= 1;
                    SetCountMissiles();
                }
            }
            
            

        }
      
    }
    void FireMissile()
    {
        //audioSrc.clip = MissileShootSFX;
        audioSrc.PlayOneShot(MissileShootSFX, 0.7f);
        Projectile = Missile;
        GameObject Bullet_Handler;
        Rigidbody Temporary_RigidBody;

        if (MissileLeft)
        {
            MissileLeft = false;
            MissileRight = true;
            Bullet_Handler = Instantiate(Projectile, MissileEmitterLeft.transform.position, MissileEmitterLeft.transform.rotation) as GameObject;
            Bullet_Handler.transform.Rotate(Vector3.left * 90);
            Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.forward * Missle_Forward_Force);
            Destroy(Bullet_Handler, 5.0f);

        }
        else if (MissileRight)

        {
            MissileLeft = true;
            MissileRight = false;
            Bullet_Handler = Instantiate(Projectile, MissileEmitterRight.transform.position, MissileEmitterRight.transform.rotation) as GameObject;
            Bullet_Handler.transform.Rotate(Vector3.left * 90);
            Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.forward * Missle_Forward_Force);
            Destroy(Bullet_Handler, 5.0f);

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RocketAmmo"))
        {
            Missiles += 1;
            SetCountMissiles();
            UIText.text = "Rocket + 1";
            FadeTime += 5;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("RocketsAmmo"))
        {
            UIText.text = "Rockets +5";
            FadeTime += 5;
            Missiles += 5;
            SetCountMissiles();
            other.gameObject.SetActive(false);
        }
    }
    void SetCountMissiles()
    {
        MissileText.text = Missiles.ToString();
    }


}
