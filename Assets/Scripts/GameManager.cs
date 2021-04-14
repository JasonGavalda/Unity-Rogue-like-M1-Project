using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> enemies;
    private int numberEnemiesInit;
    private int numberDeadEnemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Boss")){ 
           enemies.Add(enemy);
           numberEnemiesInit++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        print("nombre de boss = " + (numberEnemiesInit - numberDeadEnemies));
        numberDeadEnemies = 0;
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                //this.enemies.Remove(enemy);
                numberDeadEnemies++;
            }
        }
        if ((numberEnemiesInit - numberDeadEnemies) == 0)
        {
            print("You Win");
            PhotonNetwork.LoadLevel(3);
        }
    }
}
