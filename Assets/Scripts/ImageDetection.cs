using IBM.Cloud.SDK;
using IBM.Watson;
using IBM.Watson.Assistant.V2.Model;
using IBM.Watson.VisualRecognition.V3;
using IBM.Watson.VisualRecognition.V3.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageDetection : MonoBehaviour
{

    VisualRecognitionService VisualRecognition;

    public UnityEngine.UI.Image camera;
    public GameObject Details;
    public GameObject Edible;

    void Start()
    {
        Credentials credentials = new Credentials("monoblanco", "Sebas0126683","https://gateway.watsonplatform.net/visual-recognition/api");
        VisualRecognition = new VisualRecognitionService("2019-06-28", credentials);
        WebCamTexture cam = new WebCamTexture();
        cam.Play();
        camera.material.mainTexture = cam;

    }

    private void OnMessage(DetailedResponse<DetectedFaces> response, IBMError error)
    {
        print(response.ToString());
    }

    public void Find()
    {
        ScreenCapture.CaptureScreenshot("screenshot.png");
        StartCoroutine("DetailsMove");
        StartCoroutine("EdibleMove");
        FileStream file = new FileStream("E:/Unity Projects/New Unity Project/screenshot.png", FileMode.Open);
        byte[] bytes = new byte[file.Length];
        file.Read(bytes, 0, (int)file.Length);
        file.Close();
        MemoryStream stream = new MemoryStream(bytes);
        VisualRecognition.DetectFaces(callback: OnMessage, stream, "E:/Unity Projects/New Unity Project/screenshot.png");
    }

    IEnumerator DetailsMove()
    {
        for (int i = 0; i < 175; i++)
        {
            Details.transform.position += new Vector3(0, 4, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator EdibleMove()
    {
        for (int i = 0; i < 175; i++)
        {
            Edible.transform.position += new Vector3(-2.5f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

}

