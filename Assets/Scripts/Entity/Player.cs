using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : Entity
{
    public Attack basicAttack;
    public Attack specialAttack;

    Rigidbody2D RB;
    PhotonView PV;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PV.IsMine)
        {
            return;
        }
        Vector3 direction = new Vector3( Input.GetAxis( "Horizontal"), Input.GetAxis( "Vertical"), 0);

        if(direction.x != 0)
            this.transform.localScale = new Vector3(direction.x * stats.size / Mathf.Abs(direction.x), stats.size, stats.size);

        RB.MovePosition( RB.transform.position + direction * stats.speed * Time.deltaTime);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.Attack("basic");
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            this.Attack("special");
        }
        animateMove();
    }

    void Attack(string pString)
    {
        //animator.SetTrigger("basicAttack");
        //animator.SetTrigger("specialAttack");
        if (pString == "basic")
        {
            basicAttack.tryAttack();
            animateAttack();
        }

        else if (pString == "special")
            specialAttack.tryAttack();
    }

    override
    public Vector2 getShootTarget()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return pos;// return mouse pos
    }
}
