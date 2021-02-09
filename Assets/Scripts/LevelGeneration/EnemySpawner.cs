using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int randNumberEnemies = Random.Range(0, 4);
        for(int i = 0; i<randNumberEnemies; i++)
        {
            Invoke("Spawn", 0.1f);
        }
    }

    void Spawn()
    {
        int randEnemy = Random.Range(0, enemies.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(enemies[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
    }
}
