using Data;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Quiz
{
    public class QuizManager : MonoBehaviour
    {
        [SerializeField] private QuizUI quizUI;
        [SerializeField] private Timer timerController;
        [SerializeField] private SliderController sliderController;
        [SerializeField] private QuestionsData questionData;

        private AnswerButton[] _answerButtons;
        private int _currentQuestionIndex = -1;

        private int _currentScore;
        private int _maxQuestionsAmount;

        private void Start()
        {
            GetReferences();

            _maxQuestionsAmount = questionData.GetQuestionsAmount();
            sliderController.SetMaxValue(_maxQuestionsAmount);

            AssignCallback();

            SetNewQuestionSet();
        }

        private void LateUpdate()
        {
            quizUI.SetScoreText(_currentScore, _maxQuestionsAmount);
        }

        private void OnDestroy()
        {
            UnAssignCallback();
        }

        private void UnAssignCallback()
        {
            timerController.OnTimerEnded -= TimeOut;
            quizUI.OnNextQuestionClicked -= SetNewQuestionSet;
        }

        private void AssignCallback()
        {
            timerController.OnTimerEnded += TimeOut;
            quizUI.OnNextQuestionClicked += SetNewQuestionSet;

            for (int index = 0; index < _answerButtons.Length; index++)
            {
                int index1 = index;
                _answerButtons[index1].OnClicked += () => PrepareToNextQuestion(index1);
            }
        }

        private void SetNewQuestionSet()
        {
            UnblockAnswerButtons();
            IncreaseCurrentQuestionIndex();

            if (_currentScore >= _maxQuestionsAmount)
            {
                EndQuiz();
                return;
            }
        
            timerController.StartTimer();
            sliderController.UpdateSlider(_currentQuestionIndex);
            ChangeQuestion();
            ChangeAnswers();
        }

        private void PrepareToNextQuestion(int chosenAnswerIndex)
        {
            timerController.StopTimer();
            BlockAnswerButtons();
            quizUI.EnableNextButton();

            HighlightAnswers(chosenAnswerIndex);
        }

        private void HighlightAnswers(int chosenAnswerIndex)
        {
            if (chosenAnswerIndex != GetCorrectAnswerIndex())
            {
                HighlightWrongAnswer(chosenAnswerIndex);
                quizUI.SetCorrectAnswerText();
            }
            else
                _currentScore++;

            HighlightCorrectAnswer();
        }

        private void HighlightWrongAnswer(int chosenAnswerIndex)
        {
            _answerButtons[chosenAnswerIndex].SetAnswerCorrect();
        }

        private int GetCorrectAnswerIndex()
        {
            return questionData.GetCorrectAnswerIndex(_currentQuestionIndex);
        }

        private void ChangeQuestion()
        {
            quizUI.SetNextQuestion(questionData.GetQuestion(_currentQuestionIndex));
        }

        private void ChangeAnswers()
        {
            for (int answerIndex = 0; answerIndex < _answerButtons.Length; answerIndex++)
            {
                AnswerButton answerButton = _answerButtons[answerIndex];
                answerButton.ChangeAnswer(questionData.GetAnswer(_currentQuestionIndex, answerIndex));
            }
        }

        private void IncreaseCurrentQuestionIndex()
        {
            _currentQuestionIndex++;
        }

        private void TimeOut()
        {
            HighlightCorrectAnswer();
            quizUI.SetWrongAnswerText();
            quizUI.EnableNextButton();
        }

        private void HighlightCorrectAnswer()
        {
            int correctIndex = GetCorrectAnswerIndex();
            _answerButtons[correctIndex].SetAnswerCorrect();
        }

        private void GetReferences()
        {
            _answerButtons = GetComponentsInChildren<AnswerButton>();
        }

        private bool IsQuestionsEnded()
        {
            return _currentQuestionIndex > _maxQuestionsAmount;
        }

        private void EndQuiz()
        {
        }

        public void BlockAnswerButtons()
        {
            foreach (AnswerButton answerButton in _answerButtons) answerButton.BlockButton();
        }

        private void UnblockAnswerButtons()
        {
            foreach (AnswerButton answerButton in _answerButtons) answerButton.UnblockButton();
        }
    }
}