using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    private float distTarget;
    private float distNewTarget;

    protected GameObject aTarget;
    
    public void foundTarget(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (aTarget == null)
                aTarget = collider.gameObject;
            else
            {
                if(aTarget != collider.gameObject) // if another player is detected, if its closer then it'll go to it.
                {
                    Debug.Log("another player detected");
                    distTarget = (aTarget.transform.position - this.gameObject.transform.position).magnitude;
                    distNewTarget = (collider.gameObject.transform.position - this.gameObject.transform.position).magnitude;

                    if (distTarget - distNewTarget < 0)
                        aTarget = collider.gameObject;
                }
            }
        }      
    }
}
