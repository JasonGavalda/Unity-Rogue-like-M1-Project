using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected Stats playerStats;

    protected Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3( Input.GetAxis( "Horizontal"), Input.GetAxis( "Vertical"), 0);
        this.transform.localScale = new Vector3(direction.x * playerStats.size / Mathf.Abs(direction.x), playerStats.size, playerStats.size);

        this.transform.position += direction * playerStats.speed * Time.deltaTime;
        Debug.Log(direction);
    }
}
