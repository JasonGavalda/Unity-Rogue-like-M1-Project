using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected Stats playerStat;

    protected Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2( Input.GetAxis( "Horizontal"), Input.GetAxis( "Vertical"));

    }
}
