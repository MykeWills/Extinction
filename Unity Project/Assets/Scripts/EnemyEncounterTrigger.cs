using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounterTrigger : MonoBehaviour {

    Vector3 normalPosition;
    Vector3 playerPosition;
    public float SoundRange;
    GameObject Player;
    bool StopCount;

    public AudioSource audioSrc;
    public AudioClip EnemyEncounterSound;
    float SoundTimer = 0.0f;
    bool PlaySound;
    private void Start()
    {
        normalPosition = gameObject.transform.position;
        audioSrc.clip = EnemyEncounterSound;
    }

    private void Update()
    {
        StartCoroutine(FindPlayer());
        if (SoundTimer > 0)
        {
            PlaySound = false;
            SoundTimer -= Time.deltaTime;
        }
        if (SoundTimer <= 0)
        {
            SoundTimer = 0;
            PlaySound = true;
            StopCount = false;

        }

        if (Vector3.Magnitude(normalPosition - playerPosition) < SoundRange && !StopCount)
        {
            audioSrc.Play();
            SoundTimer += 20;
            StopCount = true;
        }

    }
    IEnumerator FindPlayer()
    {
        Player = GameObject.Find("/MainPlayer/Player");
        if (Player == null)
        {
            yield return null;
        }
        else
        {
            Player = GameObject.Find("/MainPlayer/Player");
            playerPosition = Player.transform.position;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(gameObject.transform.position, SoundRange);
    }
}
