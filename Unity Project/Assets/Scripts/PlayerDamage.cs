using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour {

    //=============ShakeScreen=======================================//
    public float shakeDuration;
    public float shakeAmount = 0.7f;
    float decreaseFactor = 1.0f;

    public Transform camTransform;
    Vector3 originalPos;
    public static Vector3 LastCheckPoint;
    public GameObject NormalLight;
    public GameObject LavaLight;
    public GameObject SlimeLight;
    //================================================================//
    public GameObject DamageScreen;
    public GameObject LavaScreen;
    public GameObject SlimeScreen;
    public AudioSource audioSrc;
    public AudioClip PlayerHitSFX;
    public AudioClip EnemyDrillSFX;
    public AudioClip EnemyMineSFX;
    public AudioClip MissileImpact;
    public AudioClip LaserImpact;
    public AudioClip GaussImpact;
    public AudioClip LavaDamage;
    public AudioClip SlimeDamage;
    public AudioClip ShieldPickup;

    public Text LivesText;
    public Text ShieldText;
    public static float Shield = 100;
    public static int Lives = 3;

    public GameObject LoadingScreenCanvas;
    public GameObject MainGameCanvas;

    bool redScreenFlashEnabled = false;
    float redScreenFlashTimer = 0.5f;
    float redScreenFlashTimerStart;

    bool Easy;
    bool Normal;
    bool Hard;
    bool Lava;
    bool Slime;

    public bool PlayerDamaged;
    public bool TakeDamage;
    public bool MissileDamage;

    public Text UIText;
    public float FadeTime;
    public float FadeSpeed = 1f;

    // Use this for initialization
    void Start () {


        LastCheckPoint = gameObject.transform.position;
        redScreenFlashTimerStart = redScreenFlashTimer;
        originalPos = camTransform.localPosition;
        Easy = true;
        Lava = false;
        Slime = false;
        PlayerDamaged = false;
        SetCountShield();
        SetCountLives();


    }

    // Update is called once per frame
    void Update()
    {

        Easy = GameControl.control.Easy;
        Normal = GameControl.control.Normal;
        Hard = GameControl.control.Hard;

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

        if (TakeDamage)
        {
            if (MissileDamage)
            {
                shakeAmount = 2.0f;
                shakeDuration += 0.5f;
                MissileDamage = false;
            }
            else
            {
                shakeAmount = 0.7f;
                shakeDuration += 0.2f;
            }
            TakeDamage = false;
        }

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }

        else if (shakeDuration < 0)
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;

        }

        if (PlayerDamaged)
        {

            if (Easy)
            {
                Shield -= 50 * Time.deltaTime;
                PlayerDamaged = false;
            }
            else if (Normal)
            {
                Shield -= 75 * Time.deltaTime;
                PlayerDamaged = false;
            }
            else if (Hard)
            {
                Shield -= 100 * Time.deltaTime;
                PlayerDamaged = false;
            }

            SetCountShield();
        }
        if (Lava || Slime)
        {
            Shield -= 1 * Time.deltaTime;
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            SetCountShield();
        }

        if (Shield <= 0)
        {
            gameObject.transform.position = LastCheckPoint;
            Lives -= 1;
            Shield = 100;
            SetCountLives();
            SetCountShield();
        }

        if (Lives <= 0)
        {
            Lives = 0;
            SetCountLives();
        }

        if (redScreenFlashEnabled == true)
        {
            DamageScreen.SetActive(true);
            redScreenFlashTimer -= Time.deltaTime;
        }

        if (redScreenFlashTimer <= 0)
        {
            DamageScreen.SetActive(false);
            redScreenFlashEnabled = false;
            redScreenFlashTimer = redScreenFlashTimerStart;
        }
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BeatLevel"))
        {

        }
        if (other.gameObject.CompareTag("Shield"))
        {
            audioSrc.PlayOneShot(ShieldPickup, 0.7f);
            UIText.text = "Shield +25";
            Shield += 25f;
            FadeTime += 5;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("NearLava"))
        {
            NormalLight.SetActive(false);
            LavaLight.SetActive(true);
            LavaScreen.SetActive(true);
        }
        if (other.gameObject.CompareTag("Lava"))
        {
            audioSrc.clip = LavaDamage;
            audioSrc.Play();
            audioSrc.loop = true;
            Lava = true;
            
        }
        if (other.gameObject.CompareTag("NearSlime"))
        {
            NormalLight.SetActive(false);
            SlimeLight.SetActive(true);
            SlimeScreen.SetActive(true);
        }
        if (other.gameObject.CompareTag("Slime"))
        {
            audioSrc.clip = SlimeDamage;
            audioSrc.Play();
            audioSrc.loop = true;
            Slime = true;

        }
        if (other.gameObject.CompareTag("Life"))
        {
            Lives += 1;
            other.gameObject.SetActive(false);
        }
        SetCountShield();
        SetCountLives();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NearLava"))
        {
            LavaScreen.SetActive(false);
            NormalLight.SetActive(true);
            LavaLight.SetActive(false);
        }
        if (other.gameObject.CompareTag("Lava"))
        {
            audioSrc.clip = LavaDamage;
            audioSrc.loop = false;
            audioSrc.Stop();
            Lava = false;
            
        }
        if (other.gameObject.CompareTag("NearSlime"))
        {
            SlimeScreen.SetActive(false);
            NormalLight.SetActive(true);
            SlimeLight.SetActive(false);
        }
        if (other.gameObject.CompareTag("Slime"))
        {
            audioSrc.clip = SlimeDamage;
            audioSrc.Play();
            audioSrc.loop = false;
            Slime = false;

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            UIText.text = "CheckPoint";
            FadeTime += 5;
            LastCheckPoint = gameObject.transform.position;
            other.gameObject.SetActive(false);

        }
        if (other.gameObject.CompareTag("DrillEnemy"))
        {
           
            redScreenFlashEnabled = true;
            audioSrc.PlayOneShot(EnemyDrillSFX, 0.7f);
            PlayerDamaged = true;
            TakeDamage = true;
        }
        if (other.gameObject.CompareTag("IonMinerEnemy"))
        {
           
            redScreenFlashEnabled = true;
            audioSrc.PlayOneShot(PlayerHitSFX, 1f);
            PlayerDamaged = true;
            TakeDamage = true;
        }
        if (other.gameObject.CompareTag("EnemyLaserLVL1"))
        {
           
            redScreenFlashEnabled = true;
            audioSrc.PlayOneShot(LaserImpact, 1f);
            if (Easy)
            {
                Shield -= 1;
            }
            else if (Normal)
            {
                Shield -= 2;
            }
            else if (Hard)
            {
                Shield -= 3;
            }
            TakeDamage = true;
            SetCountShield();

        }
        if (other.gameObject.CompareTag("EnemyLaserLVL2"))
        {
           
            redScreenFlashEnabled = true;
            audioSrc.PlayOneShot(LaserImpact, 1f);
            if (Easy)
            {
                Shield -= 2;
            }
            else if (Normal)
            {
                Shield -= 3;
            }
            else if (Hard)
            {
                Shield -= 4;
            }
            TakeDamage = true;
            SetCountShield();
        }
        if (other.gameObject.CompareTag("EnemyLaserLVL3"))
        {
          
            redScreenFlashEnabled = true;
            audioSrc.PlayOneShot(LaserImpact, 1f);
            if (Easy)
            {
                Shield -= 4;
            }
            else if (Normal)
            {
                Shield -= 5;

            }
            else if (Hard)
            {
                Shield -= 6;

            }
            TakeDamage = true;
            SetCountShield();
        }
        if (other.gameObject.CompareTag("EnemyLaserLVL4"))
        {
          
            redScreenFlashEnabled = true;
            audioSrc.PlayOneShot(LaserImpact, 1f);
            if (Easy)
            {
                Shield -= 6;

            }
            else if (Normal)
            {
                Shield -= 7;

            }
            else if (Hard)
            {
                Shield -= 8;

            }
            TakeDamage = true;
            SetCountShield();
        }
        if (other.gameObject.CompareTag("EnemyMissile"))
        {
          
            redScreenFlashEnabled = true;
            audioSrc.PlayOneShot(MissileImpact, 0.5f);
            if (Easy)
            {
                Shield -= 5;

            }
            else if (Normal)
            {
                Shield -= 10;

            }
            else if (Hard)
            {
                Shield -= 15;

            }
            MissileDamage = true;
            TakeDamage = true;
            SetCountShield();
        }
        if (other.gameObject.CompareTag("Gauss"))
        {
          
            redScreenFlashEnabled = true;
            audioSrc.PlayOneShot(GaussImpact, 0.7f);
            if (Easy)
            {
                Shield -= 2;

            }
            else if (Normal)
            {
                Shield -= 4;

            }
            else if (Hard)
            {
                Shield -= 6;

            }
            TakeDamage = true;
            SetCountShield();
        }
        if (other.gameObject.CompareTag("EnemyPlasma"))
        {

            redScreenFlashEnabled = true;
            audioSrc.PlayOneShot(LaserImpact, 1f);
            if (Easy)
            {
                Shield -= 6;

            }
            else if (Normal)
            {
                Shield -= 7;

            }
            else if (Hard)
            {
                Shield -= 8;

            }
            TakeDamage = true;
        }
        SetCountShield();
    }
 
    void SetCountShield()
    {
        ShieldText.text = Mathf.CeilToInt(Shield).ToString();
    }
    void SetCountLives()
    {
        LivesText.text = Lives.ToString();
    }
}
