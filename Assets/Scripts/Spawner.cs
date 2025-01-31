using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Project.Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnObject _prefab;
        [SerializeField] private int _countToSpawn;
        [SerializeField] private float _spawnRate;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _parentToSpawn;
        [SerializeField] private List<SpawnObject> _spawned;

        private bool isPlaying;
        private Coroutine SpawnerCoroutine;

        public UnityAction<int> OnScoreChange;

        #region Unity functions

        private void Start()
        {
            BaseSpawn();
            SpawnerCoroutine ??= StartCoroutine(SpawnObjects());
        }

        #endregion Unity functions

        #region public functions

        public void GetCollision(SpawnObject spawnObject, ReflectObject reflectObject)
        {
            if (reflectObject.ReflectType == ReflectType.NonReflect)
            {
                OnScoreChange?.Invoke(spawnObject.Score);
                spawnObject.ResetObject();
                spawnObject.gameObject.SetActive(false);
            }
            else
            {
                Vector2 reflectVector = new Vector2(Random.Range(-1.0f,1.0f),3f);
                spawnObject.Rigidbody2D.linearVelocity = Vector2.zero;
                spawnObject.Rigidbody2D.AddForce(reflectVector * 100f);
                if (reflectObject.AddScore)
                {
                    spawnObject.AddScore();
                }
            }
        }

        public void StartGame() => isPlaying = true;
        public void StopGame() => isPlaying = false;

        #endregion public functions

        #region private functions

        private void BaseSpawn()
        {
            for (int i = 0; i < _countToSpawn; i++)
            {
                Spawn();
            }
        }
        
        private SpawnObject GetObject()
        {
            var finded = _spawned.FirstOrDefault(x => !x.gameObject.activeSelf);
            if (finded == null)
            {
                Spawn();
                var newObject = _spawned[^1];
                return newObject;
            }
            return finded;
        }

        private void Spawn()
        {
            var pointToSpawn = RandomPointInBounds(_spawnPoint.GetComponent<BoxCollider2D>().bounds);
            var newObject = Instantiate(_prefab, pointToSpawn, Quaternion.identity);
            newObject.transform.SetParent(_parentToSpawn);
            newObject.SetSpawner(this);
            newObject.gameObject.SetActive(false);
            _spawned.Add(newObject);
        }
        
        private Vector3 RandomPointInBounds(Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x , bounds.max.x ),
                Random.Range(bounds.min.y , bounds.max.y ));
        }

        private IEnumerator SpawnObjects()
        {
            while (true)
            {
                if (isPlaying)
                {
                    yield return new WaitForSeconds(_spawnRate);
                    var newObject = GetObject();
                    var pointToSpawn = RandomPointInBounds(_spawnPoint.GetComponent<BoxCollider2D>().bounds);
                    newObject.transform.position = pointToSpawn;
                    newObject.gameObject.SetActive(true);
                }
                else
                {
                    yield return 0;
                }
            }
        }

        #endregion private functions
    }
}