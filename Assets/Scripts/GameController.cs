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

        #endregion Inspector variables

        #region Unity functions

        private void Start()
        {
            SetActions();
        }

        #endregion Unity functions

        #region private functions

        private void SetActions()
        {
            
        }

        #endregion private functions
    }
}
