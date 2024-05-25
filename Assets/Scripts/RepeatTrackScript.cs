using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatTrackScript : MonoBehaviour
{

    // Track Positions
    private Vector3 trackStartPosition = new Vector3 (7000, 0, 0);
    private Vector3 trackEndPosition = new Vector3 (-3000, 0, 0);
    // Track Transforms - 10 Tracks Total
    private Transform trackGroup_0;
    private Transform trackGroup_1;
    private Transform trackGroup_2;
    private Transform trackGroup_3;
    private Transform trackGroup_4;
    private Transform trackGroup_5;
    private Transform trackGroup_6;
    private Transform trackGroup_7;
    private Transform trackGroup_8;
    private Transform trackGroup_9;

    // Track List 
    public List<Transform> trackList;


    // Start is called before the first frame update
    void Start()
    {
        // Track GameObjects
        trackGroup_0 = GameObject.Find("TrackPlaneGroup (0)").GetComponent<Transform>();
        trackGroup_1 = GameObject.Find("TrackPlaneGroup (1)").GetComponent<Transform>();
        trackGroup_2 = GameObject.Find("TrackPlaneGroup (2)").GetComponent<Transform>();
        trackGroup_3 = GameObject.Find("TrackPlaneGroup (3)").GetComponent<Transform>();
        trackGroup_4 = GameObject.Find("TrackPlaneGroup (4)").GetComponent<Transform>();
        trackGroup_5 = GameObject.Find("TrackPlaneGroup (5)").GetComponent<Transform>();
        trackGroup_6 = GameObject.Find("TrackPlaneGroup (6)").GetComponent<Transform>();
        trackGroup_7 = GameObject.Find("TrackPlaneGroup (7)").GetComponent<Transform>();
        trackGroup_8 = GameObject.Find("TrackPlaneGroup (8)").GetComponent<Transform>();
        trackGroup_9 = GameObject.Find("TrackPlaneGroup (9)").GetComponent<Transform>();

        // Track List
        trackList = new List<Transform>() {trackGroup_0, trackGroup_1, trackGroup_2, trackGroup_3, trackGroup_4, trackGroup_5, trackGroup_6, trackGroup_7, trackGroup_8, trackGroup_9};

    }

    // Update is called once per frame
    void LateUpdate()
    {
        TrackReset();
        TrackSpacingAdjustmentAuto();

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
            // Snap Track-0 to Track-End
            if(trackList[0].transform.position.x > trackList[trackList.Count-1].transform.position.x) // Checks track positions to only adjust on reset
            {
                if(trackList[0].transform.position.x - trackList[trackList.Count-1].transform.position.x != 1000)
                {
                    trackList[0].transform.position = new Vector3(trackList[trackList.Count-1].transform.position.x + 1000, 
                                                                trackList[trackList.Count-1].transform.position.y, 
                                                                trackList[trackList.Count-1].transform.position.z);
                }
            }

            // Snap Track-End to Track-End - 1
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









// -------------------------- Temporary Comment Out for Refactor ---------------------------------------

    // Adjusts TrackGroups transform.position on reset to eliminate gaps between tracks that occur around ( gameSpeed > 1000 )
    // void ScenerySpacingAdjustment()
    // {
    //     // sceneryLeftGroup 0 to 1
    //     if(sceneryLeftGroup_0.position.x > sceneryLeftGroup_1.position.x) // Checks track positions to only adjust on reset
    //     {
    //         if(sceneryLeftGroup_0.position.x - sceneryLeftGroup_1.position.x != 1000)
    //         {
    //             sceneryLeftGroup_0.position = new Vector3(sceneryLeftGroup_1.position.x + 1000, 
    //                                                     sceneryLeftGroup_1.position.y, 
    //                                                     sceneryLeftGroup_1.position.z);
    //         }
    //     }

    //     // sceneryLeftGroup 1 to 0
    //     if(sceneryLeftGroup_1.position.x > sceneryLeftGroup_0.position.x) // Checks track positions to only adjust on reset
    //     {
    //         if(sceneryLeftGroup_1.position.x - sceneryLeftGroup_0.position.x != 1000)
    //         {
    //             sceneryLeftGroup_1.position = new Vector3(sceneryLeftGroup_0.position.x + 1000, 
    //                                                     sceneryLeftGroup_0.position.y, 
    //                                                     sceneryLeftGroup_0.position.z);
    //         }
    //     }

    //     // sceneryRightGroup 0 to 1
    //     if(sceneryRightGroup_0.position.x > sceneryRightGroup_1.position.x) // Checks track positions to only adjust on reset
    //     {
    //         if(sceneryRightGroup_0.position.x - sceneryRightGroup_1.position.x != 1000)
    //         {
    //             sceneryRightGroup_0.position = new Vector3(sceneryRightGroup_1.position.x + 1000, 
    //                                                     sceneryRightGroup_1.position.y, 
    //                                                     sceneryRightGroup_1.position.z);
    //         }
    //     }

    //     // sceneryRightGroup 1 to 0
    //     if(sceneryRightGroup_1.position.x > sceneryRightGroup_0.position.x) // Checks track positions to only adjust on reset
    //     {
    //         if(sceneryRightGroup_1.position.x - sceneryRightGroup_0.position.x != 1000)
    //         {
    //             sceneryRightGroup_1.position = new Vector3(sceneryRightGroup_0.position.x + 1000, 
    //                                                     sceneryRightGroup_0.position.y, 
    //                                                     sceneryRightGroup_0.position.z);
    //         }
    //     }

    // }

}




// PREVIOUS SCRIPTS FOR REFERENCE

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