using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private CharacterController controller;
    public float speed=0;
    public float cont;
    private Vector3 moveVector;
    public int point;
    public float angle;
	// Use this for initialization
	void Start () {
        point = 0;
        speed = 1000f;
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {

        float h = 2f * Input.GetAxisRaw("Horizontal");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Diamond")
        {
           // point++;
            Destroy(other.gameObject);
        }
        if (other.tag == "Barrier")
        {
            angle = transform.rotation.y;
            angle = - angle * 180;
            transform.Rotate(0, angle, 0);
        }
    }

  



}
//top niye çok ileriye gidiyo en başta?