using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour {

    public bool active;
    public AudioSource audio;
    public AudioClip hit;
    public ParticleSystem sparks;

    public float maxHealth;
    public float health;

    public float gunDamage;
    public float missileDamage;

    // Use this for initialization
    void Start () {
        active = true;
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LaserLVL1") || other.gameObject.CompareTag("LaserLVL2") || other.gameObject.CompareTag("LaserLVL3") || other.gameObject.CompareTag("LaserLVL4") || other.gameObject.CompareTag("Gauss"))
        {
            audio.PlayOneShot(hit, 0.7F);
            health = (health - gunDamage);
            /*
            if (health <= 0)
            {
                OnKill();
            }
            /*
            active = false;
            gameObject.SetActive(false);
            sparks.Play();
            */
        }
        if (other.gameObject.CompareTag("Missle"))
        {
            audio.PlayOneShot(hit, 0.7F);
            health = (health - missileDamage);
        }
        if (health <= 0)
        {
            OnKill();
        }
    }

    void OnKill()
    {
        active = false;
        gameObject.SetActive(false);
        sparks.Play();
    }
}
