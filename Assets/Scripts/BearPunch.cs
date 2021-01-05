using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCAC : Attack
{
    //private bool playerInRange = false;
    public GameObject center;
    public float radius;
    protected Collider2D[] colliders;
    public bool isPlayer;

    protected int layersToCheck;


    void Start()
    {
        layersToCheck = gameObject.GetComponent<Entity>().getLayers();
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player")) ;
    //    {
    //        playerInRange = true;
    //    }
    //}
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player")) ;
    //    {
    //        playerInRange = false;
    //    }
    //}
    protected override void useAttack()
    {
        colliders = Physics2D.OverlapCircleAll(center.transform.position,radius,layersToCheck);
        foreach (Collider2D entity in colliders)
        {
            if (entity.gameObject != this.gameObject)
                entity.gameObject.GetComponent<Entity>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(center.transform.position,radius);
    }
}

