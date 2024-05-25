using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatScenery : MonoBehaviour
{

    // scenery Positions
    private Vector3 sceneryStartPosition = new Vector3 (4000, 0, 0);
    private Vector3 sceneryEndPosition = new Vector3 (-3000, 0, 0);
    // scenery Transforms
    private Transform sceneryGroup_0;
    private Transform sceneryGroup_1;
    private Transform sceneryGroup_2;
    private Transform sceneryGroup_3;
    private Transform sceneryGroup_4;
    private Transform sceneryGroup_5;
    private Transform sceneryGroup_6;
    // scenery List 
    public List<Transform> sceneryList;


    // Start is called before the first frame update
    void Start()
    {
        // Scenery GameObjects
        sceneryGroup_0 = GameObject.Find("SceneryGroup (0)").GetComponent<Transform>();
        sceneryGroup_1 = GameObject.Find("SceneryGroup (1)").GetComponent<Transform>();
        sceneryGroup_2 = GameObject.Find("SceneryGroup (2)").GetComponent<Transform>();
        sceneryGroup_3 = GameObject.Find("SceneryGroup (3)").GetComponent<Transform>();
        sceneryGroup_4 = GameObject.Find("SceneryGroup (4)").GetComponent<Transform>();
        sceneryGroup_5 = GameObject.Find("SceneryGroup (5)").GetComponent<Transform>();
        sceneryGroup_6 = GameObject.Find("SceneryGroup (6)").GetComponent<Transform>();

        // Scenery List
        sceneryList = new List<Transform>() {sceneryGroup_0, sceneryGroup_1, sceneryGroup_2, sceneryGroup_3, sceneryGroup_4, sceneryGroup_5, sceneryGroup_6};

    }

    // Update is called once per frame
    void LateUpdate()
    {
        SceneryReset();
        ScenerySpacingAdjustmentAuto();

    }


    void SceneryReset()
    {
        if(gameObject.CompareTag("Scenery"))
        {
            Debug.Log("Scenery Group Detected");

            if(transform.position == sceneryEndPosition)
            {
                Debug.Log("At End Position");
            }

            if(transform.position.x < sceneryEndPosition.x)
            {
                transform.position = sceneryStartPosition;
                Debug.Log(sceneryGroup_0 + " reset at " + transform.position);
            }
        }
        

    }


// -------------------------- Temporary Comment Out for Refactor ---------------------------------------

    // Adjusts SceneryGroups transform.position on reset to eliminate gaps between scenerys that occur around ( gameSpeed > 1000 )
    void ScenerySpacingAdjustmentAuto()
    {
        for(int i = 0; i < sceneryList.Count; i++)
        {
            // Snap Track-0 to Track-End
            if(sceneryList[0].transform.position.x > sceneryList[sceneryList.Count-1].transform.position.x) // Checks track positions to only adjust on reset
            {
                if(sceneryList[0].transform.position.x - sceneryList[sceneryList.Count-1].transform.position.x != 1000)
                {
                    sceneryList[0].transform.position = new Vector3(sceneryList[sceneryList.Count-1].transform.position.x + 1000, 
                                                                    sceneryList[sceneryList.Count-1].transform.position.y, 
                                                                    sceneryList[sceneryList.Count-1].transform.position.z);
                }
            }

            // Snap Track-End to Track-End - 1
            if(sceneryList[sceneryList.Count-1].position.x > sceneryList[0].position.x) // Checks track positions to only adjust on reset
            {
                if(sceneryList[sceneryList.Count-1].position.x - sceneryList[sceneryList.Count-2].position.x != 1000)
                {
                    sceneryList[sceneryList.Count-1].position = new Vector3(sceneryList[sceneryList.Count-2].position.x + 1000, 
                                                                            sceneryList[sceneryList.Count-1].position.y, 
                                                                            sceneryList[sceneryList.Count-1].position.z);
                }
            }

            // Snap Track i to Track i-1
            else if(sceneryList[i].position.x > sceneryList[i+1].position.x) // Checks track positions to only adjust on reset
            {
                if(sceneryList[i].position.x - sceneryList[i-1].position.x != 1000)
                {
                    sceneryList[i].position = new Vector3(sceneryList[i-1].position.x + 1000, 
                                                        sceneryList[i-1].position.y, 
                                                        sceneryList[i-1].position.z);
                }
            }

        }

    }

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