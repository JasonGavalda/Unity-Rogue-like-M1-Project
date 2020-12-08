using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsModifier : MonoBehaviour
{
    private Stats stats;

    public void TakeDamage(int pDamage) { stats.currentHealth = stats.currentHealth - (pDamage / stats.armor); }
}
