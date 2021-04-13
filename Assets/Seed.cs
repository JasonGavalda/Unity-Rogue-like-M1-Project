using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviourPunCallbacks
{
    public int seedValue = -1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public int GetSeedValue()
    {
        return this.seedValue;
    }
}
