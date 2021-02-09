using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeFanScript : MonoBehaviour {

    public float fanSpeed;
    public bool active;
    public bool destructable;
    public AudioSource audio;
    public AudioClip hit;
    public AudioClip stop;
    public ParticleSystem sparks;
    public GameObject fanBox;

    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;
    public GameObject trigger4;

    // Use this for initialization
    void Start () {
        active = true;
        //audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * fanSpeed);
        }

        if (!destructable)
        {
            if (!trigger1.active && !trigger2.active && !trigger3.active && !trigger4.active && active)
            {
                active = false;
                fanBox.GetComponent<Collider>().enabled = false;
                audio.PlayOneShot(stop, 0.7F);
                sparks.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destructable)
        {
            if (other.gameObject.CompareTag("Laser") || other.gameObject.CompareTag("Missle"))
            {
                audio.PlayOneShot(hit, 0.7F);
                //active = false;
                gameObject.SetActive(false);
                sparks.Play();
            }
        }
    }
}
