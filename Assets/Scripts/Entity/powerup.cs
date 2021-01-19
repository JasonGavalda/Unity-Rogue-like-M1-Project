using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    public GameObject pickupEffect;
    public int healValue = 1;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Entity>().Heal(healValue);
            Debug.Log("PowerUp picked up");
            Destroy(gameObject);
        }
    }
}
