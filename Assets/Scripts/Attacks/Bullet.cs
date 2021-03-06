using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

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



    // Update is called once per frame
    public void launch(Vector2 direction, float speed, int pDamage, int intelligence, int layerstocheck)
    {
        timeLived = 0f;
        box = this.gameObject.GetComponent<BoxCollider2D>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        vel.x =  direction.x * speed;
        vel.y =  direction.y * speed;

        damage = pDamage + intelligence;
        layers = layerstocheck;
        layers += (int)Math.Pow(2,LayerMask.NameToLayer(obstacleLayer));
        rb.velocity = vel;
        Debug.Log("Launched");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        colliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, explosionRadius, layers);
        foreach (Collider2D collider in colliders)
        {
            Entity ent = collider.gameObject.GetComponent<Entity>();
            if (ent != null)
            {
                ent.TakeDamage(damage);
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("DestroyBullet", RpcTarget.All);
            }
            else if (collider.gameObject.layer == LayerMask.NameToLayer(obstacleLayer))
            {
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("DestroyBullet", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        timeLived += Time.deltaTime;
        if(timeLived > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.gameObject.transform.position, explosionRadius);
    }
}
