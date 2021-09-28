using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAZ_ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class YAZ_Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    
    public static YAZ_ObjectPooler instance;
    private void Awake()
    {
        instance = this;
    }
    
    #endregion
    
    public List<YAZ_Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    
    

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (YAZ_Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Update is called once per frame
    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist !");
            return null;
        }
        
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
