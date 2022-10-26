using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuizLogic : MonoBehaviour
{
    [SerializeField] private QuestionsData questionData;
    [SerializeField] private TextMeshProUGUI questionText;
    private AnswerButton[] _answerButtons;

    private int _currentQuestionIndex;

    private void Start()
    {
        _answerButtons = GetComponentsInChildren<AnswerButton>();
        
        questionText.text = questionData.GetQuestion(_currentQuestionIndex);
        for (var index = 0; index < _answerButtons.Length; index++)
        {
            var answerButton = _answerButtons[index];
            answerButton.ChangeAnswer(questionData.GetAnswer(_currentQuestionIndex, index));

            answerButton.OnClicked += () => VerifyAnswer(index);
        }
    }

    private void VerifyAnswer(int answerButtonIndex)
    {
        int correctIndex = questionData.GetCorrectAnswer(_currentQuestionIndex);
        if (_currentQuestionIndex == correctIndex)
        {
            _answerButtons[answerButtonIndex].SetAnswerCorrect();
            AnsweredCorrectly();
        }
        else
        {
            _answerButtons[answerButtonIndex].SetAnswerWrong();
            _answerButtons[correctIndex].SetAnswerCorrect();
            AnsweredWrong();
        }

        SetNextQuestion();
    }

    private void SetNextQuestion()
    {
        _currentQuestionIndex++;

        if (_currentQuestionIndex > questionData.GetQuestionsAmount())
        {
            EndQuiz();
            return;
        }
        
        for (var index = 0; index < _answerButtons.Length; index++)
        {
            var answerButton = _answerButtons[index];
            answerButton.ChangeAnswer(questionData.GetAnswer(_currentQuestionIndex, index));
        }
    }

    private void EndQuiz()
    {
        throw new System.NotImplementedException();
    }

    private void AnsweredWrong()
    {
        throw new System.NotImplementedException();
    }

    private void AnsweredCorrectly()
    {
        throw new System.NotImplementedException();
    }
}
