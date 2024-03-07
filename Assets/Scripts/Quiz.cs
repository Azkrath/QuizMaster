using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] int totalQuestions = 10;
    Questions questionList;
    Question currentQuestion;
    
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    bool hasAnsweredEarly = true;
    int correctAnswerIndex;
    
    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    [Header("JSON File")]
    JSONParser jsonParser;

    public bool isComplete;

    void Awake()
    {
        timer = FindAnyObjectByType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        jsonParser = FindAnyObjectByType<JSONParser>();
    }

    void Start() 
    {
        questionList = jsonParser.GetQuestions();
        if (questionList != null)
        {
            if(totalQuestions > questionList.questions.Count)
            {
                totalQuestions = questionList.questions.Count;
            }
            progressBar.maxValue = totalQuestions;
            progressBar.value = 0;
            scoreKeeper.totalQuestions = totalQuestions;
        }
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion) 
        {
            DisplayAnswer(-1);
            SetButtonEventState(false);
        }
    }

    public void OnAnswerSelected(int index) 
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonEventState(false);
        timer.CancelTimer();
        scoreText.text = "Pontuação: " + scoreKeeper.CalculateScore() + "%";
    }

    void DisplayAnswer(int index) 
    {
        Image buttonImage;

        if(index == currentQuestion.GetCorrectAnswerIndex()) 
        {
            questionText.text = "Resposta certa!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else 
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Desculpa, a resposta certa era:\n" + correctAnswer;

            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion() 
    {
        if (totalQuestions > 0) 
        {
            SetButtonEventState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questionList.questions.Count);
        currentQuestion = questionList.questions[index];

        if(questionList.questions.Contains(currentQuestion)) 
        {
            questionList.questions.Remove(currentQuestion);
        }

        totalQuestions--;
    }

    void DisplayQuestion() 
    {
        questionText.text = currentQuestion.GetQuestion();

        for(int i = 0;i < answerButtons.Length;i++) 
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonEventState(bool state) 
    {
        for (int i = 0; i < answerButtons.Length; i++) 
        {
            EventTrigger eventTrigger = answerButtons[i].GetComponent<EventTrigger>();
            eventTrigger.enabled = state;
        }

    }

    void SetDefaultButtonSprites() 
    {
        for (int i = 0; i < answerButtons.Length; i++) 
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }

    }
}
