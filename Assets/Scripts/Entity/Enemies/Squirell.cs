using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirell : Enemy
{
    public float distanceMax;
    public float animationTime;
    private float currentAnimationFrame;

    public Attack shoot;

    void FixedUpdate()
    {
        if (aTarget == null)
            return;

        if (currentAnimationFrame > 0)
        {
            currentAnimationFrame -= Time.deltaTime;
            return;
        }


        if (this.shoot.tryAttack())
        {
            this.shoot.useAttack();
            currentAnimationFrame = animationTime;
        }


        Vector2 distance = new Vector2(aTarget.transform.position.x - this.transform.position.x, aTarget.transform.position.y - this.transform.position.y);
        if (distance.magnitude < distanceMax)
        {
            distance.Normalize();
            Vector2 backwardRun = new Vector2(-distance.x * stats.speed, -distance.y * stats.speed);
            this.rb.AddForce(backwardRun);
            //this.rb.velocity = backwardRun;
        }
        else
            this.rb.AddForce(-stats.speed * rb.velocity * 5);
            //this.rb.velocity = new Vector2(0,0);

    }

    override public void Die()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        //Invoke("DestroyEntity", 0f);
        Destroy(this.gameObject);
        //this.enabled = false;
    }

}
