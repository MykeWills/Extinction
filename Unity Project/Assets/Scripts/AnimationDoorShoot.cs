using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationDoorShoot : MonoBehaviour {

    Animator animator;
    public GameObject DoorObject;
    public bool doorOpen;

    public AudioSource audioSrc;
    public AudioClip DoorOpenSFX;
    public float Timer;
    public float TimeToClose;

    public float FadeTime;
    public float FadeSpeed = 1f;

    public BoxCollider DoorColliderShoot;
    public CapsuleCollider DoorColliderRange;
    Text DoorText;

    public static bool RedKey;
    public static bool BlueKey;
    public static bool YellowKey;

    public bool RedDoorText;
    public bool BlueDoorText;
    public bool YellowDoorText;

    public bool BossDead;

    public bool RedDoor;
    public bool BlueDoor;
    public bool YellowDoor;

    public bool NormalDoor;
    public bool ExitDoor;


    void Start()
    {
        BlueDoorText = true;
        YellowDoorText = true;
        RedDoorText = true;
        BossDead = false;
        doorOpen = false;
        StartCoroutine(WaitForText());
        animator = DoorObject.GetComponent<Animator>();
    }
    private void Update()
    {

        StartCoroutine(WaitForText());
        BossDead = AfterBossSceneLoading.BossDead;
        BlueKey = KeyScript.BlueKey;
        YellowKey = KeyScript.YellowKey;
        RedKey = KeyScript.RedKey;

        if (FadeTime > 0)
        {
            FadeTime -= Time.deltaTime * FadeSpeed;
        }

        if (FadeTime < 0)
        {
            FadeTime = 0;
            DoorText.text = "";
        }

        if (FadeTime >= 5)
        {
            FadeTime = 5;
        }

        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        if(Timer > TimeToClose)
        {
            Timer = TimeToClose;
        }
        if (Timer < 0 && doorOpen)
        {
            
            Timer = 0;
            doorOpen = false;
            DoorControl("Close");
            DoorColliderShoot.enabled = true;
            DoorColliderRange.enabled = true;
        }
    }
    IEnumerator WaitForText()
    {
        yield return new WaitForSeconds(3);
        DoorText = GameObject.Find("/MainPlayer/MainGame Canvas/UI/Difficulty").GetComponent<Text>();
        if (DoorText == null)
        {
            yield return null;
        }
        else
        {
            DoorText = GameObject.Find("/MainPlayer/MainGame Canvas/UI/Difficulty").GetComponent<Text>();
        }
    }
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player" || col.gameObject.CompareTag("LaserLVL1") || col.gameObject.CompareTag("LaserLVL2") || col.gameObject.CompareTag("LaserLVL3") || col.gameObject.CompareTag("LaserLVL4") || col.gameObject.CompareTag("Missle") || col.gameObject.CompareTag("Gauss"))
        {
            if (BlueDoor)
            {
                if (BlueKey)
                {
                    if (BlueDoorText)
                    {
                        DoorText.text = "Blue Access Granted!";
                        FadeTime += 5;
                        BlueDoorText = false;
                    }
                    doorOpen = true;
                    DoorControl("Open");
                    DoorColliderShoot.enabled = false;
                    DoorColliderRange.enabled = false;
                  
                }
                else
                {
                    DoorText.text = "Blue Access Denied!";
                    FadeTime += 5;
                }

            }
            else if (YellowDoor)
            {
                if (YellowKey)
                {
                    DoorText.text = "Yellow Access Granted!";
                    doorOpen = true;
                    DoorControl("Open");
                    DoorColliderShoot.enabled = false;
                    DoorColliderRange.enabled = false;
                    FadeTime += 5;
                }
                else
                {
                    DoorText.text = "Yellow Access Denied!";
                    FadeTime += 5;
                }

            }
            else if (RedDoor)
            {
                if (RedKey)
                {
                    DoorText.text = "Red Access Granted!";
                    doorOpen = true;
                    DoorControl("Open");
                    DoorColliderShoot.enabled = false;
                    DoorColliderRange.enabled = false;
                    FadeTime += 5;
                }
                else
                {
                    DoorText.text = "Red Access Denied!";
                    FadeTime += 5;
                }

            }
            else if (ExitDoor)
            {
                if (BossDead)
                {
                    doorOpen = true;
                    DoorControl("Open");
                    DoorColliderShoot.enabled = false;
                    DoorColliderRange.enabled = false;
                    FadeTime += 5;
                }
                else
                {
                    DoorText.text = "This is Exit Only!";
                    FadeTime += 5;
                }
            }
            else if (NormalDoor)
            {
                doorOpen = true;
                DoorControl("Open");
                DoorColliderShoot.enabled = false;
                DoorColliderRange.enabled = false;
            }
            if (doorOpen)
            {
                audioSrc.clip = DoorOpenSFX;
                audioSrc.Play();
                Timer += TimeToClose;
            }
        }
        if (col.gameObject.tag == "DrillerEnemy" || col.gameObject.tag == "IonMinerEnemy" || col.gameObject.tag == "Enemy" || col.gameObject.CompareTag("EnemyLaserLVL1") || col.gameObject.CompareTag("EnemyLaserLVL2") || col.gameObject.CompareTag("EnemyLaserLVL3") || col.gameObject.CompareTag("EnemyLaserLVL4")&& NormalDoor)
        {
            doorOpen = true;
            DoorControl("Open");
            DoorColliderShoot.enabled = false;
            DoorColliderRange.enabled = false;

            if (doorOpen)
            {
                audioSrc.clip = DoorOpenSFX;
                audioSrc.Play();
                Timer += TimeToClose;
            }
        }
    }

    void DoorControl(string direction)
    {
        animator.SetTrigger(direction);
    }


   
}



