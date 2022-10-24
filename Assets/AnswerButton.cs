using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour
{
    public event Action OnClicked;
    
    private TextMeshProUGUI _answer;
    private Button _button;

    private void Start()
    {
        _answer = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(Notify);
    }

    private void Notify()
    {
        OnClicked?.Invoke();
    }

    public void SetAnswer(string answer)
    {
        _answer.text = answer;
    }
}