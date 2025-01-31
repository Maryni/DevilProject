using UnityEngine;
using UnityEngine.Events;

namespace Project.Game
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Timer _timer;

        public UnityAction OnGameEnd;
        
        public float CurrentScore { get; private set; }
        public float BestScore { get; private set; }

        
        //add endgame background
        //show at them score + menu btn
        //check why after end game wasn't start again
        
        private void Start()
        {
            SetActions();
        }

        public void StartGame()
        {
            _spawner.StartGame();
            _timer.StartTimer();
        }

        public void StopGame()
        {
            _spawner.StopGame();
            _timer.StopTimer();
        }

        private void SetActions()
        {
            _spawner.OnScoreChange += AddCurrentScore;
            _timer.OnTimerEnd += _spawner.StopGame;
            _timer.OnTimerEnd += UpdateValues;
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
            OnGameEnd?.Invoke();
        }
    }
}
