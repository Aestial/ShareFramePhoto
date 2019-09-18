using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasController[] canvasControllers;

    public void SelectCanvas(int index)
    {
        for (int i = 0; i < canvasControllers.Length; i++)
        {
            canvasControllers[i].SetActive(i == index);
        }
    }
}
