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

    public GameObject panel;
    public Text score_text;
    public Text speed_text;
    public Text scale_text;
    public Text highScore_text;
    public Text multiply_text;

    private float r;

    private bool left;
    private bool right;

    private float size;

    public GameObject sheep;

    public AudioClip barrierBoing;
    public AudioClip eating;
    public AudioClip hit;
    public AudioClip Fart_1;
    public AudioClip Fart_2;
    private AudioSource source;

    public ParticleSystem fart;

    // Use this for initialization
    void Start () {
        size = 1;

        source = GetComponent<AudioSource>();

        panel.SetActive(false);
 
        lastPos = transform.position;
        speed = 50f;
        point = 0;
        score_text.text = "Score \n0";
        speed_text.text = "Speed \n%100";
        scale_text.text = "Scale \n%100";
        highScore_text.text = "High Score <" + PlayerPrefs.GetInt("highScore", 0).ToString()+">";
        multiply_text.text = " X " + speed;

        left = false;
        right = false;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Debug.Log((int)transform.position.z);
        score_text.text = "Score \n" + point;
        int sp = (int)((speed/50)*100);
        speed_text.text = "Speed \n%" + sp;
        multiply_text.text = " X " + (int)speed;

        r = speed-50;
        r = (r / 65);
        if (r<0.5f)
        {
            multiply_text.color = new Color(2*r, 0, 1);
        }
        if (r>0.5f)
        {
            multiply_text.color = new Color(1,0,1-(r-0.5f));
        }

        if ((size>1.01f)&&(speed>80)) {
            getSmaller(speed);
        }

        int sc = (int)(size * 100);
        scale_text.text = "Scale \n%"+sc;

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
            if (speed < 150)
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
         
        moveVector.z = moveVector.z * speed;
        moveVector.x = moveVector.x * 15;
        moveVector.y = 0;

        transform.position += moveVector * Time.fixedDeltaTime;
        lastPos = transform.position;

        float delta;
        float border = 4.55f - size / 2;
        if (transform.position.x > border)
        {
            delta =lastPos.x - border;
            lastPos.x -= delta ;
            lastPos.x -= 0.01f;
            transform.position = lastPos;
        }
        if (transform.position.x < -border)
        {
            delta = lastPos.x + border;
            lastPos.x -= delta;
            lastPos.x += 0.01f;
            transform.position = lastPos;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Diamond")
        {
            source.PlayOneShot(eating, 0.5f);
            point += (int)speed;
            other.gameObject.SetActive(false);
            getBigger();
            if (point > PlayerPrefs.GetInt("highScore", 0))
            {
                PlayerPrefs.SetInt("highScore", point);
                highScore_text.text = "High Score <" + point+">";
            }
        }
        if (other.tag == "Barrier")
        {
            source.PlayOneShot(barrierBoing,0.5f);
            angle = transform.rotation.y*180*1.2f;
            transform.Rotate(0,-angle,0);
        }
        if (other.tag == "Obstacle")
        {
            source.PlayOneShot(hit, 1f);
            speed = 50f;
            point = 0;
            Time.timeScale = 0;
            panel.SetActive(true);
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
    }

    public void getSmaller(float speed)
    {
        float k;

        Vector3 tmp = fart.transform.position;
        tmp.x = transform.position.x;
        fart.transform.position = tmp;
       // fart.time = 5;
       // fart.Play();

        if (speed == 0)
        {
            k = transform.localScale.x - 1;
            transform.localScale += new Vector3(-k, -k, -k);
            size = 1;
            return;
        }

        k = speed / 160;
        k = k / 500;
        transform.localScale += new Vector3(-k, -k, -k);
        size = transform.localScale.x;
        Vector3 down = transform.position;
        down.y = (size - 1) / 2 - 0.25f;
        transform.position = down;
    }
    void getBigger()
    {
        //iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1)
        //"from", transform.localScale, "to", (transform.localScale + new Vector3(+0.05f, +0.05f, +0.05f)), "time", 1)
       // iTween.ScaleBy(transform.gameObject, iTween.Hash("x", 1.5, "y", 1.5, "z", 1.5, "time", 10));
        transform.localScale += new Vector3(+0.05f, +0.05f, +0.05f);
        size = transform.localScale.x;
        Vector3 up = transform.position;
        up.y = (size-1) / 2 - 0.25f;
        transform.position = up;
    }

    public float getSpeed()
    {
        return speed;
    }

    public void setPlayerPosZero()
    {
        Vector3 tmp;
        tmp = transform.position;
        tmp.z = 0f;
        transform.position = tmp;
    }
    public void hideButton()
    {
        panel.SetActive(false);
    }

    //Kenardan sekmeme

    //Eklenecekler
    //High score yanıp sönme
    //Her bin puan artışta particle effect
    //High score gecilince yehu sesi
    //Zayıflarken gaz çıkarma sesi
    //Yeme sesinin ikiye çıkarılması
    //Fake shadow


 
    
}
