using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public GameObject enemy;
    public Vector2 spawnValues;
    ObjectPooler pool;
    public float minY;
    public int enemyCount;
    public float spawnWait;
    // Use this for initialization
    void Start () {
        pool = GetComponent<ObjectPooler>();
        StartCoroutine(SpawnWave());
    }


    IEnumerator SpawnWave()
    {
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                yield return new WaitForSeconds(spawnWait);
                GameObject enemyClone = pool.GetPooledObject();
                enemyClone.SetActive(true);
                enemyClone.transform.position = new Vector3(Random.Range(-6.6f, 6.6f), spawnValues.y, 0);
                enemyClone.transform.rotation = Quaternion.identity;
            }
        }
    }
}
