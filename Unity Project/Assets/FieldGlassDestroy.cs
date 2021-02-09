using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGlassDestroy : MonoBehaviour {

    public GameObject explosionPrefab;
    public GameObject GlassCenterPosition;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LaserLVL1"))
        {
            Explode();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("LaserLVL2"))
        {
            Explode();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("LaserLVL3"))
        {
            Explode();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("LaserLVL4"))
        {
            Explode();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Missle"))
        {
            Explode();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Gauss"))
        {
            Explode();
            Destroy(gameObject);
        }
    }
    void Explode()
    {
        Instantiate(explosionPrefab, GlassCenterPosition.transform.position, Quaternion.identity);
    }



}
