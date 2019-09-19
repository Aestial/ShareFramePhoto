using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CaptureAndSavePhoto : MonoBehaviour
{    
    [SerializeField] RectTransform captureRect;
    [SerializeField] Canvas canvas;
    [SerializeField] RawImage target;
    [Header("Callback Actions")]
    [SerializeField] UnityEvent onScreenShotAction;

    CaptureAndSave snapshot;
    Texture2D texture;
    Rect selection;    

    public void TakePicture()
    {
        selection = RectTransformUtility.PixelAdjustRect(captureRect, canvas);
        Debug.Log(selection);

        int width = (int)selection.width;
        int height = (int)selection.height;
        int x = (int)(canvas.pixelRect.width/2.0f) + (int)selection.x;
        //int y = (int)(canvas.pixelRect.height / 2.0f - (height / 2.0f) + selection.y);
        int y = (int)(canvas.pixelRect.height / 2.0f) + (int)selection.y - 120;
        Rect rect = new Rect(x, y, width, height);
        Debug.Log(rect);
        //snapshot.CaptureAndSaveToAlbum(x, y, width, height, ImageType.PNG);
        snapshot.GetScreenShot(x, y, width, height, ImageType.PNG);
    }
    void Start()
    {
        snapshot = GetComponent<CaptureAndSave>();    
    }
    void OnEnable()
    {
        CaptureAndSaveEventListener.onError += OnError;
        CaptureAndSaveEventListener.onSuccess += OnSuccess;
        CaptureAndSaveEventListener.onScreenShot += OnScreenShot;
    }    
    void OnDisable()
    {
        CaptureAndSaveEventListener.onError -= OnError;
        CaptureAndSaveEventListener.onSuccess -= OnSuccess;
        CaptureAndSaveEventListener.onScreenShot -= OnScreenShot;
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
        Debug.Log("Got ScreenShot Texture : " + tex2D);
        texture = tex2D;
        target.texture = texture;
        onScreenShotAction.Invoke();
    }
    void Update()
    {
        
    }
}
