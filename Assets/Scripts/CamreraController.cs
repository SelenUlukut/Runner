using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreraController : MonoBehaviour {

    private Transform lookAt;
    private Vector3 offset;
    private Vector3 moveVec;



    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.Portrait;

        lookAt = GameObject.Find("Player").transform;
        offset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update () {
        moveVec = lookAt.position + offset;
        moveVec.x = 0;
        moveVec.y = transform.position.y;
        transform.position = moveVec;

        

    }
}
