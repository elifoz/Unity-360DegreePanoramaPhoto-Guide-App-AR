using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScale : MonoBehaviour
{
    public GameObject plot;

    public void PlusWidth()
    {
        PlacementIndicator.instance.visual.gameObject.transform.localScale += new Vector3(0.1f, 0f, 0.1f);
        ObjectPlacement2.instance.AllPhotoPrefab.transform.localScale += new Vector3(0.1f, 0f, 0.1f);
       // plot.transform.localScale = new Vector3(PlacementIndicator.instance.visual.gameObject.transform.localScale.x, PlacementIndicator.instance.visual.gameObject.transform.localScale.y, PlacementIndicator.instance.visual.gameObject.transform.localScale.z);     
    }
    public void PlusHeight()
    {
        PlacementIndicator.instance.visual.gameObject.transform.localScale += new Vector3(0f, 0.1f, 0f);
        ObjectPlacement2.instance.AllPhotoPrefab.transform.localScale += new Vector3(0f, 0.1f,0f);
        // plot.transform.localScale = new Vector3(PlacementIndicator.instance.visual.gameObject.transform.localScale.x, PlacementIndicator.instance.visual.gameObject.transform.localScale.y, PlacementIndicator.instance.visual.gameObject.transform.localScale.z);     
    }

    public void MinusWidth()
    {
        PlacementIndicator.instance.visual.gameObject.transform.localScale -= new Vector3(0.1f, 0f, 0.1f);
        ObjectPlacement2.instance.AllPhotoPrefab.transform.localScale -= new Vector3(0.1f, 0f, 0.1f);
      //  plot.transform.localScale = new Vector3(PlacementIndicator.instance.visual.gameObject.transform.localScale.x, PlacementIndicator.instance.visual.gameObject.transform.localScale.y, PlacementIndicator.instance.visual.gameObject.transform.localScale.z);
    }
    public void MinusHeight()
    {
        PlacementIndicator.instance.visual.gameObject.transform.localScale -= new Vector3(0f, 0.1f, 0f);
        ObjectPlacement2.instance.AllPhotoPrefab.transform.localScale -= new Vector3(0f, 0.1f, 0f);
        //  plot.transform.localScale = new Vector3(PlacementIndicator.instance.visual.gameObject.transform.localScale.x, PlacementIndicator.instance.visual.gameObject.transform.localScale.y, PlacementIndicator.instance.visual.gameObject.transform.localScale.z);
    }
}
