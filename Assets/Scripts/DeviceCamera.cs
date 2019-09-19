using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceCamera : MonoBehaviour
{
    [SerializeField] bool isFrontFace;
    [SerializeField] RawImage feedTarget;
    [SerializeField] AspectRatioFitter fitter;

    private bool isCamAvailable;
    private WebCamTexture camTexture;
    private Texture defaultBackground;

    void Start()
    {
        defaultBackground = feedTarget.texture;
        isFrontFace |= Application.isEditor;
        PrintDevices();
        StartCamera();        
    }

    private void StartCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length <= 0)
        {
            Debug.LogError("No camera detected");
            isCamAvailable = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == isFrontFace)
            {
                //Rect r = feedTarget.rectTransform.rect; // Same as in CaptureAndSavePhoto.cs
                //Debug.Log("Feed target rect: " + r);
                camTexture = new WebCamTexture(devices[i].name, 1920, 1080, 60);
            }
        }
        if (camTexture == null)
        {
            Debug.LogError("Unable to find desired camera");
            return;
        }
        camTexture.Play();
        feedTarget.texture = camTexture;
        //feedTarget.material.SetTextureScale("_Texture", new Vector2(1f, 1f));
        isCamAvailable = true;
    }

    private void PrintDevices()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
            Debug.Log(devices[i].name);
    }

    void Update()
    {
        if (!isCamAvailable)
            return;

        Debug.Log("Camera feed width: " + camTexture.width + ", height: " + camTexture.height);

        float ratio = (float)camTexture.width / (float)camTexture.height;
        fitter.aspectRatio = ratio;

        float scaleX = isFrontFace ? -1f : 1f;
        float scaleY = camTexture.videoVerticallyMirrored ? -1f : 1f;
        feedTarget.rectTransform.localScale = new Vector3(scaleX, scaleY, 1f);

        float rotAngle = -camTexture.videoRotationAngle;
        feedTarget.rectTransform.localEulerAngles = new Vector3(0, 0, rotAngle);

        feedTarget.material.SetTextureScale("_Texture", new Vector2(1f, 1f));
    }
}
