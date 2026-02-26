using System;

namespace Matcher.Core.Game.UI
{
    public class CountdownTimer
    {
        public event Action<float> OnTimeUpdated;
        public event Action OnTimeUp;

        private float _timeRemaining;
        private bool _isRunning;

        public float TimeRemaining => _timeRemaining;

        public void Start(float durationSeconds)
        {
            _timeRemaining = durationSeconds;
            _isRunning = true;
            OnTimeUpdated?.Invoke(_timeRemaining);
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Tick(float deltaTime)
        {
            if (!_isRunning) return;

            _timeRemaining -= deltaTime;

            if (_timeRemaining <= 0)
            {
                _timeRemaining = 0;
                _isRunning = false;
                OnTimeUpdated?.Invoke(_timeRemaining);
                OnTimeUp?.Invoke();
            }
            else
            {
                OnTimeUpdated?.Invoke(_timeRemaining);
            }
        }
    }
}