using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsCanvasRegister : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.optionsCanvas = GetComponent<Canvas>();
    }
}
