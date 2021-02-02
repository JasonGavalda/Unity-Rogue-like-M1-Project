using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCAC : Attack
{
    //private bool playerInRange = false;
    public GameObject center;
    public float radius;
    protected Collider2D[] colliders;


    private int layersToCheck;

    private Entity attackUser;
    private int attackerStr;

    private void Start()
    {
        attackUser = this.gameObject.GetComponent<Entity>();
        if (attackUser == null)
            attackUser = this.gameObject.GetComponentInParent<Entity>();
        if (attackUser == null)
        {
            Debug.Log("no entity for attack");
            return;
        }

        layersToCheck = attackUser.getLayers();
        attackerStr = attackUser.getStrengh();
    }

    protected override void useAttack()
    {
        //PLAY ANIM
        Debug.Log("AttackUsed");
        colliders = Physics2D.OverlapCircleAll(center.transform.position,radius,layersToCheck);
        foreach (Collider2D entity in colliders)
        {
            if (entity.gameObject != this.gameObject)
            {
                Entity ent = entity.gameObject.GetComponent<Entity>();
                if(ent != null)
                    ent.TakeDamage(damage + attackerStr);
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(center.transform.position,radius);
    }

}

