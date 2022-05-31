using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AccessCameraController : MonoBehaviour
{
    RaycastHit obj;
    Camera camera;
    float rotationObjZ;
    float rotationObjY;
    float rotationCameraX;
    float rotationCameraY;

    private void Start()
    {
        Debug.Log("olu�tu");
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    //public void Access()
    //{
    //    PhotoCapture.instance.ControlCameraManager();
    //    Destroy(gameObject);

    //}

    //public void TakePhoto()
    //{
    //    PhotoCapture.instance.TakePhoto();
    //}

    public void TakePhoto()
    {
        PhotoCapture.instance.GetImageAsync();
        Destroy(gameObject);
    }

    void Update()
    {
       
        Ray sendRay = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawLine(sendRay.origin, camera.transform.forward * 50000000, Color.green);

        if (Physics.Raycast(sendRay,out obj,0.5f))
        {
            if (obj.collider.gameObject.tag == "photoPoint")
            {
           
                rotationObjZ = obj.collider.gameObject.transform.localEulerAngles.z;
                rotationCameraX= camera.transform.localEulerAngles.x;
                rotationObjY = obj.collider.gameObject.transform.localEulerAngles.y;
                rotationCameraY = camera.transform.localEulerAngles.y;

                if (rotationCameraX >= 180f)
                {
                    rotationCameraX = rotationCameraX - 360f;
                }
                if (rotationObjZ >= 180f)
                {
                    rotationObjZ = rotationObjZ - 360f;
                }
                //if (rotationCameraY >= 180f)
                //{
                //    rotationCameraY = rotationCameraY - 360f;
                //}

                //if (rotationObjY >= 180f)
                //{
                //    rotationObjY = rotationObjY - 360f;
                //}

                PhotoCapture.instance.warningText.text = "objZ" + rotationObjZ.ToString() + "camX" + rotationCameraX.ToString() + "camY" + rotationCameraY.ToString() + "objY" + rotationObjY.ToString();
 
                if(rotationObjZ + rotationCameraX  > -15 && rotationObjZ+ rotationCameraX < 15) //ARADAK� 30 DERECE A�IYI TUTTURMAK ���N,normalde toplam 0 olmal�
                {

                    //if(rotationCameraY - rotationObjY > 85 && rotationCameraY - rotationObjY < 95) //ARADAK� 40 DERECE A�IYI TUTTURMAK ���N,normalde fark 90 olaml�.
                    //                                                                                //Ar cameran�n y rotation de�eri ilk ba�ta 90 derece olmal�
                    //{
                        Debug.Log("nesneye �arp�ld�");
                        obj.collider.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                        //StartCoroutine(ButtonActivativity());
                    //}
                   
                }
                else
                {
                    obj.collider.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
            }

        }
    }

    IEnumerator ButtonActivity()
    {
        obj.collider.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        obj.collider.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
}
