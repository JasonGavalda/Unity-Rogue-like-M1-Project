using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCACBoss : AttackCAC
{
    public GameObject center1;
    public GameObject center2;
    public GameObject center3;

    //public float radius;

    protected Collider2D[] colliders1;
    protected Collider2D[] colliders2;
    protected Collider2D[] colliders3;
    // Start is called before the first frame update
    public override void useAttack()
    {
        colliders = Physics2D.OverlapCircleAll(center.transform.position, radius, layersToCheck);
        colliders1 = Physics2D.OverlapCircleAll(center1.transform.position, radius, layersToCheck);
        colliders2 = Physics2D.OverlapCircleAll(center2.transform.position, radius, layersToCheck);
        colliders3 = Physics2D.OverlapCircleAll(center3.transform.position, radius, layersToCheck);
        foreach (Collider2D entity in colliders1)
        {
            if (entity.gameObject != this.gameObject)
            {
                Entity ent = entity.gameObject.GetComponent<Entity>();
                if (ent != null)
                    ent.TakeDamage(damage + attackUser.getStrengh());
            }

        }
    }
    public void useAttackMovement(int i)
    {
        switch (i)
        {
            case 0:
                colliders3 = Physics2D.OverlapCircleAll(center3.transform.position, radius, layersToCheck);
                foreach (Collider2D entity in colliders)
                {
                    if (entity.gameObject != this.gameObject)
                    {
                        Entity ent = entity.gameObject.GetComponent<Entity>();
                        if (ent != null)
                            ent.TakeDamage(damage + attackUser.getStrengh());
                    }

                }
                break;

            case 1:
                colliders3 = Physics2D.OverlapCircleAll(center1.transform.position, radius, layersToCheck);
                foreach (Collider2D entity in colliders1)
                {
                    if (entity.gameObject != this.gameObject)
                    {
                        Entity ent = entity.gameObject.GetComponent<Entity>();
                        if (ent != null)
                            ent.TakeDamage(damage + attackUser.getStrengh());
                    }

                }
                break;
            case 2:
                colliders3 = Physics2D.OverlapCircleAll(center2.transform.position, radius, layersToCheck);
                foreach (Collider2D entity in colliders2)
                {
                    if (entity.gameObject != this.gameObject)
                    {
                        Entity ent = entity.gameObject.GetComponent<Entity>();
                        if (ent != null)
                            ent.TakeDamage(damage + attackUser.getStrengh());
                    }

                }
                break;
            case 3:
                colliders3 = Physics2D.OverlapCircleAll(center3.transform.position, radius, layersToCheck);
                foreach (Collider2D entity in colliders3)
                {
                    if (entity.gameObject != this.gameObject)
                    {
                        Entity ent = entity.gameObject.GetComponent<Entity>();
                        if (ent != null)
                            ent.TakeDamage(damage + attackUser.getStrengh());
                    }

                }
                break;
        }
    }
}
