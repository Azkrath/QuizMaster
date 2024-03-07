using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Question
{
    public string question;
    public string[] options;
    public int correctAnswer;

    internal readonly string GetQuestion()
    {
        return question;
    }

    internal readonly string GetAnswer(int index)
    {
        return options[index];
    }

    internal readonly int GetCorrectAnswerIndex()
    {
        return correctAnswer;
    }

}

[Serializable]
public class Questions
{
    public List<Question> questions; 
}


