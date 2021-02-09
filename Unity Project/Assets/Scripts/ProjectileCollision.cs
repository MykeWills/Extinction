using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    
    public GameObject explosion;
    

    // Use this for initialization
    void Start()
    { 
        explosion.SetActive(false);
    }

    void OnCollisionEnter()
    {
        explosion.SetActive(true);
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject); // destroy the grenade
        Destroy(expl, 2f); // delete the explosion after 3 seconds

    }
}
