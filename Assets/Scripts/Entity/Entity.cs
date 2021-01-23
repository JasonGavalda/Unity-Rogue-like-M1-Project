using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    [SerializeField]
    protected GameObject healthBar;
    [SerializeField]
    protected Color goodColor;
    [SerializeField]
    protected Color middleColor;
    [SerializeField]
    protected Color badColor;


    protected Rigidbody2D rb;

    private void Start()
    {
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
        Debug.Log(this.gameObject.tag + " took damage");
        stats.currentHealth = stats.currentHealth - (pDamage / stats.armor);
        UpdateHealth();
        if (stats.currentHealth <= 0)
            this.Die();
    }

    public void Heal(int pHeal) { 
        stats.currentHealth = Math.Min(stats.currentHealth + pHeal, stats.maxHealth);
        UpdateHealth();
    }

    public int getLayers()
    {
        return LayerMask.GetMask(layers);
    }

    public void animateMove()
    {
        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical"))) *stats.speed);
        
    }

    public void animateAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void UpdateHealth()
    {
        print(stats.currentHealth / stats.maxHealth);
        healthBar.GetComponent<Scrollbar>().size = stats.currentHealth / stats.maxHealth;
        SetColor();
    }

    void SetColor()
    {
        if (stats.currentHealth / stats.maxHealth >= 0.5f)
            healthBar.transform.Find("Mask").Find("Image").GetComponent<Image>().color = goodColor;
        if (stats.currentHealth / stats.maxHealth>= 0.25f && stats.currentHealth / stats.maxHealth < 0.5f)
            healthBar.transform.Find("Mask").Find("Image").GetComponent<Image>().color = middleColor;
        if (stats.currentHealth / stats.maxHealth < 0.25f)
            healthBar.transform.Find("Mask").Find("Image").GetComponent<Image>().color = badColor;
    }
}
 