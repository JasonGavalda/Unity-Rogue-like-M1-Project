using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    protected Stats playerStats;
    protected StatsModifier playerStatsModifier;
    protected Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        this.transform.localScale = new Vector3(playerStats.size, playerStats.size, playerStats.size);
    }

    public StatsModifier getStatsModifier() { return this.playerStatsModifier; }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3( Input.GetAxis( "Horizontal"), Input.GetAxis( "Vertical"), 0);
        this.transform.localScale = new Vector3(direction.x * playerStats.size / Mathf.Abs(direction.x), playerStats.size, playerStats.size);

        this.transform.position += direction * playerStats.speed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1")){
            //this.Attack();
        }

        if(playerStats.currentHealth <= 0){
            Debug.Log("you died");
        }
    }
}
