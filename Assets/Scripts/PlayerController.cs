using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float cont;
    private Vector3 moveVector;
    public int point;
    public float angle;

    private Vector3 lastPos;

    public Canvas panel;
    public Text score_text;
    public Text speed_text;

    private bool left;
    private bool right;

    // Use this for initialization
    void Start () {
        panel.enabled = false;
 
        lastPos = transform.position;
        speed = 50f;
        point = 0;
        score_text.text = "score: 0";
        speed_text.text = "speed: 0";

        left = false;
        right = false;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        int sp = (int)((speed/50)*100);
        speed_text.text = "speed: %" + sp;

        cont = transform.rotation.y;
        if (left)
        {
            transform.Rotate(0, -1.5f, 0);
        }
        if (right)
        {
            transform.Rotate(0, 1.5f, 0);
        }

        if (transform.rotation.y!=cont)
        {
            if (speed > 51)
                speed--;
        }
        else
        {
            if (speed < 160)
            {
                speed += 0.2f;
            }
        }

          while (transform.rotation.y > 0.4f)
            {
                cont = speed;
                transform.Rotate(0,-0.01f,0);
                speed = cont;
            }
            while (transform.rotation.y < -0.4f)
            {
                cont = speed;
                transform.Rotate(0, 0.01f, 0);
                speed = cont;
            }


        moveVector = transform.rotation * new Vector3(0, 0, 1);
         
        moveVector.y = 0;
        moveVector.z = moveVector.z * speed;
        moveVector.x = moveVector.x * 15;

        
        transform.position += moveVector * Time.fixedDeltaTime;
        lastPos = transform.position;
        if (transform.position.x>3.75f)
        {
            lastPos.x = 3.75f;
            transform.position = lastPos;
        }
        if (transform.position.x < -3.75f)
        {
            lastPos.x = -3.75f;
            transform.position = lastPos;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Diamond")
        {
            point += (int)speed;
            other.gameObject.SetActive(false);
            score_text.text = "score: " + point;
        }
        if (other.tag == "Barrier")
        {
            
            angle = transform.rotation.y*180*1.2f;
            transform.Rotate(0,-angle,0);
        }
        if (other.tag == "Obstacle")
        {
            speed = 50f;
            Time.timeScale = 0;
            panel.enabled = true;
        }
    }
    
    public void turnLeft()
    {
        left = true;
    }
    public void turnRight()
    {
        right = true;
    }

    public void stopTurnLeft()
    {
        left = false;
    }
    public void stopTurnRight()
    {
        right = false;
        Debug.Log(right);
    }




    //Eklenecekler
    //Görsellik
    //Ses
    //coinlerin yol oluşturması
}
