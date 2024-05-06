using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float moveLeftSpeed;

    private PlayerController playerControllerScript;
    private GameManager gameManagerScript;

    void Start()
    {
        // This script find the GameObject by tag "Player", and gets the PlayerController class from the object,
        // and assigns it to the private variable playerControllerScript, or type ?PlayerController?
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        moveLeftSpeed = gameManagerScript.gameSpeed;
    }

    void Update()
    {
        moveLeftSpeed = gameManagerScript.gameSpeed;
        
        if(playerControllerScript.gameOver == false)
        { 
            transform.Translate(Vector3.left * Time.deltaTime * moveLeftSpeed);
        }

    }

}
