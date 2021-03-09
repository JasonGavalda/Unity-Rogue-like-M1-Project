using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        int EnemySpawn = Random.Range(0, 2);
        if (EnemySpawn == 1)
        {
            Invoke("Spawn", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Spawn()
    {
        int randEnemy = Random.Range(0, enemies.Length);

        Instantiate(enemies[randEnemy], transform.position, transform.rotation);
    }
}
