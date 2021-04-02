using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using Photon.Pun;


public abstract class Entity : MonoBehaviour
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
    private SpriteRenderer sprite;
    private float invulnerableTime = 0;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();

        sprite = GetComponent<SpriteRenderer>();
        
        if (sprite == null)
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
        }

        rb = GetComponent<Rigidbody2D>();

        this.transform.localScale = new Vector3(stats.size, stats.size, stats.size);
        isDead = false;
        stats.currentHealth = stats.maxHealth;
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
        /*
        Debug.Log("took damage : " + pDamage);
        Debug.Log("reduce to : " + pDamage / stats.armor);
        Debug.Log("hp going from: " + stats.currentHealth);
        **/
        invulnerableTime = stats.invulnerabilityTime;
        StartCoroutine(invulnerabily());
        StartCoroutine(hitBlink());
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
        int blinks = 10;

        if (sprite == null)
            yield break;

        Color col = new Color(sprite.color.r, sprite.color.g, sprite.color.b);
        for (int i = 0; i < blinks; i++)
        {
            sprite.color = new Color(col.r + 0.2f, col.g, col.b, 0.6f);
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
        if (!PV.IsMine)
        {
            animator.SetTrigger("Attack");
        }
        
    }

    public void animateSpecialAttack()
    {
        if (!PV.IsMine)
        {
            animator.SetTrigger("SpecialAttack");
        }
        
    }

    public void UpdateHealth()
    {
        if (healthBar == null)
            return;

        print(stats.currentHealth / stats.maxHealth);
        healthBar.GetComponent<Scrollbar>().size = stats.currentHealth / stats.maxHealth;
        SetColor();
    }

    void SetColor()
    {
        if (healthBar == null)
            return;
        if (stats.currentHealth / stats.maxHealth >= 0.5f)
            healthBar.transform.Find("Mask").Find("Image").GetComponent<Image>().color = goodColor;
        if (stats.currentHealth / stats.maxHealth>= 0.25f && stats.currentHealth / stats.maxHealth < 0.5f)
            healthBar.transform.Find("Mask").Find("Image").GetComponent<Image>().color = middleColor;
        if (stats.currentHealth / stats.maxHealth < 0.25f)
            healthBar.transform.Find("Mask").Find("Image").GetComponent<Image>().color = badColor;
    }

    //------------------ For the Boss -------------------------------
    protected bool setState()
    {
        if (stats.currentHealth / stats.maxHealth <= 0.9f)
            return true;
        return false;
    }
    //---------------------------------------------------------------

    abstract public Vector2 getShootTarget();

    public Rigidbody2D getRigidBody()
    {
        return rb;
    }
}
 