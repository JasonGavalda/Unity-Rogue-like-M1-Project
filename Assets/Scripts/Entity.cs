using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{
    [SerializeField]
    protected Stats stats;
    protected string[] layers;


    protected bool isDead;
    [SerializeField]
    protected float cdDeath;


    private void Start()
    {
        stats.currentHealth = stats.maxHealth;
        isDead = false;
    }

    private void Update()
    {
        if (isDead)
        {
            cdDeath -= Time.deltaTime;
            if (cdDeath < 0)
                Destroy(this.gameObject);
        }

        if (stats.currentHealth <= 0)
            this.Die();
    }

    void Die()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public bool getIsDead() { return isDead; }


//------------------ Modify stats below -------------------------------


    public void TakeDamage(int pDamage) { stats.currentHealth = stats.currentHealth - (pDamage / stats.armor); }

    public int getLayers()
    {
        return LayerMask.GetMask(layers);
    }
}
