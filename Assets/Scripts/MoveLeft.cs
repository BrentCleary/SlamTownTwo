using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    private float normalSpeed = 40;
    private float boostSpeed = 60;

    // Reduces speed value After Boost
    private float dragGroundValue = 2f;
    private float dragAirValue = .5f;

    private PlayerController playerControllerScript;

    void Start()
    {
        // This script find the GameObject by tag "Player", and gets the PlayerController class from the object,
        // and assigns it to the private variable playerControllerScript, or type ?PlayerController?
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        speed = normalSpeed;
    }

    void Update()
    {
        if(playerControllerScript.gameOver == false)
        { 
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        ChangeSpeedBoost();
        Drag();


    }


    // Changes speed to 60 if boost bool is true in PlayerController.scr
    void ChangeSpeedBoost()
    {
        if(playerControllerScript.boostOn == true)
        {
            speed = boostSpeed;
        }
    }

    void Drag()
    {
        if(speed > normalSpeed && playerControllerScript.isOnGround == true)
        {
            speed -= dragGroundValue * Time.time * Time.deltaTime;
        }

        if(speed > normalSpeed && playerControllerScript.isOnGround == false)
        {
            speed -= dragAirValue * Time.time * Time.deltaTime;
        }
    }

}
