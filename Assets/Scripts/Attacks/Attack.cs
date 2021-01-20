using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public int damage;

    public float cooldown;
    float nextAttackTime = 0f;

    public bool tryAttack()
    {
        if (nextAttackTime <= 0f)
        {
            nextAttackTime = cooldown;
            useAttack();
            return true;
        }
        else
            Debug.Log("attack on cd");
        return false;
    }

    public bool canAttack()
    {
        return (nextAttackTime <= 0f);
    }

    private void Update()
    {
        if (nextAttackTime > 0f)
            nextAttackTime -= Time.deltaTime;
    }

    abstract protected void useAttack();
}
