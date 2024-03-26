using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Spawn Manager Variables
    public GameObject[] obstaclePrefabs;

    // Spawn Position
    private Vector3 spawnPos = new Vector3(50, 0, 0);
    // SpawnObstacle Variables
    private float startDelay = 1f;
    private float repeatRate = 1f;
    private PlayerController playerControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        // Detects gameOver variable in PlayerController
        // Only active while gameOver is false
        
        int obstacleIndex = UnityEngine.Random.Range(0, obstaclePrefabs.Length);

        if(!playerControllerScript.gameOver != false)
        {
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
                    Debug.Log("Spawning");
        }
    }

    void DebugCheck()
    {
        Debug.Log("Spawning");
    }

}
