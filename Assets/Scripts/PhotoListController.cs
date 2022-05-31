using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoListController : MonoBehaviour
{
    public Image photoThumbnail;

     public void SetThumbnail(Sprite sprite)
        {
        photoThumbnail.sprite = sprite;

        }
}
