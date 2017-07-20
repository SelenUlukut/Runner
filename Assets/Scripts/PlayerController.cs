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

    public Text score_text;
    
	// Use this for initialization
	void Start () {
        lastPos = transform.position;
        speed = 50f;
        point = 0;
        score_text.text = "score: 0";
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        cont = transform.rotation.y;
        float h = 2f * Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, h, 0);

        if (transform.rotation.y!=cont)
        {
            speed = 40;
        }
        else
        {
            if (speed < 160)
            {
                Debug.Log("asd");
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
        moveVector.x = moveVector.x * 30;

        //   Debug.Log(transform.position);
        
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
            Destroy(other.gameObject);
            score_text.text = "score: " + point;
        }
        if (other.tag == "Barrier")
        {
            
            angle = transform.rotation.y*180*1.2f;
            transform.Rotate(0,-angle,0);
        }
        if (other.tag == "Obstacle")
        {

            //SceneManager.LoadScene("die_scene");
        }
        
    }

  


 
    //Eklenecekler
    //Object pooling
    //Görsellikte iyileştirme
    //Kontrollerin sağ-sol tuşlarından taşınması
    //Menu


}
