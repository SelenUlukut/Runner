using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleMovement : MonoBehaviour {
    private int rote;
    private bool move;
	// Use this for initialization
	void Start () {
        rote = (int)UnityEngine.Random.Range( -1.5f, 1.5f);
        move = true;
	}
	
	// Update is called once per frame
	void Update () {
        if ( Math.Abs(transform.position.x) > 3.5f )
        {
            rote = -rote;
        }
        if (move)
        {
            transform.position += new Vector3(5 * rote, 0, 0) * Time.fixedDeltaTime;
        }
    }    
    public void setMoveFalse()
    {
        move = false;
    }
    public void setMoveTrue()
    {
        move = true;
    }
}
