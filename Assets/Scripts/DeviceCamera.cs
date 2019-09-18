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
                camTexture = new WebCamTexture(devices[i].name, Screen.width*2, Screen.height*2);
            }
        }
        if (camTexture == null)
        {
            Debug.LogError("Unable to find desired camera");
            return;
        }
        camTexture.Play();
        feedTarget.texture = camTexture;
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

        float ratio = (float)camTexture.width / (float)camTexture.height;
        fitter.aspectRatio = ratio;

        float scaleY = camTexture.videoVerticallyMirrored ? -1f : 1f;
        feedTarget.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        float rotAngle = -camTexture.videoRotationAngle;
        feedTarget.rectTransform.localEulerAngles = new Vector3(0, 0, rotAngle);
    }
}
