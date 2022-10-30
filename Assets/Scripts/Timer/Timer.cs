using System;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float maxTime;
        private float _currentTime;

        private Image _image;
        private bool _isRunning;

        public event Action OnTimerEnded;
    
        private void Start()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            if (!_isRunning) return;

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
            _currentTime = 0;
            _isRunning = true;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        private void Notify()
        {
            OnTimerEnded?.Invoke();
        }
    }
}