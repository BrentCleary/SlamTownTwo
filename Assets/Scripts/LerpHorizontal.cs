using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpHorizontal : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float moveSpeed = 20f; // Constant movement speed

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isMoving)
        {   
            MoveLeft();
            System.Console.WriteLine("isMoving = " + isMoving);
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving)
        {
            MoveRight();
            System.Console.WriteLine("isMoving = " + isMoving);

        }

        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                isMoving = false;
                System.Console.WriteLine("isMoving = " + isMoving);

            }
        }
    }

    private void MoveLeft()
    {
        targetPosition = transform.position + new Vector3(0f, 0f, 10f);
        isMoving = true;
    }

    private void MoveRight()
    {
        targetPosition = transform.position + new Vector3(0f, 0f, -10f);
        isMoving = true;
    }
}