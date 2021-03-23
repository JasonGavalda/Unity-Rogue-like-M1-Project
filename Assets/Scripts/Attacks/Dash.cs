using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Attack
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    override protected void useAttack() { 
        Rigidbody2D rb = attackUser.getRigidBody();
        rb.MovePosition(rb.transform.position + new Vector3(20,0,0));
    }
    
}
