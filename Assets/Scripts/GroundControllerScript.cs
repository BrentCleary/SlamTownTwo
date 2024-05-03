using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControllerScript : MonoBehaviour
{

    public MoveLeft moveLeftScript;
    public bool detectCollision;

    // Start is called before the first frame update
    void Start()
    {
        moveLeftScript = GetComponent<MoveLeft>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            moveLeftScript.enabled = true;
            detectCollision = true;
            Debug.Log("Collision Detected");
        }

        if(other.gameObject.CompareTag("Player"))
        {
            moveLeftScript.enabled = true;
            detectCollision = true;
            Debug.Log("Collision Detected");
        }
    }

}
