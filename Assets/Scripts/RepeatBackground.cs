using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // Track Positions
    private Vector3 trackStartPosition = new Vector3 (2000, 0, 0);
    private Vector3 trackEndPosition = new Vector3 (-1000, 0, 0);

    // Scenery Positions
    private Vector3 sceneryStartPosition;
    private Vector3 sceneryEndPosition;
    private int scenery_X_Start = 1000;
    private int scenery_X_End = -1000;

    public Transform trackGroup_1;
    public Transform trackGroup_2;
    public Transform trackGroup_3;

    // Start is called before the first frame update
    void Start()
    {

        // Scenery Positions
        sceneryStartPosition = new Vector3(scenery_X_Start, transform.position.y, transform.position.z);
        sceneryEndPosition = new Vector3(scenery_X_End, transform.position.y, transform.position.z);

        trackGroup_1 = GameObject.Find("TrackGroup_1").GetComponent<Transform>();
        trackGroup_2 = GameObject.Find("TrackGroup_2").GetComponent<Transform>();
        trackGroup_3 = GameObject.Find("TrackGroup_3").GetComponent<Transform>();
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
                TrackSpacingAdjustment();
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

    void TrackSpacingAdjustment()
    {
        // Track Group 3 : 1
        if(trackGroup_1.position.x > trackGroup_3.position.x)
        {
            if(trackGroup_1.position.x - trackGroup_3.position.x != 1000)
            {
                trackGroup_1.position = new Vector3 (trackGroup_3.position.x + 1000, 
                                                     trackGroup_3.position.y, 
                                                     trackGroup_3.position.z);
            }
        }

    }
}
