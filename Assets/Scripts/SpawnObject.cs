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

        public void SetSpawner(Spawner spawner) => _spawner = spawner;

        private void OnCollisionEnter2D(Collision2D other)
        {
            
        }
    }
}