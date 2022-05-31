using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleSetting : MonoBehaviour
{

     RaycastHit obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ray sendRay = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //if(Physics.Raycast(sendRay,out obj))
        //{
        //    if(obj.collider.gameObject.tag == "photoPoint")
        //    {
        //        if(obj.collider.gameObject.transform.localEulerAngles.y > GameObject.FindWithTag("MainCamera").GetComponent<Camera>().transform.localEulerAngles.y-5 &&
        //            obj.collider.gameObject.transform.localEulerAngles.y < GameObject.FindWithTag("MainCamera").GetComponent<Camera>().transform.localEulerAngles.y + 5)
        //        {
        //            if(obj.collider.gameObject.transform.localEulerAngles.z > GameObject.FindWithTag("MainCamera").GetComponent<Camera>().transform.localEulerAngles.z - 5 &&
        //            obj.collider.gameObject.transform.localEulerAngles.z < GameObject.FindWithTag("MainCamera").GetComponent<Camera>().transform.localEulerAngles.z + 5)
        //            {
        //                obj.collider.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>.enabled =
        //                Debug.Log("nesneye çarpýldý");
        //            }
                
        //        }

        //        else
        //        {

        //        }
             
   
        //    }
        //}
    }

}


