using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoListManager : MonoBehaviour
{
    public static PhotoListManager instance;
    public GameObject PhotoImage;
    public Transform PhotoListPanel;
    public GameObject content;
    public List<PhotoListController> photos = new List<PhotoListController>();

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
    public void CreateImageList(byte[] image)
    {
        PhotoListController newImage = Instantiate(PhotoImage , PhotoListPanel).GetComponent<PhotoListController>();
        newImage.transform.SetParent(PhotoListPanel);
        Texture2D loadTexture = new Texture2D(2, 2);
        loadTexture.LoadImage(image);
        newImage.SetThumbnail(Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), Vector2.zero));
        photos.Add(newImage);

        Debug.Log("createdImage");
    }

}
