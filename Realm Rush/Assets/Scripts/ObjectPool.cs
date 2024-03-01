using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    GameObject[] pool;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0.5f, 30f)] float spawnTimer = 1f;
    [SerializeField] [Range(0, 50)] int poolsize = 5;

    private void Awake()
    {
        CreatObjectPool();
    }

    void CreatObjectPool()
    {
        pool = new GameObject[poolsize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab,transform);
            pool[i].SetActive(false);
        }       
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void ActiveObjectsAgain()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            ActiveObjectsAgain();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
