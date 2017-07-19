using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
    public float cont;
    private Vector3 moveVector;
    public int point;
    public float angle;

    public Text score_text;
    
	// Use this for initialization
	void Start () {
        
        point = 0;
        speed = 2f;
        rb = GetComponent<Rigidbody>();
        score_text.text = "score: 0";
    }
	
	// Update is called once per frame
	void Update () {

        cont = transform.rotation.y;
        float h = 2f * Input.GetAxisRaw("Horizontal");
        transform.Rotate(0, h, 0);
        if (transform.rotation.y!=cont)
        {
            speed = 2;
        }
        else
        {
            if (speed < 4)
            {
                speed += 0.01f;
            }
        }
     
      /*  while (transform.rotation.y > 0.4f)
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
        }*/
        moveVector = transform.rotation *new Vector3(0,0,20);
        moveVector.z = moveVector.z * speed;
        moveVector.y = 0;
        Debug.Log(moveVector);

        rb.velocity = moveVector;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Diamond")
        {
            point += (int)speed / 100;
            Destroy(collision.gameObject);
            score_text.text = "score: " + point;
        }
        if (collision.collider.tag == "Barrier")
        {
            angle = transform.rotation.y;
            angle = -1.2f * angle * 180;
            transform.Rotate(0, angle, 0);
        }
        if (collision.collider.tag == "Obstacle")
        {

            //SceneManager.LoadScene("die_scene");
        }
    }
  

    //Sorunlar
    //Top aşağıya batıyor
    //Scene değiştirince ışık gidiyor ve top hareketi garipleşiyor

    //Eklenecekler
    //Object pooling
    //Görsellikte iyileştirme
    //Kontrollerin sağ-sol tuşlarından taşınması
    //Top hareketi character control'dan rigid body'ye
    //Menu


}
