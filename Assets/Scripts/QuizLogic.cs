using Data;
using TMPro;
using UnityEngine;

public class QuizLogic : MonoBehaviour
{
    [SerializeField] private QuestionsData questionData;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TimerController timerController;

    private int _currentQuestionIndex = -1;
    private AnswerButton[] _answerButtons;

    private void Start()
    {
        _answerButtons = GetComponentsInChildren<AnswerButton>();

        SetNextQuestion();
        
        for (int index = 0; index < _answerButtons.Length; index++)
        {
            int index1 = index;
            _answerButtons[index1].OnClicked += () => VerifyAnswer(index1);
        }

        timerController.OnTimerEnded += TimeOut;
        timerController.StartTimer();
    }

    private void TimeOut()
    {
        Debug.Log("Time out!");
    }

    private void VerifyAnswer(int answerButtonIndex)
    {
        BlockAnswerButtons();

        int correctIndex = questionData.GetCorrectAnswerIndex(_currentQuestionIndex);
        HighlightAnswerButtons(answerButtonIndex, correctIndex);
    }

    private void HighlightAnswerButtons(int answerButtonIndex, int correctIndex)
    {
        if (answerButtonIndex == correctIndex)
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
    }

    private void BlockAnswerButtons()
    {
        foreach (AnswerButton answerButton in _answerButtons) answerButton.BlockButton();
    }

    private void SetNextQuestion()
    {
        _currentQuestionIndex++;

        if (IsQuestionsEnded())
        {
            EndQuiz();
            return;
        }

        SetTextToAnswerButtons();

        UnblockAnswerButtons();
    }

    private void SetTextToAnswerButtons()
    {
        for (int index = 0; index < _answerButtons.Length; index++)
        {
            AnswerButton answerButton = _answerButtons[index];
            answerButton.ChangeAnswer(questionData.GetAnswer(_currentQuestionIndex, index));
        }
    }

    private bool IsQuestionsEnded()
    {
        return _currentQuestionIndex > questionData.GetQuestionsAmount();
    }

    private void UnblockAnswerButtons()
    {
        foreach (AnswerButton answerButton in _answerButtons) answerButton.UnblockButton();
    }

    private void EndQuiz()
    {
    }

    private void AnsweredWrong()
    {
        questionText.text = "Sorry, the correct answer was:\n" + questionData.GetCorrectAnswer(_currentQuestionIndex);
    }

    private void AnsweredCorrectly()
    {
        questionText.text = "Correct!";
    }
}