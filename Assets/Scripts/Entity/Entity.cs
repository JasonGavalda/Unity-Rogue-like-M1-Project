using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        if (stats.currentHealth <= 0)
            this.Die();
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
}
 