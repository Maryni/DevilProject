using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _countToSpawn;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _parentToSpawn;
        [SerializeField] private List<GameObject> _spawned;

        private bool isPlaying;

        #region Unity functions

        private void Start()
        {
            BaseSpawn();
        }

        #endregion Unity functions

        #region private functions

        private void BaseSpawn()
        {
            for (int i = 0; i < _countToSpawn; i++)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            var pointToSpawn = RandomPointInBounds(_spawnPoint.GetComponent<BoxCollider2D>().bounds);
            var newObject = Instantiate(_prefab, pointToSpawn, Quaternion.identity);
            newObject.transform.SetParent(_parentToSpawn);
            newObject.SetActive(false);
            _spawned.Add(newObject);
        }
        
        private Vector3 RandomPointInBounds(Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x , bounds.max.x ),
                Random.Range(bounds.min.y , bounds.max.y ));
        }

        #endregion private functions


        
    }
}