using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Boss : Enemy
{
    private bool isEnraged = false;
    private bool isAfraid = true;
    public float forceX;
    public float forceY;
    public float nextWayPointDistance = 3f;


    Path path;
    int currentWayPoint = 0;
    bool EOP = false;
    bool activatePath;

    bool isPunching;

    Seeker seeker;
    public AttackCACBoss punch;

    void Start()
    {
        isPunching = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        //InvokeRepeating("animateMove", 0f, 0.5f);
    }

    public void setAfraid()
    {
        isAfraid = false;
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
        {
            animateMove();
            seeker.StartPath(rb.position, aTarget.transform.position, OnPathComplete);

        }
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
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.3f);
        if (punch.tryAttack())
            animateAttack();
        isPunching = true;
        yield return new WaitForSeconds(0.65f);
        isPunching = false;

    }

    void PunchingMovement(int i)
    {
        punch.useAttackMovement(i);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.stats.currentHealth < this.stats.maxHealth && !isEnraged)
        {
            isEnraged = true;
            animateTransformation();
        }

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
        float distanceWithTarget1 = Vector2.Distance(this.punch.center1.transform.position, aTarget.transform.position);
        float distanceWithTarget2 = Vector2.Distance(this.punch.center2.transform.position, aTarget.transform.position);
        float distanceWithTarget3 = Vector2.Distance(this.punch.center3.transform.position, aTarget.transform.position);

        if ((distanceWithTarget <= punch.radius || distanceWithTarget1 <= punch.radius || distanceWithTarget2 <= punch.radius || distanceWithTarget3 <= punch.radius) && punch.canAttack())
        {
            StartCoroutine(Punching());
        }
        if (punch.canAttack() && !isAfraid)
            moveTowardPos((Vector2)path.vectorPath[currentWayPoint]);
        else if (!isPunching || isAfraid)
        {
            Vector2 backwardDirection = new Vector2(this.transform.position.x - aTarget.transform.position.x, this.transform.position.y-aTarget.transform.position.y).normalized;
            moveTowardPos(new Vector2(6 * backwardDirection.x + this.transform.position.x, 6 * backwardDirection.y+ aTarget.transform.position.y));
        }


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