using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    public static PhotoManager instance;
    public GameObject PhotoButton;
    public Transform PhotoListPanel;
    public GameObject newPhotoButton;
   public GameObject content;

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

    public void NewObject()
    {
        PhotoCapture.instance.tempPath = Application.persistentDataPath + "/TempDir";
        PhotoCapture.instance.photoList.Clear();
        StartCoroutine(NewObjectPhotoAdd());

    }

    public IEnumerator NewObjectPhotoAdd()
    {
        PhotoCapture.instance.warningText.text = "Yeni obje için fotoðraf çekebilirsiniz!";
        yield return new WaitForSeconds(3);
        PhotoCapture.instance.warningText.text = "";
    }

}
