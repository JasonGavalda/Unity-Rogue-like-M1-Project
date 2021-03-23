using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Bear : Enemy
{
    public float forceX ;
    public float forceY ;
    public float nextWayPointDistance = 3f;
    

    Path path;
    int currentWayPoint = 0;
    bool EOP = false;
    bool activatePath;

    bool isPunching;

    Seeker seeker;
    public AttackCAC punch;

    void Start()
    {
        isPunching = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        InvokeRepeating("animateMove", 0f, 0.5f);
    }

    void moveTowardPos(Vector2 pos)
    {
        Vector2 direction = (pos - rb.position).normalized;
        Vector2 force;

        force.x = direction.x * stats.speed * forceX;
        force.y = direction.y * stats.speed * forceY;

        rb.AddForce(force);


        float distance = Vector2.Distance(rb.position, pos);

        if (distance < nextWayPointDistance)
            currentWayPoint++;

        if (force.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            healthBar.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            healthBar.transform.localScale = new Vector3(1f, 1f, 1f);
        }

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
        yield return new WaitForSeconds(0.3f);
        if (punch.tryAttack())
            animateAttack();
        isPunching = true;
        yield return new WaitForSeconds(0.65f);
        isPunching = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null || aTarget == null)
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
        else if(!isPunching)
        {
            Vector2 backwardDirection = new Vector2(this.transform.position.x - aTarget.transform.position.x, aTarget.transform.position.y).normalized;
            moveTowardPos(new Vector2(6 * backwardDirection.x + this.transform.position.x, aTarget.transform.position.y));
        }

        
    }
}

