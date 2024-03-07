using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Scene currentScene;
    public static GameManager instance;
    public Canvas quizCanvas;
    public Canvas winCanvas;
    public Canvas startCanvas;
    public Canvas optionsCanvas;

    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else 
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        //SceneManager.LoadScene("Main");
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Game")
        {
            if (quizCanvas.gameObject.GetComponent<Quiz>().isComplete)
            {
                quizCanvas.gameObject.SetActive(false);
                winCanvas.gameObject.SetActive(true);
                winCanvas.gameObject.GetComponent<EndScreen>().ShowFinalScore();
            }
            else
            {
                quizCanvas.gameObject.SetActive(true);
                winCanvas.gameObject.SetActive(false);
            }
        }
    }

    public void OnStartGame()
    {
        SwitchScene("Game");
    }

    public void OnOptionsSelect()
    {
        throw new NotImplementedException();
    }

    public void BackToMenu()
    {
        SwitchScene("Main");
        // startCanvas.gameObject.SetActive(true);
        // optionsCanvas.gameObject.SetActive(false);
        // winCanvas.gameObject.SetActive(false);
        // quizCanvas.gameObject.SetActive(false);
    }

    public void OnReplayLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SwitchScene("Game");
    }

    void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
