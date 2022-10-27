using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float maxTime;
    private float _currentTime;

    private Image _image;
    private bool isRunning;

    public event Action OnTimerEnded;
    
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (!isRunning) return;

        if (_currentTime >= maxTime)
        {
            Notify();
            StopTimer();
        }

        _currentTime += Time.deltaTime;
        _image.fillAmount = _currentTime / maxTime;
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private void Notify()
    {
        OnTimerEnded?.Invoke();
    }
}