using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    GameObject lastEnemySpawned;
    GameObject boss;

    bool hasBossSpawned = false;


    public int maxSpawns;
    int currentSpawns = 0;

    bool canSpawn = true;
    public float spawnWait;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (currentSpawns < maxSpawns && canSpawn)
        {
            lastEnemySpawned = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            currentSpawns++;
            StartCoroutine(WaitForSpawn());
        }
        else if (lastEnemySpawned == null && !hasBossSpawned && canSpawn)
        {
            boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
            hasBossSpawned = true;
        }
        else if (currentSpawns == maxSpawns && lastEnemySpawned == null && hasBossSpawned && boss == null)
        {
            SceneManager.LoadScene(4);
        }


	}

    IEnumerator WaitForSpawn()
    {
        canSpawn = false;

        yield return new WaitForSeconds(spawnWait);

        canSpawn = true;

    }


}

