using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCAC : Attack
{
    //private bool playerInRange = false;
    public GameObject center;
    public float radius;
    protected Collider2D[] colliders;

    protected override void useAttack()
    {
        //PLAY ANIM
        Debug.Log("AttackUsed");
        colliders = Physics2D.OverlapCircleAll(center.transform.position,radius,layersToCheck);
        foreach (Collider2D entity in colliders)
        {
            Debug.Log(entity.tag);
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

