using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Spawn Manager Variables
    public GameObject obstaclePrefab;
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
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }

}
