using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Audio;
using Project.Game;
using Project.UI;
using Project.Load;

namespace Project
{
    public class GameController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private AudioController _audioController;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private Loading _loading;
        [SerializeField] private UIController _uiController;
        [SerializeField] private ServerController _serverController;
        [SerializeField] private DailyController _dailyController;

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            SetActions();
            _dailyController.StartDailyCheck();
        }

        #endregion Unity functions

        #region private functions

        private void SetActions()
        {
            _dailyController.OnDailyCheck += _serverController.GetServerResponse;
            _serverController.OnServerOn += _loading.StopLoading;
            _serverController.OnServerOn +=  () => _loading.ShowField(_serverController.ResponseResult);
            _serverController.OnServerOff += _loading.StopLoading;
            _serverController.OnServerOff += _uiController.ChangeViewLoading;
            _serverController.OnServerOff += _uiController.StartButtonAnimation;
            _levelController.OnGameEnd += () => _uiController.SetBestScore(_levelController.BestScore.ToString());
            _levelController.OnGameEnd += () => _uiController.SetCurrentScore(_levelController.CurrentScore.ToString());
        }

        #endregion private functions
    }
}
