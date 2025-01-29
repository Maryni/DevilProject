using System.Collections;
using System.Collections.Generic;

namespace Project.Load
{
    using UnityEngine;
    using UnityEngine.UI;
    
    public class Loading : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private Image _loadingImage;
        [SerializeField] private float _timeLoading;
        [SerializeField] private float _rotationSpeed;

        #endregion Inspector variables
    }
}
