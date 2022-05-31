using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
using System;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using System.Text;
using UnityEditor;

public class PhotoCapture : MonoBehaviour
{
    public static PhotoCapture instance;
    public ARCameraManager cameraManager;
    Texture2D m_Texture;
    public byte[] targetByte;
    public ArrayList photoList = new ArrayList();
    private string myData;
    public Coroutine getStatusRepeater;
    public Coroutine getStatus;
    public Root root;
    public float repeatTime;
    public Text progress;
    public Text warningText;
    public bool firstclick = false;
    public bool firstFrameReceived;
    public int count = 0;
    public string path;
    public Text objectName;
    public int i = 1;
    public string tempPath;
    public bool control;
    private bool zipControl = false;
    bool init = false;

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

        tempPath = Application.persistentDataPath + "/TempDir";
        control = true;
    }

    //SCREENSHOT
    //public ARCameraBackground m_ARCameraBackground;
    //public RenderTexture renderTexture;
    //private Texture2D m_LastCameraTexture;

    //IEnumerator RecordFrame()
    //{
    //    yield return new WaitForEndOfFrame();
    //    Debug.Log(renderTexture);
    //    Debug.Log(m_ARCameraBackground);
    //    Debug.Log(m_ARCameraBackground.material);
    //    Graphics.Blit(null, renderTexture, m_ARCameraBackground.material);
    //    var activeRenderTexture = RenderTexture.active;
    //    RenderTexture.active = renderTexture;
    //    if (m_LastCameraTexture == null)
    //        m_LastCameraTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, true);
    //    m_LastCameraTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
    //    m_LastCameraTexture.Apply();
    //    RenderTexture.active = activeRenderTexture;
    //    var bytes = m_LastCameraTexture.EncodeToPNG();
    //    var path = Application.persistentDataPath + "/"+ System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")+".png";
    //    File.WriteAllBytes(path, bytes);
    //    Debug.LogError("clicked");
    //    //var texture = ScreenCapture.CaptureScreenshotAsTexture();
    //    //string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    //    //Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(texture, Application.productName + " Captures", name));
    //    //Object.Destroy(texture);
    //    //Debug.LogError("basildi");
    //}
    //public void Capture()
    //{
    //    StartCoroutine(RecordFrame());
    //}


    //SENKRON

    //    public void ControlCameraManager()
    //{
    //    if ((cameraManager == null) || (cameraManager.subsystem == null) || !cameraManager.subsystem.running)
    //    {
    //        return;
    //    }

    //    if (firstclick)

    //        //   return;

    //        using (var configurations = cameraManager.GetConfigurations(Allocator.Temp))
    //        {
    //            /*   if (configurationIndex >= configurations.Length)
    //               {
    //                   return;
    //               }*/

    //            // Get that configuration by index
    //            var configuration = configurations[configurations.Length - 1];

    //            // Make it the active one
    //            cameraManager.currentConfiguration = configuration;
    //        }
    //    firstclick = true;
    //}

    //public unsafe void OnCameraFrameReceived()
    //{
    //    if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
    //        return;

    //    // cameraManager.subsystem.currentConfiguration = cameraManager.GetConfigurations(Allocator.Temp)[1]; //In my case 0=640*480, 1= 1280*720, 2=1920*108

    //    var conversionParams = new XRCpuImage.ConversionParams
    //    {
    //        // Get the entire image.
    //        inputRect = new RectInt(0, 0, image.width, image.height),

    //        // Downsample by 2.
    //        outputDimensions = new Vector2Int(image.width, image.height),

    //        // Choose RGBA format.
    //        outputFormat = TextureFormat.RGBA32,

    //        // Flip across the vertical axis (mirror image).
    //        transformation = XRCpuImage.Transformation.MirrorY
    //    };

    //    // See how many bytes you need to store the final image.
    //    int size = image.GetConvertedDataSize(conversionParams);

    //    // Allocate a buffer to store the image.
    //    var buffer = new NativeArray<byte>(size, Allocator.Temp);

    //    // Extract the image data
    //    image.Convert(conversionParams, new IntPtr(buffer.GetUnsafePtr()), buffer.Length);

    //    // The image was converted to RGBA32 format and written into the provided buffer
    //    // so you can dispose of the XRCpuImage. You must do this or it will leak resources.
    //    image.Dispose();

    //    // At this point, you can process the image, pass it to a computer vision algorithm, etc.
    //    // In this example, you apply it to a texture to visualize it.

    //    // You've got the data; let's put it into a texture so you can visualize it.

    //    m_Texture = new Texture2D(
    //         conversionParams.outputDimensions.x,
    //         conversionParams.outputDimensions.y,
    //         conversionParams.outputFormat,
    //         false);

    //    m_Texture.LoadRawTextureData(buffer);
    //    m_Texture.Apply();


    //    // Done with your temporary data, so you can dispose it.
    //    targetByte = m_Texture.EncodeToPNG();
    //    photoList.Add(targetByte);

    //    // File.WriteAllBytes(path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png", targetByte);

    //    buffer.Dispose();
    //}


    void OnEnable()
    {
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;

    }
    void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        if (!init)
        {
            cameraManager.subsystem.currentConfiguration = cameraManager.GetConfigurations(Allocator.Temp)[2]; // 0=640*480, 1= 1280*720, 2=1920*1080
            init = true;
        }

    }
    public void GetImageAsync()
    {
        // Get information about the device camera image.
        if (cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            // If successful, launch a coroutine that waits for the image
            // to be ready, then apply it to a texture.
            StartCoroutine(ProcessImage(image));

            // It's safe to dispose the image before the async operation completes.
            image.Dispose();
        }
    }

    IEnumerator ProcessImage(XRCpuImage image)
    {
        // Create the async conversion request.
        var request = image.ConvertAsync(new XRCpuImage.ConversionParams
        {
            // Use the full image.
            inputRect = new RectInt(0, 0, image.width, image.height),

            // Downsample by 2.
            outputDimensions = new Vector2Int(image.width, image.height),

            // Color image format.
            outputFormat = TextureFormat.RGBA32,

            // Flip across the Y axis.
            transformation = XRCpuImage.Transformation.MirrorY
        });

        // Wait for the conversion to complete.
        while (!request.status.IsDone())
            yield return null;

        // Check status to see if the conversion completed successfully.
        if (request.status != XRCpuImage.AsyncConversionStatus.Ready)
        {
            // Something went wrong.
            Debug.LogErrorFormat("Request failed with status {0}", request.status);

            // Dispose even if there is an error.
            request.Dispose();
            yield break;
        }

        // Image data is ready. Let's apply it to a Texture2D.
        var rawData = request.GetData<byte>();

        // Create a texture if necessary.
        if (m_Texture == null)
        {
            m_Texture = new Texture2D(
                request.conversionParams.outputDimensions.x,
                request.conversionParams.outputDimensions.y,
                request.conversionParams.outputFormat,
                false);
        }

        // Copy the image data into the texture.
        m_Texture.LoadRawTextureData(rawData);
        m_Texture.Apply();
        targetByte = m_Texture.EncodeToPNG();
        photoList.Add(targetByte);

        // Need to dispose the request to delete resources associated
        // with the request, including the raw data.
        request.Dispose();
    }

    public void Ziple()
    {
        if (photoList.Count == ObjectPlacement2.instance.PhotoShotNumber*2) //are numbers equal?
        {
            PlacementIndicator.instance.visual.SetActive(true);
            warningText.text = "Lütfen Bekleyin!";

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            if (!Directory.Exists(tempPath + "/Photos"))
            {
                Directory.CreateDirectory(tempPath + "/Photos");
            }


            if (File.Exists(tempPath + "/Zips.zip"))
            {
                File.Delete(tempPath + "/Zips.zip");  //Eski zip siliniyor
            }

            foreach (byte[] item in photoList)
            {
                File.WriteAllBytes(tempPath + "/Photos/" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + i + ".png", item);
            
                i++;
            }

            string CompressingFile = Path.Combine(tempPath + "/Photos/"); //directory that will compress
            string directoryName = Path.GetDirectoryName(CompressingFile);
            string saveLocation = Path.Combine(tempPath + "/Zips.zip");  //Zip name
            System.IO.Compression.ZipFile.CreateFromDirectory(directoryName, saveLocation);
            myData = saveLocation;
            warningText.text = "Fotoðraflar sýkýþtýrýldý, þimdi gönderebilirsiniz!";

            // StartCoroutine(GetToken());  //send Zip to Api
        }
        else
        {
            warningText.text = "Çekilmesi gereken poz sayýsý:  " + ObjectPlacement2.instance.PhotoShotNumber + "  Çekilen  " + photoList.Count;
        }

        zipControl = true;

    }

    public void Add()
    {

        if (zipControl)
        {
            warningText.text = "Lütfen Bekleyin!";
            StartCoroutine(GetToken());


        }
        else
        {
            warningText.text = "önce fotoðraflarý sýkýþtýrmanýz gerekiyor!";
        }


    }

    public IEnumerator GetToken()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // OnLoginFail("Ýnternet baðlantýsý yok!");
        }

        // Debug.Log(url + loginAPI);
        // Debug.Log(_loginJson);

        // byte[] bytes = myData;
        List<IMultipartFormSection> form = new List<IMultipartFormSection>
        {
            new MultipartFormFileSection("file", File.ReadAllBytes(myData), "Zips.zip", "application/octet-stream")
        };
        // generate a boundary then convert the form to byte[]
        byte[] boundary = UnityWebRequest.GenerateBoundary();
        byte[] formSections = UnityWebRequest.SerializeFormSections(form, boundary);
        // my termination string consisting of CRLF--{boundary}--
        byte[] terminate = Encoding.UTF8.GetBytes(String.Concat("\r\n--", Encoding.UTF8.GetString(boundary), "--"));
        // Make my complete body from the two byte arrays
        byte[] body = new byte[formSections.Length + terminate.Length];
        Buffer.BlockCopy(formSections, 0, body, 0, formSections.Length);
        Buffer.BlockCopy(terminate, 0, body, formSections.Length, terminate.Length);
        // Set the content type - NO QUOTES around the boundary
        string contentType = String.Concat("multipart/form-data; boundary=", Encoding.UTF8.GetString(boundary));
        UnityWebRequest www = new UnityWebRequest("https://api2.spectre3d.io/scan", "POST");
        UploadHandler uploader = new UploadHandlerRaw(body);
        uploader.contentType = contentType;
        www.uploadHandler = uploader;
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        Debug.Log("request completed with code: " + www.responseCode);

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }

        else
        {
            Debug.Log(www.downloadHandler.text);
            root = JsonUtility.FromJson<Root>(www.downloadHandler.text);
           // getStatusRepeater = StartCoroutine(GetStatusRepeater(root.id)); //Root id eriþim
           getStatus = StartCoroutine(GetStatus(root.id,true));
        }

    }
    //public IEnumerator GetStatusRepeater(string rootID)
    //{
    //    while (true)
    //    {
    //        StartCoroutine(GetStatus(rootID));
    //        yield return new WaitForSeconds(repeatTime);
    //    }
    //}

    public IEnumerator GetUrls(string targetId)
    {
        var combinedURL = "https://api2.spectre3d.io/Scan/" + targetId;
        Debug.Log(combinedURL);


        UnityWebRequest www = UnityWebRequest.Get(combinedURL);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("content-type", "application/json");
        www.SetRequestHeader("x-requested-with", "XMLHttpRequest");

        // StartCoroutine(WatForResponse(webRequest));
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.LogError(www.downloadHandler.text);
            root = JsonUtility.FromJson<Root>(www.downloadHandler.text);
        }

        //  yield return new WaitForSeconds(1);
    }

    public IEnumerator GetStatus(string targetId , bool firstTime)
    {
        string _targetId = targetId;

        if (!firstTime)
        {
            yield return new WaitForSeconds(repeatTime);
        }
      
        //while (control)
        //{
        //    control = false;
        //    StartCoroutine(GetStatus(targetId));
        //    control = true;
        //    yield return new WaitForSeconds(repeatTime);

        //}

        var combinedURL = "https://api2.spectre3d.io/Scan/" + targetId + " /status/";
        Debug.Log(combinedURL);

        UnityWebRequest www = UnityWebRequest.Get(combinedURL);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("content-type", "application/json");
        www.SetRequestHeader("x-requested-with", "XMLHttpRequest");
        // warningText.text = ("Photos are being processed!");
        warningText.text = ("Gönderiliyor!");

        // StartCoroutine(WatForResponse(webRequest));
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Debug.LogError("error" + www.error);
            Debug.LogError("error");

        }
        else
        {
            Debug.LogError(www.downloadHandler.text);
            root = JsonUtility.FromJson<Root>(www.downloadHandler.text);
            //progress.text = root.progress.ToString();

            if (root.jobStatus == "Completed")
            {
                warningText.text = ("Gönderme iþlemi baþarýlý!");

                System.IO.Directory.Move(tempPath, Application.persistentDataPath + "/" + root.id);
                path = Application.persistentDataPath + "/" + root.id;

               // PhotoButtonController.instance.Initialized(objectName.text, path);
                Json.instance.Write(objectName.text, root.id, path); //write , read and create button
             

                zipControl = false;

                StartCoroutine(GetUrls(root.id));
                // StopCoroutine(getStatusRepeater);

                StopCoroutine(getStatus); //Progress tamamlanýnca tekrar istemeyi durduruyor 


          

                yield break;
            }
            else
            {
                if(root.jobStatus == "Failed")

                {
                    warningText.text = "Hata oluþtu lütfen tekrar fotoðraf çekin!";

                    //StopCoroutine(getStatusRepeater);


                    StopCoroutine(getStatus);

                    if (Directory.Exists(Application.persistentDataPath + "/TempDir/Photos"))
                    {
                        System.IO.DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/TempDir/Photos");

                        foreach (FileInfo file in dir.GetFiles())
                        {
                            if (file != null)
                            {
                                file.Delete();
                            }

                        }
                    }

                    if (File.Exists(Application.persistentDataPath + "/TempDir/Zips.zip"))
                    {
                        File.Delete(Application.persistentDataPath + "/TempDir/Zips.zip");  //delete old zip files
                    }

                    zipControl = false;
               
                    yield break;
                }

                else if(root.jobStatus == "Queued")
                {
                    StartCoroutine(GetStatus(_targetId,false));

                   yield break;
                 
                }

                else
                {
             
                    warningText.text = ( "%" + (root.progress)*100 );

                    StartCoroutine(GetStatus(_targetId,false));

                    yield break;

                }
               
            }
        }

       //yield return new WaitForSeconds(10);
    }

    public class Root
    {
        public float progress;
        public string id;
        public string usdzUrl;
        public string objUrl;
        public string glbUrl;
        public string jobStatus;

    }
}
