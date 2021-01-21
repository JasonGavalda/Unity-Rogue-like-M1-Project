using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public int damage;

    public float cooldown;
    float nextAttackTime = 0f;
    protected int layersToCheck;

    protected Entity attackUser;

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
    }

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
