using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class OmegaController : MonoBehaviour
{
    [Header("Idle Movement")]
    Vector3 newPosition;
    public Transform BodyOmega;
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
    public AudioClip LaserShoot;
    public AudioClip MissileShoot;
    public AudioClip PlasmaShoot;
    public AudioClip TeleportSound;
    Vector3 PlayerPosition;
    Vector3 normalPosition;
    [Header("OmegaSettings")]
    GameObject Player;
    public GameObject OmegaBody;
    public GameObject OmegaObject;
    public float speed = 25.0f;
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
    public GameObject BossEnemy;
    public GameObject EnemyMissile;
    public GameObject EnemyLaser;
    public GameObject EnemyPlasma;
    public GameObject explosionPrefab;
    public GameObject energyPrefab;
    public GameObject shieldPrefab;
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

    public GameObject TeleportPosition1;
    public GameObject TeleportPosition2;
    public GameObject TeleportPosition3;
    public GameObject TeleportPosition4;

    public bool Teleport1;
    public bool Teleport2;
    public bool Teleport3;
    public bool Teleport4;

    public float TeleportTimer;
    public float TeleportTime;
    public float TeleportSpeed = 1.0f;
    
    public bool Laser;
    public bool MissileLeft;
    public bool MissileRight;
    public bool PlasmaCenter;

    public static int BossCounter;

    public static int Score;


    // Use this for initialization
    void Start()
    {

        MoveToIdle();
        audioSrc = GetComponent<AudioSource>();
        normalPosition = gameObject.transform.position;
        Laser = true;
        MissileLeft = false;
        MissileRight = false;
        PlasmaCenter = false;
        Teleport1 = true;
        Teleport2 = false;
        Teleport3 = false;
        Teleport4 = false;
        TeleportTimer = TeleportTime;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForPlayer());
        Easy = GameControl.control.Easy;
        Normal = GameControl.control.Normal;
        Hard = GameControl.control.Hard;
        RandomPosition = Random.Range(1, 4);

        if (Vector3.Magnitude(transform.position - PlayerPosition) < attackRange && Time.time > lastShot + fireRate)
        {
            //=============================ShootingProjectiles============================//
            if (Laser)
            {
                if (Easy)
                {
                    TeleportSpeed = 1f;
                    fireRate = 3f;
                    projectileForce = 400;
                }
                else if (Normal)
                {
                    TeleportSpeed = 2f;
                    fireRate = 2f;
                    projectileForce = 600;

                }
                else if (Hard)
                {
                    TeleportSpeed = 3f;
                    fireRate = 1f;
                    projectileForce = 800;

                }
                shoot();
                Laser = false;
                MissileLeft = true;
            }
            else if (MissileLeft)
            {
                if (Easy)
                {
                    TeleportSpeed = 1f;
                    fireRate = 3f;
                    projectileForce = 3500;
                }
                else if (Normal)
                {
                    TeleportSpeed = 2f;
                    fireRate = 2f;
                    projectileForce = 4000;

                }
                else if (Hard)
                {
                    TeleportSpeed = 3f;
                    fireRate = 1f;
                    projectileForce = 4500;

                }
                shoot();
                MissileLeft = false;
                MissileRight = true;
            }
            else if (MissileRight)
            {
                if (Easy)
                {
                    TeleportSpeed = 1f;
                    fireRate = 3f;
                    projectileForce = 3500;
                }
                else if (Normal)
                {
                    TeleportSpeed = 2f;
                    fireRate = 2f;
                    projectileForce = 4000;

                }
                else if (Hard)
                {
                    TeleportSpeed = 3f;
                    fireRate = 1f;
                    projectileForce = 4500;

                }
                shoot();
                MissileRight = false;
                PlasmaCenter = true;
            }
            else if (PlasmaCenter)
            {
                if (Easy)
                {
                    TeleportSpeed = 1f;
                    fireRate = 3f;
                    projectileForce = 400;
                }
                else if (Normal)
                {
                    TeleportSpeed = 2f;
                    fireRate = 2f;
                    projectileForce = 450;

                }
                else if (Hard)
                {
                    TeleportSpeed = 3f;
                    fireRate = 1f;
                    projectileForce = 500;

                }
                shoot();
                PlasmaCenter = false;
                Laser = true;
            }
        }
        //================================TeleportTimer====================================//

        if (TeleportTimer > 0)
        {
            TeleportTimer -= Time.deltaTime * TeleportSpeed;
        }
        if (TeleportTimer < 0)
        {
            if (Teleport1)
            {
                audioSrc.clip = TeleportSound;
                audioSrc.Play();
                gameObject.transform.position = TeleportPosition4.transform.position;
                Teleport1 = false;
                Teleport2 = true;
                TeleportTimer = TeleportTime;
            }
            else if (Teleport2)
            {
                audioSrc.clip = TeleportSound;
                audioSrc.Play();
                gameObject.transform.position = TeleportPosition4.transform.position;
                Teleport2 = false;
                Teleport3 = true;
                TeleportTimer = TeleportTime;
            }
            else if (Teleport3)
            {
                audioSrc.clip = TeleportSound;
                audioSrc.Play();
                gameObject.transform.position = TeleportPosition4.transform.position;
                Teleport3 = false;
                Teleport4 = true;
                TeleportTimer = TeleportTime;
            }
            else if (Teleport4)
            {
                audioSrc.clip = TeleportSound;
                audioSrc.Play();
                gameObject.transform.position = TeleportPosition4.transform.position;
                Teleport4 = false;
                Teleport1 = true;
                TeleportTimer = TeleportTime;
            }

        }

        //=============================MovetoPlayer==========================//
        if (Vector3.Magnitude(normalPosition - PlayerPosition) < awarenessRange)
        {
            MoveToPlayer();
        }
        else
        {
            idle = true;
        }
        // =================================Enemy Health==============================//

        if (EnemyHealth <= 0)
        {
            BossCounter++;
            Score +=5000;
            EnemyHealth = 0;
            Explode();
            Destroy(OmegaObject);
        }
    }
    void FixedUpdate()
    {
        if (idle)
        {
            BodyOmega.position = Vector3.Lerp(BodyOmega.position, newPosition, smooth * Time.deltaTime);
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
           
            audioSrc.PlayOneShot(LaserHit, 0.7f);
            EnemyHealth -= 1;
        }
        if (other.gameObject.CompareTag("LaserLVL2"))
        {
           
            audioSrc.PlayOneShot(LaserHit, 0.7f);
            EnemyHealth -= 2;
        }
        if (other.gameObject.CompareTag("LaserLVL3"))
        {
          
            audioSrc.PlayOneShot(LaserHit, 0.7f);
            EnemyHealth -= 3;
        }
        if (other.gameObject.CompareTag("LaserLVL4"))
        {
           
            audioSrc.PlayOneShot(LaserHit, 0.7f);
            EnemyHealth -= 4;
        }
        if (other.gameObject.CompareTag("Missle"))
        {
            audioSrc.PlayOneShot(LaserHit, 0.7f);
            EnemyHealth -= 10;
        }
        if (other.gameObject.CompareTag("Gauss"))
        {
            audioSrc.PlayOneShot(GaussHit, 0.7f);
            EnemyHealth -= 3;
        }
    }
    public void shoot()
    {
        for (int i = 0; i < bulletSpawners.Length; i++)
        {
            Rigidbody Temporary_RigidBody;
            GameObject newBullet;

            if (Laser)
            {
                Projectile = EnemyLaser;
                audioSrc.clip = LaserShoot;
                audioSrc.Play();
                newBullet = Instantiate(Projectile, bulletSpawners[0].position, bulletSpawners[i].rotation);
                newBullet.transform.LookAt(PlayerPosition);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                Destroy(newBullet, 10.0f);

                newBullet = Instantiate(Projectile, bulletSpawners[1].position, bulletSpawners[i].rotation);
                newBullet.transform.LookAt(PlayerPosition);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                Destroy(newBullet, 10.0f);
          
            }
            else if (MissileLeft)
            {
                Projectile = EnemyMissile;
                audioSrc.clip = MissileShoot;
                audioSrc.Play();
                newBullet = Instantiate(Projectile, bulletSpawners[2].position, bulletSpawners[i].rotation);
                newBullet.transform.LookAt(PlayerPosition);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                Destroy(newBullet, 10.0f);
               
            }
            else if (MissileRight)
            {
                Projectile = EnemyMissile;
                audioSrc.clip = MissileShoot;
                audioSrc.Play();
                newBullet = Instantiate(Projectile, bulletSpawners[3].position, bulletSpawners[i].rotation);
                newBullet.transform.LookAt(PlayerPosition);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                Destroy(newBullet, 10.0f);
            }
            else if (PlasmaCenter)
            {
                Projectile = EnemyPlasma;
                audioSrc.clip = PlasmaShoot;
                audioSrc.Play();
                newBullet = Instantiate(Projectile, bulletSpawners[4].position, bulletSpawners[i].rotation);
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
        for (int i = 0; i < 1; i++)
        {
            RandomNumber = Random.Range(1, 15);

            if (RandomNumber == 1)
            {
                Instantiate(energyPrefab, OmegaBody.transform.position, Quaternion.identity);
            }
            else if (RandomNumber == 3)
            {
                Instantiate(shieldPrefab, OmegaBody.transform.position, Quaternion.identity);
            }
        }
        Instantiate(explosionPrefab, OmegaBody.transform.position, Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(OmegaBody.transform.position, awarenessRange);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(OmegaBody.transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(OmegaBody.transform.position, stoppingDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(OmegaBody.transform.position, enemyRadius);
    }
}

