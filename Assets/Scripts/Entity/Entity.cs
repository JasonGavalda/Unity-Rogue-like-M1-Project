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
        StartCoroutine(invulnerabily());
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
            Debug.Log(invulnerableTime);
        }
        Debug.Log("exit invulnerability");
    }

    IEnumerator hitBlink()
    {
        int blinks = 10;

        if (this.gameObject == null)
            yield break;

        Color col = new Color(sprite.color.r, sprite.color.g, sprite.color.b);
        for (int i = 0; i < blinks; i++)
        {
            sprite.color = new Color(col.r + 0.1f, col.g, col.b, 0.7f);
            yield return new WaitForSeconds(stats.invulnerabilityTime / (blinks * 2));
            sprite.color = new Color(col.r, col.g, col.b, 0.9f);
            yield return new WaitForSeconds(stats.invulnerabilityTime / (blinks * 2));
        }
        sprite.color = col;
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
 