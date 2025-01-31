using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Project.UI
{
    using UnityEngine.UI;
    using TMPro;
    
    public class UIController : MonoBehaviour
    {
        #region Inspector variables

        [Header("Main UI"),SerializeField] private GameObject _menu;
        [SerializeField] private GameObject _game;
        [SerializeField] private GameObject _loading;
        [SerializeField] private Transform _menuButton;
        [SerializeField] private GameObject _settings;
        [SerializeField] private GameObject _endgameBackground;
        [SerializeField] private GameObject _gameArrows;
        [Header("Changed"), SerializeField] private List<TMP_Text> _bestScoreList;
        [SerializeField] private List<TMP_Text> _lastScoreList;
        [Header("Animation"), SerializeField] private float maxScale;
        [SerializeField] private float duration;

        #endregion Inspector variables
        
        #region public functions

        public void ChangeViewMenu() => ChangeView(_menu);
        public void ChangeViewGame() => ChangeView(_game);
        public void ChangeViewSettings() => ChangeView(_settings);
        public void ChangeViewEndGame() => ChangeView(_endgameBackground);
        public void ChangeViewLoading() => ChangeView(_loading);
        public void ChangeViewGameArrowsOn() => ChangeViewOn(_gameArrows);
        public void ChangeViewGameArrowsOff() => ChangeViewOff(_gameArrows);

        public void SetBestScore(string value)
        {
            foreach (var item in _bestScoreList)
            {
                SetText(item, value);
            }
        }
        
        public void SetCurrentScore(string value)
        {
            foreach (var item in _lastScoreList)
            {
                SetText(item, value);
            }
        }

        public void StartButtonAnimation() => _menuButton.DOScale(new Vector3(maxScale, maxScale, maxScale), duration).SetLoops(-1, LoopType.Yoyo);

        #endregion public functions

        #region private functions

        private void SetText(TMP_Text item, string value) => item.text = value;
        private void ChangeView(GameObject item) => item.SetActive(!item.activeSelf);
        private void ChangeViewOn(GameObject item) => item.SetActive(true);
        private void ChangeViewOff(GameObject item) => item.SetActive(false);

        #endregion private functions
    }
}
