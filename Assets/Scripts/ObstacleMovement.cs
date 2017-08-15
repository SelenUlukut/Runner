using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleMovement : MonoBehaviour {
    private int rote;
    private bool move;
    public float speed;
    private float border;
    private float range;
	// Use this for initialization
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (move)
        {
            if (Math.Abs(transform.position.x) >= border)
            {
                rote = -rote;
            }
            
            transform.position += new Vector3(speed * rote, 0, 0) * Time.fixedDeltaTime;
            transform.Rotate(0,0,-rote*speed);

        }
    }    
    public void setMoveTrue()
    {
        move = true;
        if (transform.position.z<2000)
        {
            range = 1.5f;
        }
        else
        {
            if (transform.position.z < 8000)
            {
                range = 2.5f;
                speed = 2.7f;
            }
            else
            {
                range = 3.5f;
                speed = 2.2f;
            }
        }
        
        rote = (int)UnityEngine.Random.Range(-range, range);
        move = true;
        speed = 4;
        border = 4.55f - (transform.localScale.x / 1.9f);
    }
    public void setMoveFalse()
    {
        move = false;
        speed = 0;
    }
}
