using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class JonNewBossController : MonoBehaviour
{
    [Header("Idle Movement")]
    Vector3 newPosition;
    Vector3 PlayerCollision;
    public Transform BodyJonBoss;
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
    public AudioClip Shoot;
    Vector3 PlayerPosition;
    Vector3 normalPosition;
    [Header("JonBossSettings")]
    GameObject Player;
    public GameObject JonBossBody;
    Text BossHealthText;
    public GameObject JonBossObject;
    public float speed;
    public float stoppingDistance;
    public float awarenessRange;
    public float attackRange;
    public float enemyRadius;
    public int EnemyHealth;
    public float ExplosionTime = 3.0f;
    float lastShot;
    public float fireRate;
    private float projectileForce;
    [Header("Prefabs")]
    public GameObject explosionPrefab;
    public GameObject EnemyRocket;
    GameObject Projectile;

    int RandomNumber;
    int RandomPosition;
    bool Easy;
    bool Normal;
    bool Hard;
    bool idle;
    public bool shootLeft;
    public bool shootRight;
    public bool RandomizeMovement;

    public Transform[] bulletSpawners;

    public static int BossCounter;

    public static int Score;

    void Start()
    {
        MoveToIdle();
        audioSrc = GetComponent<AudioSource>();
        normalPosition = gameObject.transform.position;
        //speed = JonBossNav.GetComponent<NavMeshAgent>().speed;
        shootLeft = true;
        shootRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForPlayer());
        StartCoroutine(WaitForText());
        Easy = GameControl.control.Easy;
        Normal = GameControl.control.Normal;
        Hard = GameControl.control.Hard;
        RandomPosition = Random.Range(1, 4);

        if (Vector3.Magnitude(transform.position - PlayerPosition) < attackRange && Time.time > lastShot + fireRate)
        {
            // Difficulty
            if (Easy)
            {
                fireRate = .5f;
                projectileForce = 750;
                
            }
            else if (Normal)
            {
                fireRate = 1f;
                projectileForce = 1000;
                
            }
            else if (Hard)
            {
                fireRate = .5f;
                projectileForce = 1500;
                
            }
            if (shootLeft)
            {
                shoot();
                shootLeft = false;
                shootRight = true;
            }
            else if (shootRight)
            {
                shoot();
                shootRight = false;
                shootLeft = true;
            }
        }
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
            BossCounter++;
            Score +=500;
            EnemyHealth = 0;
            SetCountHealth();
            BossHealthText.text = "";
            Explode();

            Destroy(JonBossObject);
        }
    }
    void FixedUpdate()
    {
        if (idle)
        {
            BodyJonBoss.position = Vector3.Lerp(BodyJonBoss.position, newPosition, smooth * Time.deltaTime);
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
    IEnumerator WaitForText()
    {
        BossHealthText= GameObject.Find("/MainPlayer/MainGame Canvas/UI/Difficulty/").GetComponent<Text>();
        if (BossHealthText == null)
        {
            yield return null;
        }
        else
        {
            BossHealthText = GameObject.Find("/MainPlayer/MainGame Canvas/UI/Difficulty/").GetComponent<Text>();
           
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
        if (other.gameObject.CompareTag("IonMinerEnemy") || other.gameObject.CompareTag("DrillEnemy") || other.gameObject.CompareTag("BombardierEnemy"))
        {
            if (Vector3.Magnitude(transform.position - other.gameObject.transform.position) <= enemyRadius)
            {
                var lookRot = Quaternion.LookRotation(other.gameObject.transform.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
                transform.position = transform.position;
            }
        }
        SetCountHealth();
    }
    public void shoot()
    {
        Projectile = EnemyRocket;
        for (int i = 0; i < bulletSpawners.Length; i++)
        {
            audioSrc.clip = Shoot;
            audioSrc.PlayOneShot(Shoot, 0.3f);
            Rigidbody Temporary_RigidBody;
            GameObject newBullet;
            if (shootLeft)
            {
                newBullet = Instantiate(Projectile, bulletSpawners[0].position, bulletSpawners[i].rotation);
                newBullet.transform.LookAt(PlayerPosition);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                Destroy(newBullet, 10.0f);
       

            }
            else if (shootRight)
            {
                newBullet = Instantiate(Projectile, bulletSpawners[1].position, bulletSpawners[i].rotation);
                newBullet.transform.LookAt(PlayerPosition);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                Destroy(newBullet, 10.0f);
            }
        }
        lastShot = Time.time;
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
            //JonBossNav.GetComponent<NavMeshAgent>().destination = PlayerPosition;
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
                RandomPosition = 2;
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
        Instantiate(explosionPrefab, JonBossBody.transform.position, Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(JonBossBody.transform.position, awarenessRange);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(JonBossBody.transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(JonBossBody.transform.position, stoppingDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(JonBossBody.transform.position, enemyRadius);
    }
    void SetCountHealth()
    {
        BossHealthText.text = "Terminator " + EnemyHealth.ToString();
    }
}
