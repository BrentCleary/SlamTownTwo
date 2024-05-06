using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // Track Positions
    private Vector3 trackStartPosition;
    private Vector3 trackEndPosition;
    private int track_X_Start = 1080;
    private int track_X_End = -1080;


    // Scenery Positions
    private Vector3 sceneryStartPosition;
    private Vector3 sceneryEndPosition;
    private int scenery_X_Start = 1000;
    private int scenery_X_End = -1000;


    // Start is called before the first frame update
    void Start()
    {
        // Track Positions
        trackStartPosition = new Vector3(track_X_Start, transform.position.y, transform.position.z);
        trackEndPosition = new Vector3(track_X_End, transform.position.y, transform.position.z);

        // Scenery Positions
        sceneryStartPosition = new Vector3(scenery_X_Start, transform.position.y, transform.position.z);
        sceneryEndPosition = new Vector3(scenery_X_End, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        TrackReset();
        EnvironmentReset();
    }

    void TrackReset()
    {
        if(gameObject.CompareTag("Ground"))
        {
            if(transform.position.x < trackEndPosition.x)
            {
                transform.position = trackStartPosition;
            }
        }
    }

    void EnvironmentReset()
    {
        if(gameObject.CompareTag("Scenery"))
        {
            if(transform.position.x < sceneryEndPosition.x)
            {
                transform.position = sceneryStartPosition;
            }
        }
    }
}
