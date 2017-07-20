using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnController : MonoBehaviour {

    public GameObject[] prefabs;
    public float spawn_z;
    public float spawn_x;
    private float length;
    private int obstacleOnScreen;
    private float safeZone;
    private Transform playerTransform;
    private List<GameObject> activeObstacles;
    private int randomIndex;

    private float safeZone_road;
    public float spawn_z_road;
    private float length_road;
    private int roadOnScreen;
    private List<GameObject> activeRoads;


    // Use this for initialization
    void Start()
    {
        safeZone = 50f;
        spawn_z = 30f;
        spawn_x = 0f;
        obstacleOnScreen = 20;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activeObstacles = new List<GameObject>();

        safeZone_road= 20f;
        spawn_z_road = -10f;
        length_road = 50f;
        roadOnScreen = 9;
        activeRoads = new List<GameObject>();

        for (int i = 0; i < obstacleOnScreen; i++)
        {
            spawn();
        }

        GameObject obj;
        obj = Instantiate(prefabs[0]) as GameObject;
        obj.transform.SetParent(transform);
        obj.transform.position = Vector3.forward * spawn_z_road;
        activeRoads.Add(obj);

        for (int i = 0; i < roadOnScreen; i++)
        {
            spawnRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (playerTransform.position.z - safeZone > (spawn_z - roadOnScreen * length_road))
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
        GameObject obj;
        randomIndex = UnityEngine.Random.Range(1, prefabs.Length);
        obj = Instantiate(prefabs[randomIndex]) as GameObject;
        obj.transform.SetParent(transform);
        spawn_x = UnityEngine.Random.Range(-3.25f, 3.25f);
        obj.transform.position = new Vector3(spawn_x, 0,spawn_z);

        activeObstacles.Add(obj);
        length = UnityEngine.Random.Range(6, 45);
        spawn_z += length;
    }

    void delete()
    {
        Destroy(activeObstacles[0]);
        activeObstacles.RemoveAt(0);
    }

    void spawnRoad()
    {
        GameObject obj;
        obj = Instantiate(prefabs[0]) as GameObject;
        obj.transform.SetParent(transform);
        obj.transform.position = Vector3.forward * spawn_z_road;

        activeRoads.Add(obj);
        spawn_z_road += length_road;
    }

    void deleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }

}
