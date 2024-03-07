using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCanvasRegister : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.quizCanvas = GetComponent<Canvas>();
    }
}