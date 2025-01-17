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
    private Dictionary<string, List<GameObject>> _poolDictionary;
    private Dictionary<string, GameObject> _parentDictionary;
    
    private void Awake()
    {
        _poolDictionary = new Dictionary<string, List<GameObject>>();
        _parentDictionary = new Dictionary<string, GameObject>();
        string poolTag = null;
        
        foreach (var pool in _pools)
        {
            if (pool.prefab.TryGetComponent<Projectile>(out var projectile))
            {
                pool.tag = projectile.projectileTag;
                poolTag = pool.tag;
            }

            GameObject parent = new GameObject($"ParentOf_{poolTag}");
            parent.transform.SetParent(transform);
            _parentDictionary.Add(poolTag, parent);
            
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < pool.baseSize; i++)
            {
                GameObject obj = CreateNewObject(poolTag, pool.prefab);
                objectPool.Add(obj);
            }
            _poolDictionary.Add(poolTag, objectPool);
        }
    }

    public GameObject GetPooledObject(string projectileTag)
    {
        if (!_poolDictionary.ContainsKey(projectileTag))
        {
            return null;
        }
        
        foreach (var obj in _poolDictionary[projectileTag])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        GameObject go = CreateNewObject(projectileTag, _poolDictionary[projectileTag][0]);
        _poolDictionary[projectileTag].Add(go);
        return go;
    }

    private GameObject CreateNewObject(string tag, GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.transform.SetParent(_parentDictionary[tag].transform);
        obj.SetActive(false);
        return obj;
    }
}