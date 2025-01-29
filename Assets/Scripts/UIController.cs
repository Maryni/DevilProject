using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    using UnityEngine.UI;
    using TMPro;
    
    public class UIController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private GameObject _menu;
        [SerializeField] private GameObject _level;
        [SerializeField] private GameObject _game;
        [SerializeField] private GameObject _info;
        [SerializeField] private GameObject _loading;
        [SerializeField] private GameObject _settings;
        [SerializeField] private GameObject _onBoarding;

        #endregion Inspector variables

        #region public functions

        public void ChangeViewMenu() => ChangeView(_menu);
        public void ChangeViewGame() => ChangeView(_game);
        public void ChangeViewInfo() => ChangeView(_info);
        public void ChangeViewLevel() => ChangeView(_level);
        public void ChangeViewLoading() => ChangeView(_loading);
        public void ChangeViewSettings() => ChangeView(_settings);
        public void ChangeViewOnBoarding() => ChangeView(_onBoarding);

        #endregion public functions

        #region private functions

        private void SetText(TMP_Text item, string value) => item.text = value;
        private void ChangeView(GameObject item) => item.SetActive(!item.activeSelf);

        #endregion private functions
    }
}
