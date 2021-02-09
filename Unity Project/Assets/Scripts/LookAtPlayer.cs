using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    GameObject target;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        target = GameObject.Find("/MainPlayer/Player");
        transform.LookAt(target.transform);

    }
}
