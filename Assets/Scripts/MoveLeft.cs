using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20f;
    public float distance;

    private PlayerController playerControllerScript;

    private float leftBound = -10;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(playerControllerScript.gameOver == false)
        {
            distance = speed * Time.deltaTime;
            transform.Translate(Vector3.left * distance);
        }

        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
