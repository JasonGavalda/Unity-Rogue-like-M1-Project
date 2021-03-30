using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Attack
{
    private float dashTime;
    public float dashTimeStart;
    public float dashSpeed;
    public float direction;

    override public void useAttack() { 
        Rigidbody2D rb = attackUser.getRigidBody();
        dashTime = dashTimeStart;
        if (Input.GetAxis("Horizontal") != 0)
        {
            direction = Input.GetAxis("Horizontal");
        }
        while (dashTime >= 0)
        {
            rb.MovePosition(new Vector3(direction * dashSpeed, 0, 0));
            dashTime -= Time.deltaTime;
        }
       
    }
    
}
