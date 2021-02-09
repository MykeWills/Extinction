using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimaryGunScript : MonoBehaviour
{
    //Laser Cannon recoil
    private Vector3 LeftinitPos = new Vector3(-2.47f, 0f, 0f);
    private Vector3 LeftfinalPos = new Vector3(-2.47f, 0f, -0.1f);
    private Vector3 RightinitPos = new Vector3(2.47f, 0f, 0f);
    private Vector3 RightfinalPos = new Vector3(2.47f, 0f, -0.1f);
    float currentLerpTime;
    float currentLerpTime2;
    private Vector3 backPos;
    Transform LeftLaserGun;
    Transform RightLaserGun;
    bool RecoilLeft;
    bool RecoilRight;
    bool Recoiled;
    public GameObject LaserCannons;
    public GameObject LaserCannonUI;
    bool Spinning;
    bool PowerUp;
    bool PowerDown;
    GameObject MuzzleFlashObjectLeft;
    GameObject MuzzleFlashObjectRight;

    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;
    public GameObject LaserLVL1MuzzleFlashLeft;
    public GameObject LaserLVL1MuzzleFlashRight;
    public GameObject LaserLVL2MuzzleFlashLeft;
    public GameObject LaserLVL2MuzzleFlashRight;
    public GameObject LaserLVL3MuzzleFlashLeft;
    public GameObject LaserLVL3MuzzleFlashRight;
    public GameObject LaserLVL4MuzzleFlashLeft;
    public GameObject LaserLVL4MuzzleFlashRight;

    //GaussCannonBarrelSpin
    public bool GunSpin;
    public float SpinUpTime = 2;
    public float SpinUpTimer;
    public float MaxSpinRate = 360;
    public float SpinSpeed;
    public GameObject GaussCannons;
    public GameObject GaussCannonUI;
    Transform LeftCannonGun;
    Transform RightCannonGun;
    public GameObject GaussMuzzleFlashLeft;
    public GameObject GaussMuzzleFlashRight;
    public bool EnableGauss;
    GameObject Projectile;
   
    //Text UI Cockpit
    public Text WeaponText;
    public Text EnergyText;
    public Text AmmoText;
    public Text UIText;

    // Shooting Sound-----------------------------------
    public AudioSource PlayerAudSrc;

    public AudioClip LaserLVL1Shoot;
    public AudioClip LaserLVL2Shoot;
    public AudioClip LaserLVL3Shoot;
    public AudioClip LaserLVL4Shoot;
    public AudioClip LaserSelection;
    public AudioClip GaussShoot;
    public AudioClip EnergyPickup;
    public AudioClip LaserPickup;
    public AudioClip PlasmaShoot;
    public AudioClip MiniGunSpinUp;
    public AudioClip MiniGunSpinning;
    public AudioClip MiniGunSpinDown;


    //Bullet Emitters & Laser Objects
    public GameObject Bullet_Emitter_Left;
    public GameObject Bullet_Emitter_Right;
    public GameObject LaserBulletLVL1;
    public GameObject LaserBulletLVL2;
    public GameObject LaserBulletLVL3;
    public GameObject LaserBulletLVL4;


    //Quad Shooting
    public GameObject Bullet_Emitter_QuadLeft;
    public GameObject Bullet_Emitter_QuadRight;
    public bool QuadShot;

    //Gauss Shooting
    public GameObject Bullet_Emitter_Gauss_Left;
    public GameObject Bullet_Emitter_Gauss_Right;
    public GameObject GaussBullet;
    public GameObject GaussImage;
    public GameObject TextAmmo;
    public bool GaussLeft;
    public bool GaussRight;

    //Bullet Speed
    public float Bullet_Forward_Force;
    public float fireRate;
    public float nextFire;
    public float EnergyFillSpeed = 1.0f;
    public float Speed;

    //Lasers Active
    bool Laser;
    public bool AmmoShot;
    public bool LaserShot;
    public static bool LaserLVLOne;
    public static bool LaserLVLTwo;
    public static bool LaserLVLThree;
    public static bool LaserLVLFour;
    public static float Energy = 100;

    // Plasma Cannon
    public bool Plasma;
    public bool PlasmaShot;
    public GameObject PlasmaProjectile;

    //GaussCannon 
    bool Gauss;
    public static int Ammo = 500;

    //Refill Energy for Lasers
    public bool EnergyFill;
    

    // UI Text Fade Timer
    public float FadeTime;
    public float FadeSpeed = 1f;
    public bool GamePaused;
  

    void Start()
    {
        muzzleFlashTimerStart = muzzleFlashTimer;
        LaserCannonUI.SetActive(true);
        LaserCannons.SetActive(true);
        GaussCannonUI.SetActive(false);
        GaussCannons.SetActive(true);
        //EnableGauss = true;
        Laser = true;
        Plasma = false;
        LaserLVLOne = true;
        AmmoShot = true;
        LaserShot = true;
        PlasmaShot = true;
        GaussLeft = true;
        Gauss = false;
        EnergyFill = false;
        Recoiled = false;
        SetCountEnergy();
        SetCountAmmo();

    }

    void Update()
    {

        GamePaused = PausingScript.paused;
        StartCoroutine(FindGaussCannon());
        //===============================================MuzzleFlash===============================================//
        if (muzzleFlashEnabled == true)
        {
            if (Laser)
            {
                if (LaserLVLOne)
                {
                    LaserLVL1MuzzleFlashLeft.SetActive(true);
                    LaserLVL1MuzzleFlashRight.SetActive(true);
                }
                if (LaserLVLTwo)
                {
                    LaserLVL2MuzzleFlashLeft.SetActive(true);
                    LaserLVL2MuzzleFlashRight.SetActive(true);
                }
                if (LaserLVLThree)
                {
                    LaserLVL3MuzzleFlashLeft.SetActive(true);
                    LaserLVL3MuzzleFlashRight.SetActive(true);
                }
                if (LaserLVLFour)
                {
                    LaserLVL4MuzzleFlashLeft.SetActive(true);
                    LaserLVL4MuzzleFlashRight.SetActive(true);
                }
            }
            if (Gauss)
            {
                GaussMuzzleFlashLeft.SetActive(true);
                GaussMuzzleFlashRight.SetActive(true);
            }
            muzzleFlashTimer -= Time.deltaTime;
        }

        if (muzzleFlashTimer <= 0)
        {
            LaserLVL1MuzzleFlashLeft.SetActive(false);
            LaserLVL1MuzzleFlashRight.SetActive(false);
            LaserLVL2MuzzleFlashLeft.SetActive(false);
            LaserLVL2MuzzleFlashRight.SetActive(false);
            LaserLVL3MuzzleFlashLeft.SetActive(false);
            LaserLVL3MuzzleFlashRight.SetActive(false);
            LaserLVL4MuzzleFlashLeft.SetActive(false);
            LaserLVL4MuzzleFlashRight.SetActive(false);
            GaussMuzzleFlashLeft.SetActive(false);
            GaussMuzzleFlashRight.SetActive(false);
            muzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;
        }
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

        //===============================================Energy Refill Trigger===============================================//
        if (EnergyFill)
        {
            if (Energy >= 100 && EnergyFill)
            {
                EnergyFill = false;
            }
            if (Energy < 99)
            {
                Energy += Time.deltaTime * EnergyFillSpeed;
                SetCountEnergy();
                EnergyFill = true;
            }
        }
        if (Energy >= 200)
        {
            Energy = 200;
            SetCountEnergy();
        }
        if (Energy > 0)
        {
            LaserShot = true;
        }
        if (Ammo >= 1000)
        {
            Ammo = 1000;
            SetCountAmmo();
        }
        //===============================================SET Primary Weapon UI===============================================//

        //-------------Set Laser UI-------------------//
        if (Laser && !GamePaused)
        {
            StartCoroutine(FindLaserCannon());
            GaussCannonUI.SetActive(false);
            GaussCannons.SetActive(false);
            LaserCannonUI.SetActive(true);
            LaserCannons.SetActive(true);
            AmmoText.text = "";
            if (LaserLVLOne)
            {
                Projectile = LaserBulletLVL1;
                if (QuadShot)
                {
                    WeaponText.text = "Quad Laser LVL 1";
                    Speed = 4;
                }
                else
                {
                    WeaponText.text = "Laser LVL 1";
                    Speed = 2;
                }
            }
            else if (LaserLVLTwo)
            {
                Projectile = LaserBulletLVL2;
                if (QuadShot)
                {
                    WeaponText.text = "Quad Laser LVL 2";
                    Speed = 6;
                }
                else
                {
                    WeaponText.text = "Laser LVL 2";
                    Speed = 3;
                }
            }
            else if (LaserLVLThree)
            {
                Projectile = LaserBulletLVL3;
                if (QuadShot)
                {
                    WeaponText.text = "Quad Laser LVL 3";
                    Speed = 8;
                }
                else
                {
                    WeaponText.text = "Laser LVL 3";
                    Speed = 4;
                }
            }
            else if (LaserLVLFour)
            {
                Projectile = LaserBulletLVL4;
                if (QuadShot)
                {
                    WeaponText.text = "Quad Laser LVL 4";
                    Speed = 12;
                }
                else
                {
                    WeaponText.text = "Laser LVL 4";
                    Speed = 6;
                }
            }
        }
        //----------------------------------Set Gauss UI--------------------------//
        else if (Gauss && !GamePaused)
        {
            TextAmmo.SetActive(true);
            WeaponText.text = "Gauss Cannon";
            SetCountAmmo();
            Projectile = GaussBullet;
            GaussCannonUI.SetActive(true);
            GaussCannons.SetActive(true);
            LaserCannonUI.SetActive(false);
            LaserCannons.SetActive(false);

        }
        //-------------Set Plasma UI-------------------//
        else if (Plasma && !GamePaused)
        {
            Projectile = PlasmaProjectile;
            WeaponText.text = "Plasma Cannon";
            Speed = 5;
            GaussCannonUI.SetActive(false);
            GaussCannons.SetActive(false);
            LaserCannonUI.SetActive(false);
            LaserCannons.SetActive(false);
        }


        //===============================================Shoot Primary Weapon===============================================//
        if ((Input.GetAxis("JoystickPrimaryFire") == -1 || Input.GetKey(KeyCode.Mouse0)) && Laser && LaserShot)
        {

            //-------------Shoot Laser-----------------//
            if (Time.time > nextFire && !GamePaused)
            {
                Recoiled = true;
                muzzleFlashEnabled = true;
                if (Energy <= 0)
                {
                    LaserShot = false;
                    Energy = 0;
                    SetCountEnergy();
                }
                else
                {
                    fireRate = 0.2f;
                    Bullet_Forward_Force = 600;
                    nextFire = Time.time + fireRate;
                    Fire();
                }
            }
        }
        else if (Input.GetAxis("JoystickPrimaryFire") == -1 || Input.GetKey(KeyCode.Mouse0) && Plasma && PlasmaShot)
        {
            if (Time.time > nextFire && !GamePaused)
            {
                if (Energy <= 0)
                {
                    PlasmaShot = false;
                    Energy = 0;
                    SetCountEnergy();
                }
                else
                {
                    fireRate = 0.15f;
                    Bullet_Forward_Force = 400;
                    nextFire = Time.time + fireRate;
                    Fire();
                }
            }
        }
        //-------------Shoot Gauss-----------------//
        else if ((Input.GetAxis("JoystickPrimaryFire") == -1 || Input.GetKey(KeyCode.Mouse0)) && Gauss && AmmoShot)
        {
            GunSpin = true;
           
            if (Ammo <= 0)
            {
                AmmoShot = false;
                Ammo = 0;
                SetCountAmmo();
            }
            
            else if (GunSpin)
            {
                SpinUpTimer = Mathf.Clamp(SpinUpTimer + Time.deltaTime, 0, SpinUpTime);
                if (SpinUpTimer > 0 && SpinUpTimer < 0.1)
                {
                    PowerUp = true;
                }
              
                if (SpinUpTimer >= SpinUpTime && Time.time > nextFire && !GamePaused)
                {
                    fireRate = 0.1f;
                    Bullet_Forward_Force = 10000;
                    nextFire = Time.time + fireRate;
                    Fire();
                    PlayerAudSrc.clip = MiniGunSpinning;
                    PlayerAudSrc.Play();
                    PlayerAudSrc.loop = true;
                    nextFire = Time.time + fireRate;
                }
            }
        }
        else
        {
            GunSpin = false;
            SpinUpTimer = Mathf.Clamp(SpinUpTimer - Time.deltaTime, 0, SpinUpTime);
            if (SpinUpTimer < 2f && SpinUpTimer > 1.8f)
            {
                PlayerAudSrc.clip = MiniGunSpinDown;
                PlayerAudSrc.loop = false;
                PowerDown = true;
            }
        }
        float theta = (SpinUpTimer / SpinUpTime) * MaxSpinRate * Time.deltaTime * SpinSpeed;
        LeftCannonGun.transform.Rotate(Vector3.forward, theta);
        RightCannonGun.transform.Rotate(Vector3.forward, -theta);

        if (PowerUp)
        {
            PlayerAudSrc.PlayOneShot(MiniGunSpinUp, 0.7f);
            PowerUp = false;
        }
        if (PowerDown)
        {
            PlayerAudSrc.PlayOneShot(MiniGunSpinDown, 0.7f);
            PowerDown = false;
        }
        //=====================================Switch to Laser Weapon================================================//
        if (Input.GetButtonDown("WeaponSelect") && !GamePaused)
        {
            Laser = true;
            Gauss = false;
            Plasma = false;
            PlayerAudSrc.PlayOneShot(LaserSelection, 0.7f);
            if (Laser)
            {
                if (LaserLVLOne)
                {
                    LaserLVLOne = true;
                    LaserLVLTwo = false;
                    LaserLVLThree = false;
                    LaserLVLFour = false;
                }
                else if (LaserLVLTwo)
                {
                    LaserLVLOne = false;
                    LaserLVLTwo = true;
                    LaserLVLThree = false;
                    LaserLVLFour = false;
                }
                else if (LaserLVLThree)
                {
                    LaserLVLOne = false;
                    LaserLVLTwo = false;
                    LaserLVLThree = true;
                    LaserLVLFour = false;
                }
                else if (LaserLVLFour)
                {
                    LaserLVLOne = false;
                    LaserLVLTwo = false;
                    LaserLVLThree = false;
                    LaserLVLFour = true;
                }
            }
        }
        //==============================================Switch to Gauss Weapon=====================================//
        else if (Input.GetButtonDown("GaussSelect") && EnableGauss && !GamePaused)
        {
            Plasma = false;
            Laser = false;
            Gauss = true;
            PlayerAudSrc.PlayOneShot(LaserSelection, 0.7f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !GamePaused)
        {
            Plasma = true;
            Laser = false;
            Gauss = false;
            PlayerAudSrc.PlayOneShot(LaserSelection, 0.7f);
        }
        //===============================================Set Quad Laser===============================================//
        else if (Input.GetButtonDown("QuadSelect") && !GamePaused)
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
    }

    void OnTriggerEnter(Collider other)
    {
        //===============================================Energy Pickup===============================================//
        if (other.gameObject.CompareTag("Energy"))
        {
            PlayerAudSrc.PlayOneShot(EnergyPickup, 0.7f);
            UIText.text = "Energy +15";
            Energy += 15f;
            FadeTime += 5;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("GaussCannonPickUp"))
        {
            PlayerAudSrc.PlayOneShot(LaserPickup, 0.7f);
            EnableGauss = true;
            Gauss = true;
            UIText.text = "Gauss Cannon";
            Ammo += 500;
            FadeTime += 5;
            other.gameObject.SetActive(false);
        }

        //===============================================Gauss Ammo Pickup===============================================//
        if (other.gameObject.CompareTag("GaussAmmo"))
        {
            Ammo+= 100;
            other.gameObject.SetActive(false);
        }

        //===============================================Energy Refill Trigger===============================================//
        if (other.gameObject.CompareTag("EnergyRefill"))
        {
            EnergyFill = true;
        }

        //===============================================Laser Pickup===============================================//
        if (other.gameObject.CompareTag("LaserPickUp"))
        {
            if (LaserLVLOne)
            {
                PlayerAudSrc.PlayOneShot(LaserPickup, 0.7f);
                UIText.text = "Weapon Upgraded to Laser Level 2!";
                FadeTime += 5;
                Energy += 50;
                LaserLVLOne = false;
                LaserLVLTwo = true;
            }
            else if (LaserLVLTwo)
            {
                PlayerAudSrc.PlayOneShot(LaserPickup, 0.7f);
                UIText.text = "Weapon Upgraded to Laser Level 3!";
                FadeTime += 5;
                Energy += 50;
                LaserLVLTwo = false;
                LaserLVLThree = true;
            }
            else if (LaserLVLThree)
            {
                PlayerAudSrc.PlayOneShot(LaserPickup, 0.7f);
                UIText.text = "Weapon Upgraded to Laser Level 4!";
                FadeTime += 5;
                Energy += 50;
                LaserLVLThree = false;
                LaserLVLFour = true;
            }
            else
            {
                PlayerAudSrc.PlayOneShot(LaserPickup, 0.7f);
                UIText.text = "Lasers Cannot Be Upgraded Anymore!";
                FadeTime += 5;
                Energy += 50;
            }
      
            other.gameObject.SetActive(false);
        }
        SetCountEnergy();
        SetCountAmmo();  
    }


    void OnTriggerExit(Collider other)
    {
        //===============================================Energy Refill Trigger===============================================//
        if (other.gameObject.CompareTag("EnergyRefill"))
        {
            EnergyFill = false;
        }
    }
 

    void Fire()
    {
        GameObject Bullet_Handler;
        Rigidbody Temporary_RigidBody;
        if (Laser)
        {
            Energy -= Time.deltaTime * Speed;
            SetCountEnergy();
            if (LaserLVLTwo)
            {
                PlayerAudSrc.PlayOneShot(LaserLVL2Shoot, 1f);
            }
            else if (LaserLVLThree)
            {
                PlayerAudSrc.PlayOneShot(LaserLVL3Shoot, 1f);
            }
            else if (LaserLVLFour)
            {
                PlayerAudSrc.PlayOneShot(LaserLVL4Shoot, 1f);
            }
            else
            {
                PlayerAudSrc.PlayOneShot(LaserLVL1Shoot, 1f);
            }

            if (QuadShot)
            {
                
                Bullet_Handler = Instantiate(Projectile, Bullet_Emitter_QuadLeft.transform.position, Bullet_Emitter_QuadLeft.transform.rotation) as GameObject;
                Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Bullet_Handler, 5.0f);

                Bullet_Handler = Instantiate(Projectile, Bullet_Emitter_QuadRight.transform.position, Bullet_Emitter_QuadRight.transform.rotation) as GameObject;
                Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Bullet_Handler, 5.0f);
            }
            
            Bullet_Handler = Instantiate(Projectile, Bullet_Emitter_Left.transform.position, Bullet_Emitter_Left.transform.rotation) as GameObject;
            Bullet_Handler.transform.Rotate(Vector3.left * 90);
            Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Bullet_Handler, 5.0f);
            
            Bullet_Handler = Instantiate(Projectile, Bullet_Emitter_Right.transform.position, Bullet_Emitter_Right.transform.rotation) as GameObject;
            Bullet_Handler.transform.Rotate(Vector3.left * 90);
            Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Bullet_Handler, 5.0f);

        }
        else if (Gauss)
        {
            Ammo -= 1;

            PlayerAudSrc.PlayOneShot(GaussShoot, 0.7f);
            SetCountAmmo();

            if (GaussLeft)
            {
                muzzleFlashEnabled = true;
                GaussLeft = false;
                GaussRight = true;
                Bullet_Handler = Instantiate(Projectile, Bullet_Emitter_Gauss_Left.transform.position, Bullet_Emitter_Gauss_Left.transform.rotation) as GameObject;
                Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Bullet_Handler, 5.0f);
                
            }
            else if (GaussRight)
            {
                muzzleFlashEnabled = true;
                GaussLeft = true;
                GaussRight = false;
                Bullet_Handler = Instantiate(Projectile, Bullet_Emitter_Gauss_Right.transform.position, Bullet_Emitter_Gauss_Right.transform.rotation) as GameObject;
                Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Bullet_Handler, 5.0f);
                
            }
           
        }
        else if (Plasma)
        {
            Energy -= Time.deltaTime * Speed;
            SetCountEnergy();
            PlayerAudSrc.PlayOneShot(PlasmaShoot, 0.7f);

            Bullet_Handler = Instantiate(Projectile, Bullet_Emitter_Left.transform.position, Bullet_Emitter_Left.transform.rotation) as GameObject;
            Bullet_Handler.transform.Rotate(Vector3.left * 90);
            Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Bullet_Handler, 5.0f);

            Bullet_Handler = Instantiate(Projectile, Bullet_Emitter_Right.transform.position, Bullet_Emitter_Right.transform.rotation) as GameObject;
            Bullet_Handler.transform.Rotate(Vector3.left * 90);
            Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
            Destroy(Bullet_Handler, 5.0f);
        }

    }
    void SetCountEnergy()
    {
        EnergyText.text = Mathf.CeilToInt(Energy).ToString();
    }
    void SetCountAmmo()
    {
        AmmoText.text = Ammo.ToString();
    }
    //===============================================FindCannonGameObjects============================================//
    IEnumerator FindLaserCannon()
    {
        LeftLaserGun = GameObject.Find("MainPlayer/Player/MainShip/Camera/Main Camera/Ship/Lasers/LaserLeft").transform;
        RightLaserGun = GameObject.Find("MainPlayer/Player/MainShip/Camera/Main Camera/Ship/Lasers/LaserRight").transform;
        if (LeftLaserGun == null && RightLaserGun == null)
        {
            yield return null;
        }
        else
        {
            LeftLaserGun = GameObject.Find("MainPlayer/Player/MainShip/Camera/Main Camera/Ship/Lasers/LaserLeft").transform;
            RightLaserGun = GameObject.Find("MainPlayer/Player/MainShip/Camera/Main Camera/Ship/Lasers/LaserRight").transform;
            Recoiling();
        }
    }
    IEnumerator FindGaussCannon()
    {
        LeftCannonGun = GameObject.Find("MainPlayer/Player/MainShip/Camera/Main Camera/Ship/Cannons/GaussLeft").transform;
        RightCannonGun = GameObject.Find("MainPlayer/Player/MainShip/Camera/Main Camera/Ship/Cannons/GaussRight").transform;
        if (LeftCannonGun == null && RightCannonGun == null)
        {
            yield return null;
        }
        else
        {
            LeftCannonGun = GameObject.Find("MainPlayer/Player/MainShip/Camera/Main Camera/Ship/Cannons/GaussLeft").transform;
            RightCannonGun = GameObject.Find("MainPlayer/Player/MainShip/Camera/Main Camera/Ship/Cannons/GaussRight").transform;
        }
    }
    public void Recoiling()
    {
        if (Recoiled)
        {
            currentLerpTime += Time.deltaTime;
            currentLerpTime2 += Time.deltaTime;
            float perc = currentLerpTime / fireRate;
            float perc2 = currentLerpTime2 / fireRate;
            //==========================ShootCannonBack================================//
            if (currentLerpTime >= fireRate)
            {
                currentLerpTime = fireRate;
            }

            LeftLaserGun.transform.localPosition = Vector3.Lerp(LeftinitPos, LeftfinalPos, perc);
            RightLaserGun.transform.localPosition = Vector3.Lerp(RightinitPos, RightfinalPos, perc);
            //==========================ReturnCannonForward================================//
            if (LeftLaserGun.transform.localPosition == LeftfinalPos && RightLaserGun.transform.localPosition == RightfinalPos)
            {

                if (currentLerpTime2 >= fireRate)
                {
                    currentLerpTime2 = fireRate;
                }

                LeftLaserGun.transform.localPosition = Vector3.Lerp(LeftfinalPos, LeftinitPos, perc2);
                RightLaserGun.transform.localPosition = Vector3.Lerp(RightfinalPos, RightinitPos, perc2);
                //-------------------TurnoffLeftCannon-------------------//
                if (LeftLaserGun.transform.localPosition == LeftinitPos && RightLaserGun.transform.localPosition == RightinitPos)
                {
                    Recoiled = false;
                    currentLerpTime = 0;
                    currentLerpTime2 = 0;
                }
            }
        }
    }

}



