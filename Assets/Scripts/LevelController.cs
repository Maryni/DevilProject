using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Game
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Timer _timer;
        [SerializeField] private float _bonusTimer;
        [SerializeField] private int _typeMovement;
        [SerializeField] private Platform _platform;
        
        public UnityAction OnGameEnd;
        public UnityAction OnUIEnableInput;
        public UnityAction OnUIDisableInput;

        private Coroutine BonusCoroutine;
        
        public float CurrentScore { get; private set; }
        public float BestScore { get; private set; }
        
        private void Start()
        {
            SetActions();
            SelectMovementType(_typeMovement);
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
                _platform.EnableAccelerometer();
                OnUIDisableInput?.Invoke();
            }

            if (value == 1)
            {
                _platform.EnableDrag();
                OnUIDisableInput?.Invoke();
            }

            if (value == 2)
            {
                _platform.EnableArrows();
                OnUIEnableInput?.Invoke();
            }
        }

        private void SetActions()
        {
            _spawner.OnScoreChange += AddCurrentScore;
            _spawner.OnBonusGet += BonusCollect;
            _timer.OnTimerEnd += () => _spawner.StopGame(true);
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

        private void BonusCollect(BonusType bonusType)
        {
            if (bonusType == BonusType.Gravity)
            {
                BonusCoroutine ??= StartCoroutine(GravityTimer());
            }

            if (bonusType == BonusType.BoxSize)
            {
                if (_platform.transform.localScale.x < 50)
                {
                    _platform.transform.localScale = new Vector3(
                        _platform.transform.localScale.x + 2,
                        _platform.transform.localScale.y,
                        _platform.transform.localScale.z
                        );
                }
            }
        }

        private IEnumerator GravityTimer()
        {
            _spawner.CastSmallGravity();
            yield return new WaitForSeconds(_bonusTimer);
            _spawner.ReturnGravity();
        }
    }
}
