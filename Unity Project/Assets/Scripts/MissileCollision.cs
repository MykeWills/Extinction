using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollision : MonoBehaviour
{
    
    public GameObject explosion;
    public GameObject MissileRocketObject;

    // Use this for initialization
    void Start()
    {
        explosion.SetActive(false);
    }
    void OnCollisionEnter()
    {
        explosion.SetActive(true);
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(MissileRocketObject); // destroy the grenade
        Destroy(expl, 0.5f); // delete the explosion after 3 seconds
    }
}
