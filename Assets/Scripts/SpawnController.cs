using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnController : MonoBehaviour {

    public GameObject[] prefabs;
    private float spawn_z;
    private float spawn_x;
    private float spawn_y;
    private float length;
    private int obstacleOnScreen;
    private float safeZone;
    private Transform playerTransform;
    private List<GameObject> activeObstacles;
    

    private int index;

    private float safeZone_road;
    private float spawn_z_road;
    private float length_road;
    private int roadOnScreen;
    private List<GameObject> activeRoads;
    private GameObject [][] pool;
    private int[] poolPointer;

    public GameObject player;
    public GameObject sheep;
    // Use this for initialization
    void Start()
    {
        safeZone = 10f;
        spawn_z = 100f;
        spawn_x = 0f;
        obstacleOnScreen = 30;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activeObstacles = new List<GameObject>();

        safeZone_road = 60f;
        spawn_z_road = -10f;
        length_road = 50f;
        roadOnScreen = 12;
        activeRoads = new List<GameObject>();
        
        //Createing Pools
        pool = new GameObject[prefabs.Length][];
        poolPointer = new int[prefabs.Length];
        int capacity = roadOnScreen + 1;
        createPool(0, capacity);
          for (int i = 1; i < prefabs.Length; i++)
          {
              capacity = (obstacleOnScreen / (prefabs.Length - 1)) * 6;
              createPool(i, capacity);
          }

        //Spawning Obstacles
         for (int i = 0; i < obstacleOnScreen; i++)
         {
             spawn();
         }

        //Spawning Roads
        for (int i = 0; i < roadOnScreen; i++)
         {
             spawnRoad();
         }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (player.transform.position.z > 5000)
        {
            setPositionZero();
        }*/
        while (playerTransform.position.z - safeZone > (spawn_z - roadOnScreen * length_road-100))
          {
              spawn();
              delete();
          }

          if (playerTransform.position.z - safeZone_road > (spawn_z_road - roadOnScreen * length_road))
           {
               spawnRoad();
               deleteRoad();
           }

        
    }

    void spawn()
    {
        index = UnityEngine.Random.Range(1, prefabs.Length);

        if ((index < 4) && (index > 0))
        { 
            spawn_y = -0.25f;
        }
        if ((index < 7) && (index > 3))
        {
            spawn_y = 0.25f;
        }
        else
        {
            spawn_y = 0;
        }

        spawn_x = UnityEngine.Random.Range(-3.2f, 3.2f);
        pool[index][poolPointer[index]].transform.position = new Vector3(spawn_x, spawn_y,spawn_z);
        pool[index][poolPointer[index]].SetActive(true);

        if (pool[index][poolPointer[index]].tag == "Obstacle")
        {
            pool[index][poolPointer[index]].GetComponent<ObstacleMovement>().setMoveTrue();
        }

        activeObstacles.Add(pool[index][poolPointer[index]]);

        length = UnityEngine.Random.Range(15, 70);

        poolPointer[index]++;
        poolPointer[index] = poolPointer[index] % pool[index].Length;

        spawn_z += length;
    }
   
    void delete()
    {
        activeObstacles[0].SetActive(false);
        if (activeObstacles[0].tag == "Obstacle")
        {
            activeObstacles[0].GetComponent<ObstacleMovement>().setMoveTrue();
        }
        activeObstacles.RemoveAt(0);
    }

    void spawnRoad()
    {
        pool[0][poolPointer[0]].transform.position = Vector3.forward* spawn_z_road;
        pool[0][poolPointer[0]].SetActive(true);
        activeRoads.Add(pool[0][poolPointer[0]]);
        poolPointer[0]++;
        poolPointer[0] = poolPointer[0]%pool[0].Length ;
        spawn_z_road += length_road;
    }

    void deleteRoad()
    {
        activeRoads[0].SetActive(false);
        activeRoads.RemoveAt(0);
    }

    private void createPool(int poolID, int poolSize)
    {
        pool[poolID] = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[poolID][i] = Instantiate(prefabs[poolID]);
            pool[poolID][i].transform.SetParent(transform);
            pool[poolID][i].SetActive(false);
        }
        poolPointer[poolID] = 0;
    }
    public void Restart()
    {
        player.GetComponent<PlayerController>().hideButton();
        for (int i=0;i<obstacleOnScreen;i++)
        {
            delete();
        }
        for(int i = 0; i < roadOnScreen; i++)
        {
            deleteRoad();
        }
        Vector3 tmp = player.transform.position;
        player.transform.position = new Vector3(0,0,0);
        Quaternion Qtmp = player.transform.rotation;
        Qtmp.y = 0;
        player.transform.rotation = Qtmp;

        
        spawn_z = 100f;
        spawn_z_road = -10f;
     //   Spawnind Road
        for (int i = 0; i < roadOnScreen; i++)
        {
            spawnRoad();
        }
    //Spawning Obstacles
        for (int i = 0; i < obstacleOnScreen; i++)
        {
            spawn();
        }
        

        player.GetComponent<PlayerController>().getSmaller(0);
        sheep.GetComponent<Spin>().enabled = true;
        Time.timeScale = 1;

    }
     private void setPositionZero()
    {
        Vector3 tmp, delta;

        delta = player.transform.position;
        player.GetComponent<PlayerController>().setPlayerPosZero();
        
        for (int i = 0; i < roadOnScreen; i++)
        {
            Debug.Log("asd");
            tmp = activeRoads[i].transform.position;
            tmp.z -= delta.z;
            activeRoads[i].transform.position = tmp;
        }
        for (int i = 0; i < obstacleOnScreen; i++)
        {
            tmp = activeObstacles[i].transform.position;
            tmp.z -= delta.z;
            activeObstacles[i].transform.position = tmp;
        }
    }
   
}
