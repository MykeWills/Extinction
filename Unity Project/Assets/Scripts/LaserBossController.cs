using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class LaserBossController : MonoBehaviour
{
    [Header("Idle Movement")]
    Vector3 newPosition;
    public Transform BodyLaserBoss;
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
    public AudioClip PlasmaShoot;
    Vector3 PlayerPosition;
    Vector3 normalPosition;

    [Header("OmegaSettings")]
    GameObject Player;
    public GameObject LaserBossBody;
    public GameObject LaserBossObject;
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
    public GameObject EnemyLaser;
    public GameObject EnemyPlasma;
    public GameObject explosionPrefab;
    public GameObject smallBossPrefab;

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
    
    public bool Laser;
    public bool Plasma;
    public bool ShootState1;
    public bool ShootState2;
    public bool ShootState3;
    public bool ShootState4;

    public float CloneTimer;
    public int CloneCounter;
    public bool EnableClone;
 

    public static int BossCounter;

    public static int Score;


    // Use this for initialization
    void Start()
    {

        MoveToIdle();
        audioSrc = GetComponent<AudioSource>();
        normalPosition = gameObject.transform.position;
        Laser = true;
        ShootState1 = true;
        ShootState2 = false;
        ShootState3 = false;
        ShootState4 = false;
        Plasma = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForPlayer());
        Easy = GameControl.control.Easy;
        Normal = GameControl.control.Normal;
        Hard = GameControl.control.Hard;
        RandomPosition = Random.Range(1, 4);

        if (CloneCounter <= 5)
        {
            if (CloneTimer > 0)
            {
                CloneTimer -= Time.deltaTime;
            }
            if (CloneTimer < 0)
            {
                CloneTimer += 5;
                EnableClone = true;
            }
            if (EnableClone)
            {
                CloneCounter++;
                Replicate();
                EnableClone = false;
            }
        }

        if (Vector3.Magnitude(transform.position - PlayerPosition) < attackRange && Time.time > lastShot + fireRate)
        {
            //=============================ShootingProjectiles============================//
            if (Laser)
            {
                if (ShootState1)
                {
                    if (Easy)
                    {
                        fireRate = 3f;
                        projectileForce = 400;
                    }
                    else if (Normal)
                    {
                        fireRate = 2f;
                        projectileForce = 600;

                    }
                    else if (Hard)
                    {
                        fireRate = 1f;
                        projectileForce = 800;

                    }
                    shoot();
                    ShootState1 = false;
                    ShootState2 = true;
                }
                if (ShootState2)
                {
                    if (Easy)
                    {
                        fireRate = 3f;
                        projectileForce = 400;
                    }
                    else if (Normal)
                    {
                        fireRate = 2f;
                        projectileForce = 600;

                    }
                    else if (Hard)
                    {
                        fireRate = 1f;
                        projectileForce = 800;

                    }
                    shoot();
                    ShootState2 = false;
                    ShootState3 = true;
                }
                if (ShootState3)
                {
                    if (Easy)
                    {
                        fireRate = 3f;
                        projectileForce = 400;
                    }
                    else if (Normal)
                    {
                        fireRate = 2f;
                        projectileForce = 600;

                    }
                    else if (Hard)
                    {
                        fireRate = 1f;
                        projectileForce = 800;

                    }
                    shoot();
                    ShootState3 = false;
                    ShootState4 = true;
                }
                if (ShootState4)
                {
                    if (Easy)
                    {
                        fireRate = 3f;
                        projectileForce = 400;
                    }
                    else if (Normal)
                    {
                        fireRate = 2f;
                        projectileForce = 600;

                    }
                    else if (Hard)
                    {
                        fireRate = 1f;
                        projectileForce = 800;

                    }
                    shoot();
                    ShootState4 = false;
                    Plasma = true;
                }
            }
         
            else if (Plasma)
            {
                if (Easy)
                {
                    fireRate = 3f;
                    projectileForce = 400;
                }
                else if (Normal)
                {
                    fireRate = 2f;
                    projectileForce = 450;

                }
                else if (Hard)
                {
                    fireRate = 1f;
                    projectileForce = 500;

                }
                shoot();
                Plasma = false;
                Laser = true;
                ShootState1 = true;
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
            Destroy(LaserBossObject);
        }
    }
    void FixedUpdate()
    {
        if (idle)
        {
            BodyLaserBoss.position = Vector3.Lerp(BodyLaserBoss.position, newPosition, smooth * Time.deltaTime);
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
                
                if (ShootState1)
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
                else if (ShootState2)
                {
                    Projectile = EnemyLaser;
                    audioSrc.clip = LaserShoot;
                    audioSrc.Play();

                    newBullet = Instantiate(Projectile, bulletSpawners[2].position, bulletSpawners[i].rotation);
                    newBullet.transform.LookAt(PlayerPosition);
                    Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                    Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                    Destroy(newBullet, 10.0f);

                    newBullet = Instantiate(Projectile, bulletSpawners[3].position, bulletSpawners[i].rotation);
                    newBullet.transform.LookAt(PlayerPosition);
                    Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                    Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                    Destroy(newBullet, 10.0f);
                }
                else if (ShootState3)
                {
                    Projectile = EnemyLaser;
                    audioSrc.clip = LaserShoot;
                    audioSrc.Play();

                    newBullet = Instantiate(Projectile, bulletSpawners[4].position, bulletSpawners[i].rotation);
                    newBullet.transform.LookAt(PlayerPosition);
                    Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                    Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                    Destroy(newBullet, 10.0f);

                    newBullet = Instantiate(Projectile, bulletSpawners[5].position, bulletSpawners[i].rotation);
                    newBullet.transform.LookAt(PlayerPosition);
                    Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                    Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                    Destroy(newBullet, 10.0f);
                }
                else if (ShootState4)
                {
                    Projectile = EnemyLaser;
                    audioSrc.clip = LaserShoot;
                    audioSrc.Play();

                    newBullet = Instantiate(Projectile, bulletSpawners[6].position, bulletSpawners[i].rotation);
                    newBullet.transform.LookAt(PlayerPosition);
                    Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                    Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                    Destroy(newBullet, 10.0f);

                    newBullet = Instantiate(Projectile, bulletSpawners[7].position, bulletSpawners[i].rotation);
                    newBullet.transform.LookAt(PlayerPosition);
                    Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                    Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
                    Destroy(newBullet, 10.0f);
                }
            }
            else if (Plasma)
            {
                Projectile = EnemyPlasma;
                audioSrc.clip = PlasmaShoot;
                audioSrc.Play();
                newBullet = Instantiate(Projectile, bulletSpawners[8].position, bulletSpawners[i].rotation);
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
        Instantiate(explosionPrefab, LaserBossBody.transform.position, Quaternion.identity);
    }
    void Replicate()
    {
        Instantiate(smallBossPrefab, LaserBossBody.transform.position, Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(LaserBossBody.transform.position, awarenessRange);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(LaserBossBody.transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(LaserBossBody.transform.position, stoppingDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(LaserBossBody.transform.position, enemyRadius);
    }
}

