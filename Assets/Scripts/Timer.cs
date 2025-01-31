using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Project.Game
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private float _timeDefault;
        [SerializeField] private Slider _slider;
        
        private bool isPlaying;
        private Coroutine timerCoroutine;

        public UnityAction OnTimerEnd;

        private void Start()
        {
            _time = _timeDefault;
            _slider.maxValue = _timeDefault;
            _slider.value = _timeDefault;
            timerCoroutine ??= StartCoroutine(Time());
        }

        private IEnumerator Time()
        {
            while (true)
            {
                yield return null;
                if (isPlaying)
                {
                    if (_time > 0)
                    {
                        _time -= UnityEngine.Time.deltaTime;
                        _slider.value = _time;
                    }
                    else
                    {
                        StopTimer();
                        OnTimerEnd?.Invoke();
                        _time = _timeDefault;
                        _slider.value = _time;
                    }
                }
            }
        }

        public void StartTimer() => isPlaying = true;

        public void StopTimer() => isPlaying = false;
    }
}
