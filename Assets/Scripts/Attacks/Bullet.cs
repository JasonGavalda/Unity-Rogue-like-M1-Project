using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float lifeTime;
    private float timeLived = 0f;
    public float explosionRadius;
    private int damage;
    private int layers;
    private Collider2D[] colliders;

    // Update is called once per frame
    public void launch(Vector2 direction, float speed, int pDamage, int intelligence, int layerstocheck)
    {
        rb.velocity = direction * speed;
        damage = pDamage + intelligence;
        layers = layerstocheck;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        colliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, explosionRadius, layers);
        foreach (Collider2D entity in colliders)
        {
            if (entity.gameObject != this.gameObject)
            {
                Entity ent = entity.gameObject.GetComponent<Entity>();
                if (ent != null)
                    ent.TakeDamage(damage);
            }
        }
    }

    private void Update()
    {
        timeLived += Time.deltaTime;
        if(timeLived > lifeTime)
        {
            Destroy(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.gameObject.transform.position, explosionRadius);
    }
}
