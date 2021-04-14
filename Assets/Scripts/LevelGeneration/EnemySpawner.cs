using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemySpawner : MonoBehaviour
{
    public string[] enemies;

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
        if (PhotonNetwork.IsMasterClient) { 
            int randEnemy = Random.Range(0, enemies.Length);
            Debug.Log(randEnemy);
            PhotonNetwork.Instantiate(Path.Combine("Prefab", "Entities", enemies[randEnemy]), transform.position, transform.rotation);
        //Instantiate(enemies[randEnemy], transform.position, transform.rotation);
        }
    }
}
