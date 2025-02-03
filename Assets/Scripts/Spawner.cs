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
        [SerializeField] private SpawnObject _prefabBonus;
        [SerializeField] private int _countToSpawn;
        [SerializeField] private float _spawnRate;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _parentToSpawn;
        [SerializeField] private List<SpawnObject> _spawnedRegular;
        [SerializeField] private List<SpawnObject> _spawnedBonus;
        [SerializeField, Range(0f,100f)] private float _bonusChance;
        [Space, SerializeField] private float _modForceToRandomBounce;
        [SerializeField, Range(0f,0.75f)] private float _smallGravity;
        [SerializeField, Range(0.75f,1f)] private float _baseGravity;

        private bool isPlaying;
        private Coroutine SpawnerCoroutine;

        public UnityAction<int> OnScoreChange;
        public UnityAction<BonusType> OnBonusGet;
        public UnityAction OnCollision;

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
            if (reflectObject.ReflectType == ReflectType.Reflect)
            {
                if (reflectObject.AddScore)
                {
                    if (spawnObject.GetComponent<Bonus>())
                    {
                        var bonusType = spawnObject.GetComponent<Bonus>().BonusType;
                        OnBonusGet(bonusType);
                        spawnObject.ResetObject();
                        spawnObject.gameObject.SetActive(false);
                    }
                    
                    GetRandomBounce(spawnObject.Rigidbody2D);
                    OnCollision?.Invoke();
                    spawnObject.AddScore();
                }
            }
            else
            {
                OnScoreChange?.Invoke(spawnObject.Score);
                spawnObject.ResetObject();
                spawnObject.gameObject.SetActive(false);
            }
        }

        public void StartGame() => isPlaying = true;

        public void StopGame(bool end = false)
        {
            isPlaying = false;
            HideAll();
            if (end)
            {
                ResetAll();
            }
        }

        public void CastSmallGravity()
        {
            foreach (var item in _spawnedRegular)
            {
                item.Rigidbody2D.gravityScale = _smallGravity;
            }
        }

        public void ReturnGravity()
        {
            foreach (var item in _spawnedRegular)
            {
                item.Rigidbody2D.gravityScale = _baseGravity;
            }
        }

        #endregion public functions

        #region private functions

        private void GetRandomBounce(Rigidbody2D rigidbody2D)
        {
            Vector2 randForce = new Vector2(Random.Range(-1f,1f),0f);
            rigidbody2D.AddForce(randForce * _modForceToRandomBounce, ForceMode2D.Impulse);
        }

        private void BaseSpawn()
        {
            for (int i = 0; i < _countToSpawn; i++)
            {
                Spawn();
            }
            
            for (int i = 0; i < _countToSpawn; i++)
            {
                Spawn(true);
            }
        }
        
        private SpawnObject GetObject(bool isBonus = false)
        {
            if (!isBonus)
            {
                var finded = _spawnedRegular.FirstOrDefault(x => !x.gameObject.activeSelf);
                if (finded == null)
                {
                    Spawn();
                    var newObject = _spawnedRegular[^1];
                    return newObject;
                }
                return finded;
            }
            else
            {
                var finded = _spawnedBonus.FirstOrDefault(x => !x.gameObject.activeSelf);
                if (finded == null)
                {
                    Spawn(true);
                    var newObject = _spawnedBonus[^1];
                    return newObject;
                }
                return finded;
            }
        }

        private void Spawn(bool isBonus = false)
        {
            var pointToSpawn = RandomPointInBounds(_spawnPoint.GetComponent<BoxCollider2D>().bounds);
            SpawnObject newObject;
            if (!isBonus)
            {
                newObject = Instantiate(_prefab, pointToSpawn, Quaternion.identity);
                _spawnedRegular.Add(newObject);
            }
            else
            {
                BonusType bonusType = (BonusType)Random.Range(1, 3);
                newObject = Instantiate(_prefabBonus, pointToSpawn, Quaternion.identity);
                newObject.GetComponent<Bonus>().BonusType = bonusType;
                _spawnedBonus.Add(newObject);
            }
            
            newObject.transform.SetParent(_parentToSpawn);
            newObject.SetSpawner(this);
            newObject.gameObject.SetActive(false);
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
                    var currentBonus = Random.Range(0f, 100f);
                    SpawnObject newObject = currentBonus <= _bonusChance ? GetObject(true) : GetObject();

                    var pointToSpawn = RandomPointInBounds(_spawnPoint.GetComponent<BoxCollider2D>().bounds);
                    newObject.transform.position = pointToSpawn;
                    newObject.gameObject.SetActive(true);
                    
                    yield return new WaitForSeconds(_spawnRate);
                }
                else
                {
                    yield return 0;
                }
            }
        }

        private void HideAll()
        {
            foreach (var item in _spawnedRegular)
            {
                item.gameObject.SetActive(false);
                item.CollectScore();
            }

            foreach (var item in _spawnedBonus)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void ResetAll()
        {
            foreach (var item in _spawnedRegular)
            {
                item.ResetObject();
            } 
        }

        #endregion private functions
    }
}