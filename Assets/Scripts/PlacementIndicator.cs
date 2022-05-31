using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    public static PlacementIndicator instance;
    public ARRaycastManager rayManager;
    public GameObject visual;
    public ARPlaneManager plane;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        plane = GetComponent<ARPlaneManager>();
        visual = transform.GetChild(0).gameObject;
       // visual.SetActive(false); //it has to be show after plane detection,not start
        
    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width/2,Screen.height/2), hits, TrackableType.Planes);
       
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy)
            {
                    visual.SetActive(true);  
              
            }
        }

    }
}
 