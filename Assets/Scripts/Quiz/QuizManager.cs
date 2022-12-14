using Data;
using UI;
using UnityEngine;

namespace Quiz
{
    public class QuizManager : MonoBehaviour
    {
        [SerializeField] private QuizUI quizUI;
        [SerializeField] private Timer timerController;
        [SerializeField] private SliderController sliderController;
        [SerializeField] private QuestionsData questionData;
        [SerializeField] private ScoreHolder scoreHolder;

        private AnswerButton[] _answerButtons;
        private int _currentQuestionIndex = -1;

        private int _maxQuestionsAmount;

        private void Start()
        {
            scoreHolder.Score = 0;

            GetReferences();

            _maxQuestionsAmount = questionData.GetQuestionsAmount();
            sliderController.SetMaxValue(_maxQuestionsAmount);

            AssignCallback();

            SetNewQuestionSet();
        }

        private void LateUpdate()
        {
            quizUI.SetScoreText(scoreHolder.Score, _maxQuestionsAmount);
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
                _answerButtons[index].OnClicked += PrepareToNextQuestion;
        }

        private void SetNewQuestionSet()
        {
            UnblockAnswerButtons();
            IncreaseCurrentQuestionIndex();

            if (AreQuestionsEnded())
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
                quizUI.SetWrongAnswerText();
            }
            else
            {
                quizUI.SetCorrectAnswerText();
                scoreHolder.Score++;
            }

            HighlightCorrectAnswer();
        }

        private void HighlightWrongAnswer(int chosenAnswerIndex)
        {
            _answerButtons[chosenAnswerIndex].SetAnswerWrong();
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

        private bool AreQuestionsEnded()
        {
            return _currentQuestionIndex >= _maxQuestionsAmount;
        }

        private void EndQuiz()
        {
            SceneLoader.LoadGameEndScene();
        }

        private void BlockAnswerButtons()
        {
            foreach (AnswerButton answerButton in _answerButtons) answerButton.BlockButton();
        }

        private void UnblockAnswerButtons()
        {
            foreach (AnswerButton answerButton in _answerButtons) answerButton.UnblockButton();
        }
    }
}