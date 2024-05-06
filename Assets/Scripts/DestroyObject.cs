using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    private float obstacleBound = -20;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < obstacleBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if(transform.position.x < obstacleBound && gameObject.CompareTag("Particle"))
        {
            Destroy(gameObject);
        }
    }
}
