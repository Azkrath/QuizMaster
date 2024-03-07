using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    public int totalQuestions = 10;

    public int GetCorrectAnswers() 
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float) totalQuestions * 100);
    }
}
