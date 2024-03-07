using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvasRegister : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.startCanvas = GetComponent<Canvas>();
    }
}
