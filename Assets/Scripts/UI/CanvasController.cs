using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] bool activeOnAwake;

    private Canvas canvas;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = activeOnAwake;    
    }

    public void SetActive(bool isActive)
    {
        canvas.enabled = isActive;
    }
}
