using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : Entity
{
    public Attack basicAttack;
    public Attack specialAttack;

    [SerializeField]
    private Camera cam;

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
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (direction.x != 0)
            this.transform.localScale = new Vector3(direction.x * stats.size / Mathf.Abs(direction.x), stats.size, stats.size);

        RB.MovePosition(RB.transform.position + direction * stats.speed * Time.deltaTime);
    }

    private void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //this.Attack("basic");
            if (basicAttack.canAttack())
            {
                animateAttack();
                
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            //this.Attack("special");
            if (specialAttack.canAttack())
            {
                animateSpecialAttack();
                
            }
        }
        animateMovePayer();
        
    }

    void Attack(string pString)
    {
        //animator.SetTrigger("basicAttack");
        //animator.SetTrigger("specialAttack");
        if (pString == "basic")
        {
            //basicAttack.tryAttack();
            //animateAttack();
            basicAttack.useAttack();
        }

        else if (pString == "special")
        {
            //specialAttack.tryAttack();
            //animateSpecialAttack(); // L'animation se charge de lancer l'attaque au bon moment
            specialAttack.useAttack();
        }
    }

    override
    public Vector2 getShootTarget()
    {
        Vector2 pos = GetComponentInChildren<Camera>().ScreenToWorldPoint(Input.mousePosition);
        return pos;// return mouse pos
    }
}
