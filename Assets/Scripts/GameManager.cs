using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){ 
            this.enemies.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
       foreach (GameObject enemy in enemies) {
            if (enemy.GetComponent<Entity>().getIsDead())
                this.enemies.Remove(enemy);
        }
       if (this.enemies.Count == 0)
            print("you win");
    }
}
