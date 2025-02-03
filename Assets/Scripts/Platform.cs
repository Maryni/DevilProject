using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Game
{
    public class Platform : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField, Range(1f, 10f)] private float _modeMove;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _leftLimit;
        [SerializeField] private float _rightLimit;

        private bool isPlaying;
        private bool isDragActive;
        private bool isAccelerometer;
        private bool isArrows;
        private float direction;
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

        private void FixedUpdate()
        {
            if (isPlaying)
            {
                if (isArrows)
                {
                    MovePlatform(direction);
                }
            }
        }

        public void MoveLeft()
        {
            direction = -1f;
        }

        public void MoveRight()
        {
            direction = 1f;
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
            isArrows = false;
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }

        public void EnableArrows()
        {
            isDragActive = false;
            isAccelerometer = false;
            isArrows = true;
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        public void EnableAccelerometer()
        {
            isDragActive = false;
            isAccelerometer = true;
            isArrows = false;
        }

        public void StartPlay() => isPlaying = true;
        public void StopPlay() => isPlaying = false;

        public void StopMoving()
        {
            direction = 0f;
        }
        
        private void MovePlatform(float direction)
        {
            if (direction != 0)
            {
                float newX = Mathf.Clamp(_rigidbody2D.position.x + direction * _modeMove * Time.deltaTime, _leftLimit, _rightLimit);
                _rigidbody2D.linearVelocity = new Vector2(direction * _modeMove, 0f);
                if (Mathf.Approximately(_rigidbody2D.position.x, _leftLimit) || 
                    Mathf.Approximately(_rigidbody2D.position.x, _rightLimit))
                {
                    _rigidbody2D.linearVelocity = Vector2.zero;
                }
            }
            else
            {
                _rigidbody2D.linearVelocity = Vector2.zero;
            }
        }
    }
}
