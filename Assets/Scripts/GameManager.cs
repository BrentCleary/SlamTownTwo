using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameSpeed;
    public float normalSpeed = 40;
    private float boostSpeed = 40;
    private float speedTestSpeed = 2000;

    // Reduces speed value After Boost
    private float dragGroundValue = 3f;
    private float dragAirValue = .3f;



    private PlayerController playerControllerScript;

    void Start()
    {
        // This script find the GameObject by tag "Player", and gets the PlayerController class from the object,
        // and assigns it to the private variable playerControllerScript, or type ?PlayerController?
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameSpeed = normalSpeed;

    }

    void Update()
    {
        ChangeSpeedBoost();
        Drag();

    }


    // Changes speed to 60 if boost bool is true in PlayerController.scr
    void ChangeSpeedBoost()
    {
        // Boost Speed for Gameplay
        if(playerControllerScript.boostOn == true)
        {
            gameSpeed = gameSpeed + boostSpeed;
        }

        // Test Speed for Debugging
        if(playerControllerScript.speedTestOn == true)
        {
            gameSpeed = gameSpeed + speedTestSpeed;
        }
    }

    void Drag()
    {
        if(gameSpeed > normalSpeed && playerControllerScript.isOnGround == true)
        {
            gameSpeed -= dragGroundValue * Time.time * Time.deltaTime;
        }

        if(gameSpeed > normalSpeed && playerControllerScript.isOnGround == false)
        {
            gameSpeed -= dragAirValue * Time.time * Time.deltaTime;
        }
    }

    // A function to check when 



}
