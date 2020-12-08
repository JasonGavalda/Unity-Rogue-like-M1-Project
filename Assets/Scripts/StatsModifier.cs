using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsModifier : MonoBehaviour
{
    private Stats stats;

    public void takeDamage(int pDamage) { stats.hp = stats.hp - (pDamage / stats.armor); }
}
