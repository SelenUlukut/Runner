using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    float randx;
    float randy;
    float randz;
    int counter;
    public GameObject player;
    private void Start()
    {
        counter = 0;
        changeRote();
    }
    // Update is called once per frame
    void Update () {
        counter++;
        float sp = player.GetComponent<PlayerController>().getSpeed();
        sp = sp / 40;
        transform.Rotate(sp*randx, sp*randy, sp*randz);
        if (counter>250)
        {
            changeRote();
            counter = 0;
        }
       
	}
    void changeRote()
    {
        int k;
        randx = Random.Range(1.5f, 3);
        randy = Random.Range(1.5f, 3);
        randz = Random.Range(1.5f, 3);
        k = Random.Range(-1, 1);
        if (k < 0)
        {
            randx = -randx;
        }
        k = Random.Range(-1, 1);
        if (k < 0)
        {
            randy = -randy;
        }
        k = Random.Range(-1, 1);
        if (k < 0)
        {
            randz = -randz;
        }
    }
}
