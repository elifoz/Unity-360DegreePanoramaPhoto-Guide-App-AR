using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PhotoButtonController : MonoBehaviour
{
    public static PhotoButtonController instance;

    public Text   objectName;
    private string photoPath;
    private string rootID;
    private int i = 1;

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
    public void Initialized(string objName, string rootId , string path)
    {

        objectName.text = objName;
        photoPath = path;
        rootID = rootId;

    }

    public void GoDirectory()
    {
        foreach (Transform child in PhotoListManager.instance.content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        PhotoListManager.instance.photos.Clear();
        
        string tempPath = Application.persistentDataPath + "/TempDir";

        PhotoCapture.instance.tempPath = tempPath;
        PhotoCapture.instance.objectName.text = objectName.text;


        if (!Directory.Exists(tempPath))
        {
            Directory.CreateDirectory(tempPath);
        }


        if (!Directory.Exists(tempPath + "/Photos"))
        {
            Directory.CreateDirectory(tempPath + "/Photos");
        }

        if (Directory.Exists(tempPath + "/Photos"))
        {
            System.IO.DirectoryInfo dir = new DirectoryInfo(tempPath + "/Photos");

            foreach (FileInfo file in dir.GetFiles())
            {
                if (file != null)
                {
                    file.Delete();
                }

            }
        }

        if (File.Exists(tempPath + "/Zips.zip"))
        {
            File.Delete(tempPath + "/Zips.zip");  //delete old zip files
        }



        string[] array = Directory.GetFiles(photoPath + "/Photos");

        foreach (string file in array)
        {
            byte[] tempPhoto = File.ReadAllBytes(file);

            if (tempPhoto != null)
            {
                PhotoListManager.instance.CreateImageList(tempPhoto); //to show photos
            }
      

            File.WriteAllBytes(tempPath + "/Photos/" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + i + ".png", tempPhoto);

            i++;

            Debug.Log("tempPhoto" + tempPhoto);
        }

    

    }

    public void Info()
    {

        StartCoroutine(NewPhotoAdd());
    }

    public IEnumerator NewPhotoAdd()
    {
        PhotoCapture.instance.warningText.text = objectName.text + " " + "nesnesine ekleme yapabilirsiniz!" + rootID;
        yield return new WaitForSeconds(3);
        PhotoCapture.instance.warningText.text = "";
    }

  
}