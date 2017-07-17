using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private CharacterController controller;
    public float speed=0;
    public float cont;
    private Vector3 moveVector;
	// Use this for initialization
	void Start () {
        speed = 1000f;
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {

        float h = 5f * Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, h, 0);
     
        while (transform.rotation.y > 0.4f)
        {
            transform.Rotate(0,-0.01f,0);
        }
        while (transform.rotation.y < -0.4f)
        {
            transform.Rotate(0, 0.01f, 0);
        }


        moveVector = Vector3.forward * speed;
        moveVector = transform.rotation * moveVector;
        controller.SimpleMove(moveVector * Time.deltaTime);



    }
}
//top niye çok ileriye gidiyo en başta?