using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    [RequireComponent(typeof(Button))]
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite correctAnswerSprite;
        [SerializeField] private Sprite wrongAnswerSprite;
        [SerializeField] private int index;

        private TextMeshProUGUI _answer;
        private Button _button;
        private Image _image;

        private void Awake()
        {
            _answer = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();

            _button.onClick.AddListener(ButtonClicked);
        }

        public event Action<int> OnClicked;

        private void ButtonClicked()
        {
            OnClicked?.Invoke(index);
        }

        public void SetAnswerWrong()
        {
            _image.sprite = wrongAnswerSprite;
        }

        public void SetAnswerCorrect()
        {
            _image.sprite = correctAnswerSprite;
        }

        public void ChangeAnswer(string answer)
        {
            _answer.text = answer;
            _image.sprite = defaultSprite;
        }

        public void BlockButton()
        {
            _button.interactable = false;
        }

        public void UnblockButton()
        {
            _button.interactable = true;
        }
    }
}