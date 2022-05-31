using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement2 : MonoBehaviour
{
        public static ObjectPlacement2 instance;
        public GameObject plot;
        public PlacementIndicator pI;
        private GameObject plus;
        public List<GameObject> photoDestroy = new List<GameObject>();
        private GameObject tempPhoto;
        RaycastHit hit;
        public GameObject PhotoPointPrefab;
        public int PhotoShotNumber;
        private int angleValue;
        private bool control = true;
        public GameObject AllPhotoPrefab;
        public float spawnAngleValue;
           


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
            plot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            plot.transform.rotation = Quaternion.Euler(0, 0, 0);
            pI = FindObjectOfType<PlacementIndicator>();
            angleValue = 360 / PhotoShotNumber;
       // plot.transform.localScale = new Vector3(PlacementIndicator.instance.visual.gameObject.transform.localScale.x, PlacementIndicator.instance.visual.gameObject.transform.localScale.y, PlacementIndicator.instance.visual.gameObject.transform.localScale.z);

        }


    //private void Update()
    //{
    //    float eulerAngX = GameObject.FindWithTag("MainCamera").transform.localEulerAngles.x;
    //   // PhotoCapture.instance.warningText.text = eulerAngX.ToString();

    //    if (photoDestroy != null)
    //    {

    //        if (eulerAngX > 30f && eulerAngX < 45f)
    //        {

    //            foreach (var item in photoDestroy)
    //            {
    //                if (item != null)
    //                {
    //                    var itemRenderer = item.gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
    //                    //var itemRenderer = item.GetComponent<Renderer>();
    //                    //  itemRenderer.material.SetColor("_Color", Color.blue);
    //                    itemRenderer.material.color = new Color( 1.02f, 2.55f, 2.55f, 0.4f );
    //                    // item.SetActive(true);
    //                }

    //            }
    //        }
    //        else
    //        {

    //            foreach (var item in photoDestroy)
    //            {
    //                if (item != null)
    //                {
    //                    var itemRenderer = item.gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
    //                    // var itemRenderer = item.GetComponent<Renderer>();
    //                    // itemRenderer.material.SetColor("_Color", Color.white);
    //                    itemRenderer.material.color = new Color( 0f, 0f, 0f, 0.5f );
    //                    //item.SetActive(false);
    //                }

    //            }
    //        }
    //    }
    //}

    public void Reset()
        {
       plot.transform.localScale = new Vector3(PlacementIndicator.instance.visual.gameObject.transform.localScale.x, PlacementIndicator.instance.visual.gameObject.transform.localScale.y, PlacementIndicator.instance.visual.gameObject.transform.localScale.z);
        if (!control)
            {
                foreach (var item in photoDestroy)
                {
                    Destroy(item.gameObject);
                }
                photoDestroy.Clear();

                control = true;

                PlacementIndicator.instance.visual.GetComponent<MeshRenderer>().enabled = true;
                //  PlacementIndicator.instance.visual.SetActive(true);

                Destroy(plus);
            }

            //pI.plane.requestedDetectionMode = PlaneDetectionMode.Horizontal;

            else
            {
                PlacementIndicator.instance.visual.GetComponent<MeshRenderer>().enabled = false;
                // PlacementIndicator.instance.visual.SetActive(false);
                PhotoCapture.instance.count = 0;
                int addAngleValue = angleValue;
                int addAngleValueTop = angleValue;
                //  plus = Instantiate(plot, pI.transform.position, Quaternion.Euler(0, 0, 0));
                plus = Instantiate(plot, pI.transform.position, pI.transform.rotation);
                plus.transform.SetParent(AllPhotoPrefab.transform);

                for (int i = 1; i <= PhotoShotNumber; i++)
                {
                   spawnAngleValue = 0f;
                    if (i == 1)
                    {
                        tempPhoto = Instantiate(PhotoPointPrefab, new Vector3(PlacementIndicator.instance.visual.transform.position.x, PlacementIndicator.instance.visual.transform.position.y, PlacementIndicator.instance.visual.transform.position.z), Quaternion.Euler(0, plot.transform.localEulerAngles.y + 0, plot.transform.localEulerAngles.z+spawnAngleValue));             
                        tempPhoto.transform.SetParent(AllPhotoPrefab.transform);
                        tempPhoto.transform.localScale = PlacementIndicator.instance.visual.transform.localScale;
                        photoDestroy.Add(tempPhoto);
                                         
                    }

                    else
                    {
                        tempPhoto = Instantiate(PhotoPointPrefab, new Vector3(PlacementIndicator.instance.visual.transform.position.x, PlacementIndicator.instance.visual.transform.position.y , PlacementIndicator.instance.visual.transform.position.z), Quaternion.Euler(plot.transform.localEulerAngles.x, plot.transform.localEulerAngles.y + addAngleValue, plot.transform.localEulerAngles.z+spawnAngleValue));
                        photoDestroy.Add(tempPhoto);
                        tempPhoto.transform.SetParent(AllPhotoPrefab.transform);
                        tempPhoto.transform.localScale = PlacementIndicator.instance.visual.transform.localScale;

                    addAngleValue += angleValue;

                    }
                }

            for (int i = 1; i <= PhotoShotNumber; i++)
            {
                spawnAngleValue = 30f;
                if (i == 1)
                {
                    tempPhoto = Instantiate(PhotoPointPrefab, new Vector3(PlacementIndicator.instance.visual.transform.position.x, PlacementIndicator.instance.visual.transform.position.y + 0.09f, PlacementIndicator.instance.visual.transform.position.z), Quaternion.Euler(0, plot.transform.localEulerAngles.y + 0, plot.transform.localEulerAngles.z-spawnAngleValue));
                    tempPhoto.transform.SetParent(AllPhotoPrefab.transform);
                    tempPhoto.transform.localScale = PlacementIndicator.instance.visual.transform.localScale;
                    photoDestroy.Add(tempPhoto);

                }

                else
                {
                    tempPhoto = Instantiate(PhotoPointPrefab, new Vector3(PlacementIndicator.instance.visual.transform.position.x, PlacementIndicator.instance.visual.transform.position.y + 0.09f, PlacementIndicator.instance.visual.transform.position.z), Quaternion.Euler(plot.transform.localEulerAngles.x, plot.transform.localEulerAngles.y + addAngleValueTop, plot.transform.localEulerAngles.z-spawnAngleValue));
                    photoDestroy.Add(tempPhoto);
                    tempPhoto.transform.SetParent(AllPhotoPrefab.transform);
                    tempPhoto.transform.localScale = PlacementIndicator.instance.visual.transform.localScale;

                    addAngleValueTop += angleValue;

                }
            }

            PhotoCapture.instance.photoList.Clear();
                control = false;

                //pI.plane.requestedDetectionMode = PlaneDetectionMode.None;
            }
        }

        public void DestroyPhotoPoint()
        {
            Destroy(gameObject);
        }
    }




