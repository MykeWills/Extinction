using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour {

    public AudioSource audioSrc;
    public AudioClip KeySFX;

    public static bool RedKey;
    public static bool YellowKey;
    public static bool BlueKey;

    public bool IHaveRedKey;
    public bool IHaveYellowKey;
    public bool IHaveBlueKey;

    Vector3 teleportPosition;
    GameObject Teleport;


    public Text UIText;
    public float FadeTime;
    public float FadeSpeed = 1f;



    // Use this for initialization
    void Start () {

        Teleport = GameObject.FindGameObjectWithTag("Teleporter");
        BlueKey = AnimationDoorShoot.BlueKey;
        YellowKey = AnimationDoorShoot.YellowKey;
        RedKey = AnimationDoorShoot.RedKey;

        BlueKey = false;
        YellowKey = false;
        RedKey = false;

        UIText.text = "";
	}
	
	// Update is called once per frame
	void Update () {


        StartCoroutine(WaitForTeleport());
        if (IHaveRedKey)
        {
            RedKey = true;
        }
        else
        {
            RedKey = false;
        }

        if (IHaveYellowKey)
        {
            YellowKey = true;
        }
        else
        {
            YellowKey = false;
        }

        if (IHaveBlueKey)
        {
            BlueKey = true;
        }
        else
        {
            BlueKey = false;
        }


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
    }
    IEnumerator WaitForTeleport()
    {

        Teleport = GameObject.FindGameObjectWithTag("Teleporter");
        if (Teleport == null)
        {
            yield return null;
        }
        else
        {
            Teleport = GameObject.FindGameObjectWithTag("Teleporter");
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BlueKey"))
        {
            audioSrc.PlayOneShot(KeySFX, 0.7f);
            UIText.text = "Blue Upgrade!";
            IHaveBlueKey = true;
            FadeTime += 5;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("YellowKey"))
        {
            audioSrc.PlayOneShot(KeySFX, 0.7f);
            UIText.text = "Yellow Upgrade!";
            IHaveYellowKey = true;
            FadeTime += 5;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("RedKey"))
        {
            audioSrc.PlayOneShot(KeySFX, 0.7f);
            UIText.text = "Red Upgrade!";
            IHaveRedKey = true;
            FadeTime += 5;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Teleport"))
        {
            gameObject.transform.position = Teleport.gameObject.transform.position;
        }
    }
}
