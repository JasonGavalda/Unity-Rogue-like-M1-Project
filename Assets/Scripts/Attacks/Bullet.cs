using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box;

    public float lifeTime;
    private float timeLived;
    public float explosionRadius;
    private int damage;
    private int layers;
    private Collider2D[] colliders;
    private Vector2 vel;
    public string obstacleLayer;

    void Start()
    {
        timeLived = 0f;
        box = this.gameObject.GetComponent<BoxCollider2D>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        //layers += LayerMask.NameToLayer(obstacleLayer);
    }

    // Update is called once per frame
    public void launch(Vector2 direction, float speed, int pDamage, int intelligence, int layerstocheck)
    {
        if (rb == null)
        {
            Debug.Log("rb null");
            rb = this.GetComponent<Rigidbody2D>();
        }
        vel.x = 1; // direction.x * speed;
        vel.y = 0; // direction.y * speed;
        Debug.Log(vel);
        damage = pDamage + intelligence;
        layers = layerstocheck;
        Debug.Log("Launched");
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        colliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, explosionRadius, layers);
        foreach (Collider2D entity in colliders)
        {
            Debug.Log("boum");
            Entity ent = entity.gameObject.GetComponent<Entity>();
            if (ent != null) {
                ent.TakeDamage(damage);
                Destroy(this.gameObject);
            }

        }
        Debug.Log("test");
    }
    */

    private void Update()
    {
        if (rb.velocity.magnitude < vel.magnitude)
        {
            Debug.Log("inferiorspeed");
            rb.velocity = vel;
        }
        Debug.Log(vel);

        timeLived += Time.deltaTime;
        if(timeLived > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.gameObject.transform.position, explosionRadius);
    }
}
