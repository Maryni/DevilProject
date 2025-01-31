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
        [SerializeField]
        [Header("Changed"), SerializeField] private List<TMP_Text> _bestScoreList;
        [SerializeField] private List<TMP_Text> _lastScoreList;
        [Header("Animation"), SerializeField] private float maxScale;
        [SerializeField] private float duration;

        #endregion Inspector variables
        
        #region public functions

        public void ChangeViewMenu() => ChangeView(_menu);
        public void ChangeViewGame() => ChangeView(_game);
        public void ChangeViewLoading() => ChangeView(_loading);

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

        #endregion private functions
    }
}
