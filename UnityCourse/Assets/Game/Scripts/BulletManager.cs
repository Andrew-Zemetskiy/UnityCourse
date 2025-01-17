using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _amountToPool = 4;

    [SerializeField] private List<GameObject> _pooledObjects;

    private void Awake()
    {
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _amountToPool; i++)
        {
            CreateNewObject();
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }

        Debug.Log("New object created");
        return CreateNewObject();
    }

    private GameObject CreateNewObject()
    {
        var go = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        go.transform.SetParent(transform);
        go.SetActive(false);
        _pooledObjects.Add(go);
        return go;
    }
}