using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Project.Load
{
    using UnityEngine;
    using UnityEngine.UI;
    
    public class Loading : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private Image _loadingImage;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private float _timeLoading;
        [SerializeField] private float _rotationSpeed;

        #endregion Inspector variables

        #region private variables

        private bool isRotating;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            isRotating = true;
        }
        
        private void Update()
        {
            if (isRotating)
            {
                _loadingImage.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
            }
        }

        #endregion Unity functions

        #region public functions

        [ContextMenu("StopLoading")]
        public void StopLoading()
        {
            isRotating = false;
            _loadingImage.gameObject.SetActive(false);
        }

        public void ShowField(string value)
        {
            _inputField.gameObject.SetActive(true);
            _inputField.text = value;
        }

        #endregion public functions
    }
}
