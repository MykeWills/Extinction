using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationDoorRange : MonoBehaviour
{

    Animator animator;
    public bool doorOpen;
    public AudioSource audioSrc;
    public AudioClip DoorOpenSFX;
    public BoxCollider DoorShootCollider;
    public float FadeTime;
    public float FadeSpeed = 1f;

    Text DoorText;

    public static bool RedKey;
    public static bool BlueKey;
    public static bool YellowKey;
    public bool BossDead;

    public bool RedDoor;
    public bool BlueDoor;
    public bool YellowDoor;

    public bool RedDoorText;
    public bool BlueDoorText;
    public bool YellowDoorText;

    public bool NormalDoor;
    public bool ExitDoor;

    void Start()
    {
        StartCoroutine(WaitForText());
        doorOpen = false;
        BlueDoorText = true;
        YellowDoorText = true;
        RedDoorText = true;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        StartCoroutine(WaitForText());
        BlueKey = KeyScript.BlueKey;
        YellowKey = KeyScript.YellowKey;
        RedKey = KeyScript.RedKey;
        BossDead = AfterBossSceneLoading.BossDead;

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

    void OnTriggerEnter(Collider other)
    {
        //==============================================WhenPlayerEntersDoorField=======================//
        if (other.gameObject.CompareTag("Player"))
        {
            if (BlueDoor)
            {
                if (BlueKey)
                {
                    DoorText.text = "Blue Access Granted!";
                    doorOpen = true;
                    DoorControl("Open");
                    DoorShootCollider.enabled = false;
                    FadeTime += 5;
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
                    DoorShootCollider.enabled = false;
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
                    DoorShootCollider.enabled = false;
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
                    DoorShootCollider.enabled = false;
                    FadeTime += 5;
                }
                else
                {
                    //DoorText.text = "Exit Only, Defeat Level Boss!";
                    FadeTime += 5;
                }
            }
            else if (NormalDoor)
            {
                doorOpen = true;
                DoorControl("Open");
                DoorShootCollider.enabled = false;
            }
            if (doorOpen)
            {
                audioSrc.clip = DoorOpenSFX;
                audioSrc.Play();
            }
        }

        if (other.gameObject.tag == "DrillerEnemy" || other.gameObject.tag == "IonMinerEnemy" || other.gameObject.tag == "Enemy" || other.gameObject.CompareTag("EnemyLaserLVL1") || other.gameObject.CompareTag("EnemyLaserLVL2") || other.gameObject.CompareTag("EnemyLaserLVL3") || other.gameObject.CompareTag("EnemyLaserLVL4") && NormalDoor)
        {
            doorOpen = true;
            DoorControl("Open");
            DoorShootCollider.enabled = false;

            if (doorOpen)
            {
                audioSrc.clip = DoorOpenSFX;
                audioSrc.Play();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        //==============================================WhenPlayerExitsDoorField=======================//
        if (other.gameObject.CompareTag("Player"))
        {
            if (BlueDoor)
            {
                if (BlueKey)
                {
                    doorOpen = false;
                    DoorControl("Close");
                    DoorShootCollider.enabled = true;
                }
            }
            else if (YellowDoor)
            {
                if (YellowKey)
                {
                    doorOpen = false;
                    DoorControl("Close");
                    DoorShootCollider.enabled = true;
                }
            }
            else if (RedDoor)
            {
                if (RedKey)
                {
                    doorOpen = false;
                    DoorControl("Close");
                    DoorShootCollider.enabled = true;
                }
            }
            else if (ExitDoor)
            {
                if (BossDead)
                {
                    doorOpen = false;
                    DoorControl("Close");
                    DoorShootCollider.enabled = true;
                }
            }
            else if (NormalDoor)
            {
                doorOpen = false;
                DoorControl("Close");
                DoorShootCollider.enabled = true;
            }
        }
    }
    void DoorControl(string direction)
    {
        animator.SetTrigger(direction);
    }
}



