using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Game
{
    public class Platform : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _modeMove;
        [SerializeField] private Canvas _canvas;

        private bool isPlaying;
        private bool isDragActive;
        private bool isAccelerometer;
        private Vector2 _offset;
        private Vector2 _startLocalPos;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (isPlaying)
            {
                if (isAccelerometer)
                {
                    Vector3 newPosition = Input.acceleration;
                    newPosition.y = transform.localPosition.y;
                    transform.localPosition = newPosition;
                }
            }
        }

        public void MoveLeft()
        {
            _rigidbody2D.angularVelocity = 0f;
            _rigidbody2D.AddForce(Vector2.left * _modeMove);
        }

        public void MoveRight()
        {
            _rigidbody2D.angularVelocity = 0f;
            _rigidbody2D.AddForce(Vector2.right * _modeMove);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startLocalPos = _rectTransform.localPosition;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                eventData.position,
                _canvas.worldCamera,
                out _offset
            );
            
            _offset =_startLocalPos - _offset;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (isDragActive)
            {
                if (isDragActive)
                {
                    Vector2 localPoint;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        _rectTransform.parent as RectTransform,
                        eventData.position,
                        _canvas.worldCamera,
                        out localPoint
                    );

                    localPoint += _offset;
                    localPoint.y = _rectTransform.localPosition.y; 

                    _rectTransform.localPosition = localPoint;
                }
            }
        }

        public void EnableDrag()
        {
            isDragActive = true;
            isAccelerometer = false;
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }

        public void EnableArrows()
        {
            isDragActive = false;
            isAccelerometer = false;
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        public void EnableAccelerometer()
        {
            isDragActive = false;
            isAccelerometer = true;
        }

        public void StartPlay() => isPlaying = true;
        public void StopPlay() => isPlaying = false;
    }
}
