using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observation : MonoBehaviour
{
    private Enemy parentEnemy;

    private void Start()
    {
        parentEnemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        parentEnemy.foundTarget(collision);
    }
}
