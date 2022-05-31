using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class Json : MonoBehaviour

{
    public static Json instance;

    private PhotoDataArray photoDataArray;
    private PhotoDataArray returnedData;
    private PhotoData temp = new PhotoData();
    private List<PhotoButtonController> allButtons = new List<PhotoButtonController>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }

        photoDataArray = new PhotoDataArray();
        returnedData = new PhotoDataArray();
        photoDataArray.photos = new List<PhotoData>();

        Read();

    }

    public void Write(string objName,string rootId,string path)
    {
        
        temp.objName = objName;
        temp.id = rootId;
        temp.path = path;

        photoDataArray.photos.Add(temp);
          
        string multipleDataPath = Application.persistentDataPath + "/multipleData.json";

            var json = JsonUtility.ToJson(photoDataArray, true);
            System.IO.File.WriteAllText(multipleDataPath, json);

            photoDataArray.photos.Clear();

        Read();

    }

    public void Read()
    {
        
        if (File.Exists(Application.persistentDataPath + "/multipleData.json"))
        {
            string multipleDataPath = Application.persistentDataPath + "/multipleData.json";

            var jsonData = System.IO.File.ReadAllText(multipleDataPath);
           
          
            returnedData = JsonConvert.DeserializeObject<PhotoDataArray>(jsonData);
           

            photoDataArray.photos.Clear(); //yazılan okunurken  de tekrar yazılmasın diye

              DeleteButton();
            
            
            foreach (PhotoData item in returnedData.photos)
            {
                Debug.Log("returneddata" + item.path + item.objName);
                CreateButton(item.objName, item.id, item.path);
                photoDataArray.photos.Add(item);

            }

        }

    }

    private void CreateButton(string objName, string rootId , string path)
    {
        PhotoButtonController newButton = Instantiate(PhotoManager.instance.PhotoButton, PhotoManager.instance.PhotoListPanel).GetComponent<PhotoButtonController>();
        newButton.transform.SetParent(PhotoManager.instance.PhotoListPanel);
        newButton.Initialized(objName, rootId , path);
        allButtons.Add(newButton);
        Debug.Log("created");
    }

    private void DeleteButton()
    {
        foreach (Transform child in PhotoManager.instance.content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        allButtons.Clear();
    }

}

    [Serializable]
    public class PhotoData
    {
        public string objName;
        public string path;
        public string id;
    }

    [Serializable]
    public class PhotoDataArray
    {
        public List<PhotoData> photos;
    }