using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleMovement : MonoBehaviour {
    private int rote;
    private Rigidbody rb;
   

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rote = (int)UnityEngine.Random.Range( -1.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if ( Math.Abs(transform.position.x) > 3.5f )
        {
            rote = -rote;
        }
        rb.velocity =  new Vector3(rote, 0, 0);
	}

    
}
