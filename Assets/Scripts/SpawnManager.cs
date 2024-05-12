using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Spawn Manager Variables
    public GameObject[] obstaclePrefabs;

    // Scripts
    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;

    // Spawn Position
    // private Vector3 spawnPos = new Vector3(0, 0, 0);
    private Vector3 spawnPos;

    // SpawnObstacle Variables
    private float startDelay = 1f;
    private float repeatRate = 1f;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        spawnPos = gameObject.transform.position;
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        // increases the repeat rate accordering to the speed of the move left script
        repeatRate = (repeatRate / (gameManagerScript.normalSpeed / gameManagerScript.gameSpeed)) * Time.deltaTime;
    }

    void SpawnObstacle()
    {
        // Detects gameOver variable in PlayerController
        // Only active while gameOver is false
        
        int obstacleIndex = UnityEngine.Random.Range(0, obstaclePrefabs.Length);

        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
            Debug.Log("Spawning " + obstaclePrefabs[obstacleIndex].name);
        }
    }

    public Vector3 SpawnPositionRandomizer()
    {
        Vector3 spawnPosLeft = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z - 10);
        Vector3 spawnPosCenter = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z);
        Vector3 spawnPosRight = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z + 10);

        List<Vector3> spawnPositionList = new List<Vector3>() {spawnPosLeft, spawnPosCenter, spawnPosRight};
        
        Vector3 randomSpawnPosition = spawnPositionList[UnityEngine.Random.Range(0,3)];

        return randomSpawnPosition;

    }
}
