using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureAndSavePhoto : MonoBehaviour
{
    [SerializeField] CaptureAndSave snapshot;
    //[SerializeField] Rect selection;
    [SerializeField] RectTransform target;
    [SerializeField] Canvas canvas;

    Rect selection;
    Texture2D texture;

    public void TakePicture()
    {
        selection = RectTransformUtility.PixelAdjustRect(target, canvas);
        Debug.Log(selection);

        int width = (int)selection.width;
        int height = (int)selection.height;
        int x = (int)(canvas.pixelRect.width/2.0f) + (int)selection.x;
        int y = (int)-selection.y + 69;
        //int y = (int)(canvas.pixelRect.height / 2.0f - (height / 2.0f) + selection.y);
        //int y = (int)(canvas.pixelRect.height / 2.0f) + (int)selection.y;

        Rect rect = new Rect(x, y, width, height);
        Debug.Log(rect);

        snapshot.CaptureAndSaveToAlbum(x, y, width, height, ImageType.JPG);
    }
    void OnEnable()
    {
        CaptureAndSaveEventListener.onError += OnError;
        CaptureAndSaveEventListener.onSuccess += OnSuccess;
        CaptureAndSaveEventListener.onScreenShotInvoker += OnScreenShot;
    }    
    void OnDisable()
    {
        CaptureAndSaveEventListener.onError -= OnError;
        CaptureAndSaveEventListener.onSuccess -= OnSuccess;
        CaptureAndSaveEventListener.onScreenShotInvoker -= OnScreenShot;
    }    
    private void OnError(string err)
    {
        //log += "\n" + error;
        Debug.Log("Error : " + err);
    }
    private void OnSuccess(string msg)
    {
        //log += "\n" + msg;
        Debug.Log("Success : " + msg);
    }
    private void OnScreenShot(Texture2D tex2D)
    {
        texture = tex2D;
    }
    void Update()
    {
        
    }
}
