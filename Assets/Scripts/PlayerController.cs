using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private CharacterController controller;
    public float speed=5.0f;
    private Vector3 moveVector;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        moveVector = Vector3.forward * speed;
        moveVector.x += Input.GetAxisRaw("Horizontal") * speed;

        controller.Move(moveVector * Time.deltaTime );

	}
}
