using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Stoptracking : MonoBehaviour
{
    // Start is called before the first frame update
    public ARPlaneManager planeManager;
    public PlaceContent placeContent;
    bool tracking = true;
    void Start()
    {
        tracking = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking == false)
        {
            planeManager.enabled = false;
            placeContent.enabled = false;
            foreach (ARPlane plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

            //manager.enabled = false;
        }
        else
        {
            planeManager.enabled = true;
            placeContent.enabled = true;
            foreach (ARPlane plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }

        }
    }
    public void turnOfforOn()
    {

        if (tracking==true)
        {
            tracking = false;
        }
        else
        {
            tracking = true;
        }
        
    }
  
}
