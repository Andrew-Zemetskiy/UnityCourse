using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    [System.Serializable]
    public class Pool
    {
        [HideInInspector]
        public string tag;
        public GameObject prefab;
        [Range(1,15)]
        public int baseSize = 1;
    }

    [SerializeField] private List<Pool> _pools;
    public Dictionary<string, List<GameObject>> _poolDictionary;

    private void Awake()
    {
        _poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (var pool in _pools)
        {
            if (pool.prefab.TryGetComponent<Projectile>(out var projectile))
            {
                pool.tag = projectile.tag;
            }
            Debug.Log("Pool tag" + pool.tag);
            
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < pool.baseSize; i++)
            {
                GameObject obj = CreateNewObject(pool.prefab);
                objectPool.Add(obj);
            }
            _poolDictionary.Add(pool.tag, objectPool);
        }

        foreach (var v in _poolDictionary)
        {
            Debug.Log(v.Key);
        }
    }

    public GameObject GetPooledObject(string projectileTag)
    {
        if (!_poolDictionary.ContainsKey(projectileTag))
        {
            Debug.LogError("Not found tag");
            return null;
        }
        Debug.Log("Contains!");

        foreach (var obj in _poolDictionary[projectileTag])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        Debug.Log("Not enough space");
        return CreateNewObject(_poolDictionary[projectileTag][0]);
    }

    private GameObject CreateNewObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        return obj;
    }
}