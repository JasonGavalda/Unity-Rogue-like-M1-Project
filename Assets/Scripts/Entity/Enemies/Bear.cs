﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Bear : Enemy
{
    public float forceX = 10f;
    public float forceY = 10f;
    public float nextWayPointDistance = 3f;


    Path path;
    int currentWayPoint = 0;
    bool EOP = false;
    bool activatePath;

    Seeker seeker;
    public AttackCAC punch;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        InvokeRepeating("animateMove", 0f, 0.5f);
    }

    void moveTowardPos(Vector2 pos)
    {
        Vector2 direction = (pos - rb.position).normalized;
        Vector2 force;
        int comp = 1;

        if (rb.velocity.magnitude <= stats.speed)
        {
            force.x = direction.x * this.forceX * Time.deltaTime;
            force.y = direction.y * this.forceY * Time.deltaTime;
        }
        else
        {
            force.x = -rb.velocity.x * this.forceX * Time.deltaTime;
            force.y = -rb.velocity.y * this.forceY * Time.deltaTime;
            comp = -1;
        }

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, pos);

        if (distance < nextWayPointDistance)
            currentWayPoint++;

        if (force.x > 0)
            transform.localScale = new Vector3(comp * -1f, 1f, 1f);
        else
            transform.localScale = new Vector3(comp * 1f, 1f, 1f);
    }

    void UpdatePath()
    {
        if (aTarget == null)
            return;

        if (seeker.IsDone() && aTarget)
            seeker.StartPath(rb.position, aTarget.transform.position, OnPathComplete);

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    IEnumerator Punching()
    {
        rb.velocity = new Vector2(0,0);
        yield return new WaitForSeconds(0.4f);
        if (punch.tryAttack())
            animateAttack();
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            EOP = true;
            return;
        }
        else
        {
            EOP = false;
        }

        float distanceWithTarget = Vector2.Distance(this.punch.center.transform.position, aTarget.transform.position);

        if(distanceWithTarget <= punch.radius && punch.canAttack())
        {
            StartCoroutine(Punching());
        }
        if (punch.canAttack())
            moveTowardPos((Vector2)path.vectorPath[currentWayPoint]);
        else
            moveTowardPos(new Vector2(this.transform.position.x- aTarget.transform.position.x, aTarget.transform.position.y));
    }
}

