 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        
        EventTrigger eventTrigger = GetComponentInChildren<EventTrigger>();
        EventTrigger.Entry eventTriggerEntry = new()
        {
            eventID = EventTriggerType.PointerUp
        };

        eventTriggerEntry.callback.AddListener((eventData) => { GameManager.instance.OnReplayLevel(); });
        eventTrigger.triggers.Add(eventTriggerEntry);
    }

    public void ShowFinalScore()
    {
        int score = scoreKeeper.CalculateScore();
        finalScoreText.text = "Parabéns!\nConseguiste uma pontuação de " + score + "%";
    }
}
