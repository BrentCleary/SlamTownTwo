using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Camera Settings
    public Camera camera;

    // Camera Field of View
    public float viewField;
    public float normalFieldOfView = 40;
    public float boostFieldOfView = 80;
    public float viewFieldInterpolater = 5;

    // Camera Position Offsets
    public Vector3 cameraPosition;
    public Vector3 normalCameraOffset = new Vector3(-15, 5, 0); // Offset from the player's position
    public Vector3 boostCameraOffset = new Vector3(-30, 5, 0); // Offset from the player's position
    public float smoothSpeed = 10.0f; // The smoothing speed of the camera follow

    // Boost Camera 
    public bool boostCameraOn = false;
    public float boostCameraTimer;

    // Player Variables
    private GameObject player;
    private PlayerController playerControllerScript;
    public Vector3 playerPosition; // The player's transform.position to follow


    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();

        camera = GetComponent<Camera>();
    }

    void Update()
    {
        playerPosition = player.GetComponent<Transform>().position;
        boostCameraTimer = boostCameraTimer - Time.deltaTime;

        SetCameraPosition();
        BoostTimerSet();
        
    }

    // Moves the camera further behind the player when boost is activated
    void SetCameraPosition()
    {
        // Null Check
        if (playerPosition == null) return;

        // Boost Camera
        if(boostCameraOn == true)
        {
            cameraPosition = playerPosition + boostCameraOffset;
            Vector3 boostSmoothedPosition = Vector3.Lerp(transform.position, cameraPosition, smoothSpeed * Time.deltaTime);
            transform.position = boostSmoothedPosition;

            // View Field Adjuster
            if(camera.fieldOfView < boostFieldOfView)
            {
                camera.fieldOfView = Mathf.Lerp(normalFieldOfView, boostFieldOfView, viewFieldInterpolater * Time.deltaTime);
            }
        }
        else if(boostCameraOn == false)
        {
            cameraPosition = playerPosition + normalCameraOffset;
            Vector3 normalSmoothedPosition = Vector3.Lerp(transform.position, cameraPosition, smoothSpeed * Time.deltaTime);
            transform.position = normalSmoothedPosition;

            // View Field Adjuster
            if(camera.fieldOfView > normalFieldOfView)
            {
                camera.fieldOfView = Mathf.Lerp(boostFieldOfView, normalFieldOfView, viewFieldInterpolater * Time.deltaTime);
            }
        }

    }

    // Sets a timer for the camera being in the boostCameraPosition
    void BoostTimerSet()
    {
        bool boostPossible = playerControllerScript.secondJump || playerControllerScript.isOnGround;

        if((playerControllerScript.boostOn && boostPossible) || (playerControllerScript.speedTestOn && boostPossible))
        {
            boostCameraTimer = .3f;
            boostCameraOn = true;
        }

        if(boostCameraTimer <= 0)
        {
            boostCameraOn = false;
        }

    }
}
