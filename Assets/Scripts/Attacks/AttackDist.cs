using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDist : Attack
{
    // Start is called before the first frame update
    public GameObject projectile;
    public GameObject cannon;
    public float bulletSpeed;

    private Vector2 shootTarget;
    
    public override void useAttack()
    {
        shootTarget = attackUser.getShootTarget();
        Vector2 directionTarget = new Vector2(shootTarget.x - this.transform.position.x, shootTarget.y - this.transform.position.y);
        GameObject bullet = Instantiate(projectile, cannon.transform.position, cannon.transform.rotation);
        bullet.GetComponent<Bullet>().launch(directionTarget.normalized, bulletSpeed, damage, attackUser.getInt(), layersToCheck);       
    }
}
