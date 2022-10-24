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

    private void Start()
    {
        _answerButtons = GetComponentsInChildren<AnswerButton>();
        
        questionText.text = questionData.GetQuestion(0);
        for (var index = 0; index < _answerButtons.Length; index++)
        {
            var answerButton = _answerButtons[index];
            answerButton.SetAnswer(questionData.GetAnswer(0, index));
        }
    }
}
