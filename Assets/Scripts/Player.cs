﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    protected Rigidbody2D rb;

    public Attack basicAttack;
    public Attack specialAttack;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        this.transform.localScale = new Vector3(stats.size, stats.size, stats.size);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3( Input.GetAxis( "Horizontal"), Input.GetAxis( "Vertical"), 0);

        if(direction.x != 0)
            this.transform.localScale = new Vector3(direction.x * stats.size / Mathf.Abs(direction.x), stats.size, stats.size);

        this.transform.position += direction * stats.speed * Time.deltaTime;

        if (Input.GetButtonDown("Mouse1")){
            this.Attack("basic"); 
        }
        else if (Input.GetButtonDown("Mouse2"))
        {
            this.Attack("special");
        }
    }

    void Attack(string pString)
    {
        //animator.SetTrigger("basicAttack");
        //animator.SetTrigger("specialAttack");
        if (pString == "basic")
            basicAttack.tryAttack();
        else if (pString == "special")
            specialAttack.tryAttack();
    }
}
