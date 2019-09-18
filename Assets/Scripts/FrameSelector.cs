using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameSelector : MonoBehaviour
{
    [SerializeField] Sprite[] frames;
    [SerializeField] Image target;

    public void Select(int index)
    {
        if (index < 0 || index >= frames.Length)
        {
            Debug.LogError("Unable to find selected frame. Using default");
            return;
        }
        target.sprite = frames[index];
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
