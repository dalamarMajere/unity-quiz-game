using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public event Action OnNextQuestionClicked;
    
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button nextButton;

    private void Start()
    {
        nextButton.onClick.AddListener(Notify);
    }

    private void Notify()
    {
        OnNextQuestionClicked?.Invoke();
    }

    public void SetNextQuestion(string question)
    {
        questionText.text = question;

        nextButton.gameObject.SetActive(false);
    }

    public void SetWrongAnswerText()
    {
        questionText.text = "Sorry, you answered wrong\n";
    }

    public void SetCorrectAnswerText()
    {
        questionText.text = "Correct!";
    }

    public void SetScoreText(string s)
    {
        scoreText.text = s;
    }

    public void EnableNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }
}