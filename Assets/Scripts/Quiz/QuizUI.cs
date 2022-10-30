using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class QuizUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Button nextButton;

        private void Start()
        {
            nextButton.onClick.AddListener(Notify);
        }

        public event Action OnNextQuestionClicked;

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

        public void SetScoreText(int score, int maxScore)
        {
            scoreText.text = $"{score}/{maxScore}";
        }

        public void EnableNextButton()
        {
            nextButton.gameObject.SetActive(true);
        }
    }
}