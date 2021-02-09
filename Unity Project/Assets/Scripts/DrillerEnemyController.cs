using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DrillerEnemyController : MonoBehaviour
{
    [Header("Idle Movement")]
    Vector3 newPosition;
    public Transform BodyDriller;
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public Transform position4;
    public string State;
    public float smooth;
    public float resetTime;

    [Header("Audio")]
    AudioSource audioSrc;
    public AudioClip LaserHit;
    public AudioClip GaussHit;
    [HideInInspector]
    Vector3 PlayerPosition;
    [HideInInspector]
    Vector3 normalPosition;

    [Header("DrillerSettings")]
    GameObject Player;
    public GameObject DrillerBody;
    public GameObject DrillerObject;
    public float stoppingDistance;
    public float speed = 10.0f;
    public float awarenessRange;
    public float enemyRadius;
    public int EnemyHealth;
    public float ExplosionTime = 3.0f;

    [Header("Prefabs")]
    public GameObject explosionPrefab;
    public GameObject energyPrefab;
    public GameObject shieldPrefab;

    int RandomNumber;
    int RandomPosition;
    bool Easy;
    bool Normal;
    bool Hard;
    bool idle;
    public bool RandomizeMovement;

    public static int Score ;

    // Use this for initialization
    void Start()
    {
        MoveToIdle();
        audioSrc = GetComponent<AudioSource>();
        normalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForPlayer());
        Easy = GameControl.control.Easy;
        Normal = GameControl.control.Normal;
        Hard = GameControl.control.Hard;
        RandomPosition = Random.Range(1, 4);

        // Move the Enemy To the Player
        if (Vector3.Magnitude(normalPosition - PlayerPosition) < awarenessRange)
        {
            MoveToPlayer();
        }
        else
        {
            idle = true;
        }

        if (EnemyHealth <= 0)
        {
            Score += 300;
            EnemyHealth = 0;
            Explode();
            Destroy(DrillerObject);
        }

       
    }
    void FixedUpdate()
    {
        if (idle)
        {
            BodyDriller.position = Vector3.Lerp(BodyDriller.position, newPosition, smooth * Time.deltaTime);
        }
    }
    IEnumerator WaitForPlayer()
    {
        Player = GameObject.Find("/MainPlayer/Player/");
        if (Player == null)
        {
            yield return null;
        }
        else
        {
            Player = GameObject.Find("/MainPlayer/Player");
            PlayerPosition = Player.gameObject.transform.position;
        }
    }
    void OnCollisionEnter(Collision other)
    {
         if (other.gameObject.CompareTag("LaserLVL1"))
        {
            audioSrc.clip = LaserHit;
            audioSrc.Play();
            EnemyHealth -= 1;
        }
        if (other.gameObject.CompareTag("LaserLVL2"))
        {
            audioSrc.clip = LaserHit;
            audioSrc.Play();
            EnemyHealth -= 2;
        }
        if (other.gameObject.CompareTag("LaserLVL3"))
        {
            audioSrc.clip = LaserHit;
            audioSrc.Play();
            EnemyHealth -= 3;
        }
        if (other.gameObject.CompareTag("LaserLVL4"))
        {
            audioSrc.clip = LaserHit;
            audioSrc.Play();
            EnemyHealth -= 4;
        }
        if (other.gameObject.CompareTag("Missle"))
        {
            audioSrc.clip = LaserHit;
            audioSrc.Play();
            EnemyHealth -= 10;
        }
        if (other.gameObject.CompareTag("Gauss"))
        {
            audioSrc.clip = GaussHit;
            audioSrc.Play();
            EnemyHealth -= 3;
        }
    }
    public void MoveToPlayer()
    {
        idle = false;
        transform.LookAt(Player.transform);
        if (Vector3.Magnitude(transform.position - PlayerPosition) <= stoppingDistance)
        {
            var lookRot = Quaternion.LookRotation(PlayerPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position = transform.position;
        }
        else
        {
            var lookRot = Quaternion.LookRotation(PlayerPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
    public void MoveToIdle()
    {
        if (RandomizeMovement)
        {
            if (RandomPosition == 1)
            {
                RandomPosition = Random.Range(1, 4);
                State = "Moving To Position 2";
                newPosition = position2.position;
                transform.LookAt(position2.transform);
            }
            else if (RandomPosition == 2)
            {
                RandomPosition = Random.Range(1, 4);
                State = "Moving To Position 3";
                newPosition = position3.position;
                transform.LookAt(position3.transform);
            }
            else if (RandomPosition == 3)
            {
                RandomPosition = Random.Range(1, 4);
                State = "Moving To Position 4";
                newPosition = position4.position;
                transform.LookAt(position4.transform);
            }
            else if (RandomPosition == 4)
            {
                RandomPosition = Random.Range(1, 4);
                State = "Moving To Position 1";
                newPosition = position1.position;
                transform.LookAt(position1.transform);
            }
            else if (RandomPosition == 0)
            {
                State = "Moving To Position 1";
                RandomPosition = Random.Range(1, 4);
                newPosition = position1.position;
                transform.LookAt(position1.transform);
            }
            Invoke("MoveToIdle", resetTime);
        }
        else
        {
            if (RandomPosition == 1)
            {
                RandomPosition = 2 ;
                State = "Moving To Position 2";
                newPosition = position2.position;
                transform.LookAt(position2.transform);
            }
            else if (RandomPosition == 2)
            {
                RandomPosition = 3;
                State = "Moving To Position 3";
                newPosition = position3.position;
                transform.LookAt(position3.transform);
            }
            else if (RandomPosition == 3)
            {
                RandomPosition = 4;
                State = "Moving To Position 4";
                newPosition = position4.position;
                transform.LookAt(position4.transform);
            }
            else if (RandomPosition == 4)
            {
                RandomPosition = 1;
                State = "Moving To Position 1";
                newPosition = position1.position;
                transform.LookAt(position1.transform);
            }
            else if (RandomPosition == 0)
            {
                State = "Moving To Position 1";
                RandomPosition = 1;
                newPosition = position1.position;
                transform.LookAt(position1.transform);
            }
            Invoke("MoveToIdle", resetTime);
        }
       
    }

    void Explode()
    {
        for (int i = 0; i < 1; i++)
        {
            RandomNumber = Random.Range(1, 15);
            
            if (RandomNumber == 1)
            {
                Instantiate(energyPrefab, DrillerBody.transform.position, Quaternion.identity);
            }
            else if (RandomNumber == 3)
            {
                Instantiate(shieldPrefab, DrillerBody.transform.position, Quaternion.identity);
            }
        }
        
        Instantiate(explosionPrefab, DrillerBody.transform.position, Quaternion.identity);
    }

  
    void OnDrawGizmosSelected()
    {  
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(DrillerBody.transform.position, awarenessRange);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(DrillerBody.transform.position, enemyRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(DrillerBody.transform.position, stoppingDistance);
    }

}
