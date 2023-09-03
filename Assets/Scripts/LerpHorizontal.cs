using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpHorizontal : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float moveSpeed = 30f; // Constant movement speed

    // These variables are set to low to make sure they trigger
    private float boundaryLeft = 8f;  // Max Z position
    private float  boundaryRight= -8f; // Min Z position
    
    private bool lerpLeft = true;
    private bool lerpRight = true;

    public PlayerController playerController;
    public bool isOnGround;
    

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        BoundaryCheck();

        if (Input.GetKeyDown(KeyCode.A) && !isMoving && playerController.isOnGround && !playerController.gameOver)
        {   
            MoveLeft();
            Debug.Log("isMoving = " + isMoving);
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving && playerController.isOnGround && !playerController.gameOver)
        {
            MoveRight();
            Debug.Log("isMoving = " + isMoving);
        }

        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                isMoving = false;
                Debug.Log("isMoving = " + isMoving);

            }
        }
    }

    private void MoveLeft()
    {
        if(lerpLeft)
        {
            targetPosition = transform.position + new Vector3(0f, 0f, 10f);
            isMoving = true;
        }
    }

    private void MoveRight()
    {
        if(lerpRight)
        {
            targetPosition = transform.position + new Vector3(0f, 0f, -10f);
            isMoving = true;
        }
    }

    // if Player Transform Z position is greater/less than boundaries, set position to boundaries
    public void BoundaryCheck()
    {
        
        if(transform.position.z >= boundaryLeft)
        {
            lerpLeft = false;
        }
        else
        {
            lerpLeft = true;
        }

        if(transform.position.z <= boundaryRight)
        {
            lerpRight = false;
        }
        else
        {
            lerpRight = true;
        }
    }

}