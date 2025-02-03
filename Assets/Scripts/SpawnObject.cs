using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public void CollectScore()
        {
            UpdateScore();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<ReflectObject>())
            {
                //_rigidbody2D.sharedMaterial.bounciness = Random.Range(0.2f, 1f);
                var reflect = other.gameObject.GetComponent<ReflectObject>();
                if (reflect.ReflectType != ReflectType.None)
                {
                    _spawner.GetCollision(this, reflect);
                }
            }
        }

        private void UpdateScore() =>_text.text = _score.ToString();
    }
}