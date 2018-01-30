using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public float startWait;
    public float waveWait;

    public float doubleTimeWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    private bool gameOver;
    private bool restart;
    public GameObject player;

    public GameObject enemy;

    public Vector2 spawnValues;
    ObjectPooler pool;
    public float minY;
    public int enemyCount;
    public float spawnWait;
    // Use this for initialization
    void Start () {
        pool = GetComponent<ObjectPooler>();
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        PlayerPrefs.SetInt("Score", 0);
        StartCoroutine(SpawnWave());
        
    }


    void Update()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Battle");
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {

                GameObject enemyClone = pool.GetPooledObject();
                enemyClone.SetActive(true);
                enemyClone.transform.position = new Vector3(Random.Range(-6.6f, 6.6f), spawnValues.y, 0);
                enemyClone.transform.rotation = Quaternion.identity;
                yield return new WaitForSeconds(spawnWait);

            }

            if (gameOver)
            {
                gameOverText.text = "Game Over!";
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }
}
