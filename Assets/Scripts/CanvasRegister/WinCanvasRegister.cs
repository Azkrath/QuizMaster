using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCanvasRegister : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.winCanvas = GetComponent<Canvas>();
    }
}
