using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRefillTrigger : MonoBehaviour {


    public AudioSource audioSrc;
    public AudioClip RefillSFX;
    GameObject Player;

    float PlayerEnergy;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(FindPlayer());
        PlayerEnergy = GameControl.control.Energy;

        if (PlayerEnergy >= 99)
        {
            audioSrc.clip = RefillSFX;
            audioSrc.Stop();
        }
    }
    IEnumerator FindPlayer()
    {
        Player = GameObject.Find("/MainPlayer/Player/");
        if (Player == null)
        {
            yield return null;
        }
        else
        {
            Player = GameObject.Find("/MainPlayer/Player/");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
    
            if (PlayerEnergy < 99)
            {
                audioSrc.clip = RefillSFX;
                audioSrc.Play();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            audioSrc.clip = RefillSFX;
            audioSrc.Stop();
        }
        
    }
}
