using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Game
{
    public class ButtonHelper : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private bool _isRight;
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _platform.StopMoving();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isRight)
            {
                _platform.MoveRight();
            }
            else
            {
               _platform.MoveLeft(); 
            }
        }
    }
}
