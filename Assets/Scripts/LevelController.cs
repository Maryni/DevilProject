using UnityEngine;
using UnityEngine.Events;

namespace Project.Game
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Timer _timer;
        [SerializeField] private int _typeMovement;
        [SerializeField] private Platform _platform;
        
        public UnityAction OnGameEnd;
        public UnityAction OnUIEnableInput;
        public UnityAction OnUIDisableInput;
        
        public float CurrentScore { get; private set; }
        public float BestScore { get; private set; }
        
        private void Start()
        {
            SetActions();
        }

        public void StartGame()
        {
            CurrentScore = 0;
            _spawner.StartGame();
            _timer.StartTimer();
            _platform.StartPlay();
        }

        public void StopGame()
        {
            _spawner.StopGame();
            _timer.StopTimer();
            _platform.StopPlay();
        }

        public void SelectMovementType(int value)
        {
            if (value == 0)
            {
                _platform.IsDragActive = false;
                _platform.IsAccelerometer = true;
                OnUIDisableInput?.Invoke();
            }

            if (value == 1)
            {
                _platform.IsDragActive = true;
                _platform.IsAccelerometer = false;
                OnUIDisableInput?.Invoke();
            }

            if (value == 2)
            {
                _platform.IsDragActive = false;
                _platform.IsAccelerometer = false;
                OnUIEnableInput?.Invoke();
            }
        }

        private void SetActions()
        {
            _spawner.OnScoreChange += AddCurrentScore;
            _timer.OnTimerEnd += _spawner.StopGame;
            _timer.OnTimerEnd += _timer.StopTimer;
            _timer.OnTimerEnd += _platform.StopPlay;
            _timer.OnTimerEnd += UpdateValues;
            _timer.OnTimerEnd += ()=> OnGameEnd?.Invoke();
        }

        private void AddCurrentScore(int value)
        {
            CurrentScore += value;
            UpdateValues();
        }

        private void UpdateValues()
        {
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
            }
        }
    }
}
