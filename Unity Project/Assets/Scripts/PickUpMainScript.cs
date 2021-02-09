using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMainScript : MonoBehaviour {

    //public GameObject player;

    bool playerInTerritory;

    //public bool InRange;

    public float RespawnCountdown;

    public float RespawnTime;

    public GameObject Object;

    public AudioSource audio;
    public AudioClip collect;
    public ParticleSystem sparks;

    // Use this for initialization
    void Start()
    {

        RespawnCountdown = 0f;
        //player = GameObject.FindGameObjectWithTag("Player");
        playerInTerritory = false;

    }

    // Update is called once per frame
    void Update()
    {


        if (RespawnCountdown > 0)
        {
            RespawnCountdown -= Time.deltaTime;
        }
        if (RespawnCountdown < 0)
        {
            RespawnCountdown = 0;
            Object.SetActive(true);
        }
        if (RespawnCountdown >= RespawnTime)
        {
            RespawnCountdown = RespawnTime;
        }
        if (playerInTerritory == true && RespawnCountdown == 0)
        {
            RespawnCountdown += RespawnTime;
            Object.SetActive(false);
            playerInTerritory = false;

            audio.PlayOneShot(collect, 0.4F);
            sparks.Play();
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && RespawnCountdown == 0)
        {
            playerInTerritory = true;
            //InRange = true;
        }
    }
    /*
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InRange = false;
        }
    }
    */
}
