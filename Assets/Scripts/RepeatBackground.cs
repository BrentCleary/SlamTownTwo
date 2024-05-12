using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // Track Positions
    private Vector3 trackStartPosition = new Vector3 (4000, 0, 0);
    private Vector3 trackEndPosition = new Vector3 (-3000, 0, 0);

    // Track Transforms
    public Transform trackGroup_0;
    public Transform trackGroup_1;
    public Transform trackGroup_2;
    public Transform trackGroup_3;
    public Transform trackGroup_4;
    public Transform trackGroup_5;
    public Transform trackGroup_6;

    // Track List 
    public List<Transform> trackList;


    // Scenery Positions
    private Vector3 sceneryStartPosition;
    private Vector3 sceneryEndPosition;
    private int scenery_X_Start = 1000;
    private int scenery_X_End = -1000;

    // Scenery Transforms
    public Transform sceneryLeftGroup_0;
    public Transform sceneryLeftGroup_1;
    public Transform sceneryRightGroup_0;
    public Transform sceneryRightGroup_1;


    // Start is called before the first frame update
    void Start()
    {
        // Scenery Positions
        sceneryStartPosition = new Vector3(scenery_X_Start, transform.position.y, transform.position.z);
        sceneryEndPosition = new Vector3(scenery_X_End, transform.position.y, transform.position.z);

        trackGroup_0 = GameObject.Find("TrackPlaneGroup (0)").GetComponent<Transform>();
        trackGroup_1 = GameObject.Find("TrackPlaneGroup (1)").GetComponent<Transform>();
        trackGroup_2 = GameObject.Find("TrackPlaneGroup (2)").GetComponent<Transform>();
        trackGroup_3 = GameObject.Find("TrackPlaneGroup (3)").GetComponent<Transform>();
        trackGroup_4 = GameObject.Find("TrackPlaneGroup (4)").GetComponent<Transform>();
        trackGroup_5 = GameObject.Find("TrackPlaneGroup (5)").GetComponent<Transform>();
        trackGroup_6 = GameObject.Find("TrackPlaneGroup (6)").GetComponent<Transform>();

        trackList = new List<Transform>() {trackGroup_0, trackGroup_1, trackGroup_2, trackGroup_3, trackGroup_4, trackGroup_5, trackGroup_6};

        sceneryLeftGroup_0 = GameObject.Find("SceneryLeftGroup (0)").GetComponent<Transform>();
        sceneryLeftGroup_1 = GameObject.Find("SceneryLeftGroup (1)").GetComponent<Transform>();
        sceneryRightGroup_0 = GameObject.Find("SceneryRightGroup (0)").GetComponent<Transform>();
        sceneryRightGroup_1 = GameObject.Find("SceneryRightGroup (1)").GetComponent<Transform>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        TrackReset();
        // TrackSpacingAdjustment();
        TrackSpacingAdjustmentAuto();

        SceneryReset();
        ScenerySpacingAdjustment();
    }

    void TrackReset()
    {
        if(gameObject.CompareTag("Track"))
        {
            if(transform.position.x < trackEndPosition.x)
            {
                transform.position = trackStartPosition;
            }
        }
    }

    // For-Loop Track Reset
    // Adjusts TrackGroups transform.position on reset to eliminate gaps between tracks that occur around ( gameSpeed > 1000 )
    void TrackSpacingAdjustmentAuto()
    {
        for(int i = 0; i < trackList.Count; i++)
        {
            // Snap Track 0 to Track 6
            if(trackList[0].transform.position.x > trackList[trackList.Count-1].transform.position.x) // Checks track positions to only adjust on reset
            {
                if(trackList[0].transform.position.x - trackList[trackList.Count-1].transform.position.x != 1000)
                {
                    trackList[0].transform.position = new Vector3(trackList[trackList.Count-1].transform.position.x + 1000, 
                                                                trackList[trackList.Count-1].transform.position.y, 
                                                                trackList[trackList.Count-1].transform.position.z);
                }
            }

            // Snap Track End of Track List to Previous Track to Track i-1
            if(trackList[trackList.Count-1].position.x > trackList[0].position.x) // Checks track positions to only adjust on reset
            {
                if(trackList[trackList.Count-1].position.x - trackList[trackList.Count-2].position.x != 1000)
                {
                    trackList[trackList.Count-1].position = new Vector3(trackList[trackList.Count-2].position.x + 1000, 
                                                                        trackList[trackList.Count-1].position.y, 
                                                                        trackList[trackList.Count-1].position.z);
                }
            }

            // Snap Track i to Track i-1
            else if(trackList[i].position.x > trackList[i+1].position.x) // Checks track positions to only adjust on reset
            {
                if(trackList[i].position.x - trackList[i-1].position.x != 1000)
                {
                    trackList[i].position = new Vector3(trackList[i-1].position.x + 1000, 
                                                        trackList[i-1].position.y, 
                                                        trackList[i-1].position.z);
                }
            }

        }

    }

