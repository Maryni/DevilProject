using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Game
{
    public class Platform : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public bool IsPlaying;
        public bool IsDragActive;
        public bool IsAccelerometer;
        
        private Vector2 _offset;
        private RectTransform _rectTransform;
        [SerializeField] private Canvas _canvas;


        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsDragActive)
            {
                var newPosition = eventData.position;
                newPosition.y = transform.localPosition.y;
                transform.localPosition = newPosition;
            }
        }

        private void Update()
        {
            if (IsPlaying)
            {
                if (IsAccelerometer)
                {
                    var newPosition = Input.acceleration;
                    newPosition.y = transform.localPosition.y;
                    transform.localPosition = newPosition;
                }
            }
        }

        public void MoveLeft()
        {
            _rigidbody2D.angularVelocity = 0f;
            _rigidbody2D.AddForce(Vector2.left * 10f);
        }

        public void MoveRight()
        {
            _rigidbody2D.angularVelocity = 0f;
            _rigidbody2D.AddForce(Vector2.right * 10f);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                eventData.position,
                _canvas.worldCamera,
                out _offset
            );
        }
    }
}
