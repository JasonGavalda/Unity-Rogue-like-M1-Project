﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private bool isEnraged = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isEnraged = setState(); 
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