// ---------------------- Temporarily Disabled While Modular Reset in Development ---------------------------

    // Manual Track Reset - Being Refactored
    // Adjusts TrackGroups transform.position on reset to eliminate gaps between tracks that occur around ( gameSpeed > 1000 )
    // void TrackSpacingAdjustment()
    // {
    //     // Track Group 0 to 2
    //     if(trackGroup_0.position.x > trackGroup_2.position.x) // Checks track positions to only adjust on reset
    //     {
    //         if(trackGroup_0.position.x - trackGroup_2.position.x != 1000)
    //         {
    //             trackGroup_0.position = new Vector3(trackGroup_2.position.x + 1000, 
    //                                                 trackGroup_2.position.y, 
    //                                                 trackGroup_2.position.z);
    //         }
    //     }

    //     // Track Group 1 to 0
    //     if(trackGroup_1.position.x > trackGroup_0.position.x)
    //     {
    //         if(trackGroup_1.position.x - trackGroup_0.position.x != 1000)
    //         {
    //             trackGroup_1.position = new Vector3(trackGroup_0.position.x + 1000, 
    //                                                 trackGroup_0.position.y, 
    //                                                 trackGroup_0.position.z);
    //         }
    //     }

    //     // Track Group 2 to 1
    //     if(trackGroup_2.position.x > trackGroup_1.position.x)
    //     {
    //         if(trackGroup_2.position.x - trackGroup_1.position.x != 1000)
    //         {
    //             trackGroup_2.position = new Vector3(trackGroup_1.position.x + 1000, 
    //                                                 trackGroup_1.position.y, 
    //                                                 trackGroup_1.position.z);
    //         }
    //     }
    // }


    void SceneryReset()
    {
        if(gameObject.CompareTag("Scenery"))
        {
            if(transform.position.x < sceneryEndPosition.x)
            {
                transform.position = sceneryStartPosition;
            }
        }
    }

    // Adjusts TrackGroups transform.position on reset to eliminate gaps between tracks that occur around ( gameSpeed > 1000 )
    void ScenerySpacingAdjustment()
    {
        // sceneryLeftGroup 0 to 1
        if(sceneryLeftGroup_0.position.x > sceneryLeftGroup_1.position.x) // Checks track positions to only adjust on reset
        {
            if(sceneryLeftGroup_0.position.x - sceneryLeftGroup_1.position.x != 1000)
            {
                sceneryLeftGroup_0.position = new Vector3(sceneryLeftGroup_1.position.x + 1000, 
                                                        sceneryLeftGroup_1.position.y, 
                                                        sceneryLeftGroup_1.position.z);
            }
        }

        // sceneryLeftGroup 1 to 0
        if(sceneryLeftGroup_1.position.x > sceneryLeftGroup_0.position.x) // Checks track positions to only adjust on reset
        {
            if(sceneryLeftGroup_1.position.x - sceneryLeftGroup_0.position.x != 1000)
            {
                sceneryLeftGroup_1.position = new Vector3(sceneryLeftGroup_0.position.x + 1000, 
                                                    sceneryLeftGroup_0.position.y, 
                                                    sceneryLeftGroup_0.position.z);
            }
        }

        // sceneryRightGroup 0 to 1
        if(sceneryRightGroup_0.position.x > sceneryRightGroup_1.position.x) // Checks track positions to only adjust on reset
        {
            if(sceneryRightGroup_0.position.x - sceneryRightGroup_1.position.x != 1000)
            {
                sceneryRightGroup_0.position = new Vector3(sceneryRightGroup_1.position.x + 1000, 
                                                    sceneryRightGroup_1.position.y, 
                                                    sceneryRightGroup_1.position.z);
            }
        }

        // sceneryRightGroup 1 to 0
        if(sceneryRightGroup_1.position.x > sceneryRightGroup_0.position.x) // Checks track positions to only adjust on reset
        {
            if(sceneryRightGroup_1.position.x - sceneryRightGroup_0.position.x != 1000)
            {
                sceneryRightGroup_1.position = new Vector3(sceneryRightGroup_0.position.x + 1000, 
                                                    sceneryRightGroup_0.position.y, 
                                                    sceneryRightGroup_0.position.z);
            }
        }

    }

}
