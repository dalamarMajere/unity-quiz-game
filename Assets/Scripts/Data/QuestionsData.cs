using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Data
{
    [Serializable]
    class QuestionAnswers
    {
        [TextArea(2, 6)]
        [SerializeField] private string question;
        [SerializeField] private string[] answers = new string[4];
        [SerializeField] private int correctAnswerIndex;

        public int CorrectAnswerIndex => correctAnswerIndex;

        public string[] Answers => answers;

        public string Question => question;

        public string GetAnswer(int answerIndex)
        {
            if (answerIndex >= 0 && answerIndex < answers.Length)
            {
                return answers[answerIndex];
            }

            return "";
        }
    }

    [CreateAssetMenu(menuName = "Create Quiz QuestionsData", fileName = "QuestionsData", order = 0)]
    public class QuestionsData : ScriptableObject
    {
        [SerializeField] private QuestionAnswers[] questions;

        public string GetQuestion(int index)
        {
            if (index >= 0 && index < questions.Length)
            {
                return questions[index].Question;
            }
            
            return "";
        }

        public string GetAnswer(int questionIndex, int answerIndex)
        {
            if (questionIndex >= 0 && questionIndex < questions.Length)
            {
                return questions[questionIndex].GetAnswer(answerIndex);
            }

            return "";
        }

        public int GetCorrectAnswer(int questionIndex)
        {
            if (questionIndex >= 0 && questionIndex < questions.Length)
            {
                return questions[questionIndex].CorrectAnswerIndex;
            }

            return -1;
        }

        public int GetQuestionsAmount()
        {
            return questions.Length;
        }
    }
}