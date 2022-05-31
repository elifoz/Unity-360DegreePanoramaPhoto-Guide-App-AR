using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoShowController : MonoBehaviour
{
    public GameObject PhotoScroll;
  
    public void PhotoActive()
    {
        if (PhotoScroll.gameObject.activeInHierarchy)
        {
            PhotoScroll.gameObject.SetActive(false);
        }

        else
        {
            PhotoScroll.gameObject.SetActive(true);
        }
    }
}
