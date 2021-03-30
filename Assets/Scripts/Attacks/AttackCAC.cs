using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCAC : Attack
{
    //private bool playerInRange = false;
    public GameObject center;
    public float radius;
    protected Collider2D[] colliders;

    public override void useAttack()
    {
        colliders = Physics2D.OverlapCircleAll(center.transform.position,radius,layersToCheck);
        foreach (Collider2D entity in colliders)
        {
            if (entity.gameObject != this.gameObject)
            {
                Entity ent = entity.gameObject.GetComponent<Entity>();
                if(ent != null)
                    ent.TakeDamage(damage + attackUser.getStrengh());
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(center.transform.position,radius);
    }

}

