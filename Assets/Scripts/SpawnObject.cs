using System;
using TMPro;
using UnityEngine;

namespace Project.Game
{
    public class SpawnObject : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private int _score;

        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public int Score => _score;

        public void SetSpawner(Spawner spawner) => _spawner = spawner;

        public void AddScore()
        {
            _score++;
            UpdateScore();
        }

        public void ResetObject()
        {
            _score = 0;
            UpdateScore();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ReflectObject>())
            {
                var reflect = other.GetComponent<ReflectObject>();
                if (reflect.ReflectType != ReflectType.None)
                {
                    _spawner.GetCollision(this, reflect);
                }
            }
        }
        
        private void UpdateScore() =>_text.text = _score.ToString();
    }
}