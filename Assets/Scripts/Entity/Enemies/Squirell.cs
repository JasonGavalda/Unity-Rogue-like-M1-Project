using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirell : Enemy
{
    public float distanceMax;
    public float animationTime;
    private float currentAnimationFrame;

    public Attack shoot;

    void Update()
    {
        if (aTarget == null)
            return;

        if (currentAnimationFrame > 0)
        {
            currentAnimationFrame -= Time.deltaTime;
            return;
        }


        if (this.shoot.canAttack())
        {
            this.shoot.tryAttack();
            currentAnimationFrame = animationTime;
        }


        Vector2 distance = new Vector2(aTarget.transform.position.x - this.transform.position.x, aTarget.transform.position.y - this.transform.position.y);

        if (distance.magnitude < distanceMax)
        {
            Vector2 backwardRun = new Vector2(-distance.normalized.x * stats.speed, -distance.normalized.y * stats.speed);
            this.rb.AddForce(backwardRun);
            //this.rb.velocity = backwardRun;
        }
        else
            this.rb.AddForce(-stats.speed * rb.velocity);
            //this.rb.velocity = new Vector2(0,0);

    }

}
