using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDist : Attack
{
    // Start is called before the first frame update
    public GameObject cannon;
    public float bulletSpeed;

    private Vector2 shootTarget;
    private bool targetSet = false;
    
    protected override void useAttack()
    {
        if (!targetSet)
            Debug.Log("Target not set");
        else
        {
            Instantiate(cannon);
            cannon.GetComponent<Bullet>().launch(shootTarget, bulletSpeed, damage, attackUser.getInt(), layersToCheck);
        }
    }

    private void setTarget(Vector2 pTarget)
    {
        shootTarget = pTarget;
        targetSet = true;
    }
}
