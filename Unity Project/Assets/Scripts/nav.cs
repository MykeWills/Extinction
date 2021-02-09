using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nav : MonoBehaviour {
    public Vector3 PlayerPosition;

    public GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
        transform.LookAt(PlayerPosition);
        GetComponent<NavMeshAgent>().destination = Player.transform.position;


    }
}
