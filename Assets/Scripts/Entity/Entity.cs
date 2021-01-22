using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Entity : MonoBehaviour
{
    [SerializeField]
    protected Stats stats;

    [SerializeField]
    protected string[] layers;


    protected bool isDead;
    [SerializeField]
    protected float cdDeath;

    [SerializeField]
    public Animator animator;

    protected Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float invulnerableTime = 0;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        this.transform.localScale = new Vector3(stats.size, stats.size, stats.size);
        stats.currentHealth = stats.maxHealth;
        isDead = false;
    }

    private void DestroyEntity()
    {
        Destroy(this.gameObject);
    }

    void Die()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        Invoke("DestroyEntity", 0f);
        //this.enabled = false;
    }

    public bool getIsDead() { return isDead; }


    //------------------ Modify stats below -------------------------------

    public int getStrengh() { return stats.strengh; }
    public int getInt() { return stats.intelligence; }

    public void TakeDamage(int pDamage) {
        if (invulnerableTime > 0)
            return;
        invulnerableTime = stats.invulnerabilityTime;
        StartCoroutine(hitBlink());
        stats.currentHealth = stats.currentHealth - (pDamage / stats.armor);
        if (stats.currentHealth <= 0)
            this.Die();
    }

    public void Heal(int pHeal) { stats.currentHealth = Math.Min(stats.currentHealth + pHeal, stats.maxHealth); }

    public int getLayers()
    {
        return LayerMask.GetMask(layers);
    }

    IEnumerator invulnerabily()
    {
        while(invulnerableTime > 0)
        {
            invulnerableTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator hitBlink()
    {
        Color col = sprite.color;
        for(int i = 0; i < 5; i++)
        {
            sprite.color = new Color(col.r,col.g, col.b, 0.8f);
            yield return new WaitForSeconds(0.15f);
            sprite.color = new Color(col.r, col.g, col.b, 1f);
        }

    public void animateMove()
    {
        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical"))) *stats.speed);
        
    }

    public void animateAttack()
    {
        animator.SetTrigger("Attack");
    }
}
 