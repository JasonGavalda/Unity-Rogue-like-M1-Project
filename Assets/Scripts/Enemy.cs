using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected Stats enemyStats;
    protected StatsModifier playerStatsModifier;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats.currentHealth = enemyStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        enemyStats.currentHealth -= damage;
        if (enemyStats.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("enemy dead");

        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public bool enemyDead()
    {
        return isDead;
    }
}
