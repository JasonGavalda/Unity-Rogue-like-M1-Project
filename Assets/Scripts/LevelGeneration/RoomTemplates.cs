﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] bossRooms;

    public List<GameObject> rooms;

    public GameObject closedRoom;

    public float waitTime; // Temps d'attente pour faire spawner le boss
    private bool spawnedBoss;
    // public GameObject boss;

    // Update is called once per frame
    void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = rooms.Count-1; i >=0; i--)
            {
                if(i == rooms.Count-1)
                {
                    if (rooms[i].name == "B(Clone)" || rooms[i].name == "B2(Clone)")
                    {
                        Instantiate(bossRooms[0], rooms[i].transform.position, Quaternion.identity);
                        Destroy(rooms[i]);
                        // Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                    }
                    else if (rooms[i].name == "L(Clone)" || rooms[i].name == "L2(Clone)")
                    {
                        Instantiate(bossRooms[1], rooms[i].transform.position, Quaternion.identity);
                        Destroy(rooms[i]);
                        // Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                    }
                    else if (rooms[i].name == "R(Clone)" || rooms[i].name == "R2(Clone)")
                    {
                        Instantiate(bossRooms[2], rooms[i].transform.position, Quaternion.identity);
                        Destroy(rooms[i]);
                        // Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                    }
                    else if (rooms[i].name == "T(Clone)" || rooms[i].name == "T2(Clone)")
                    {
                        Instantiate(bossRooms[3], rooms[i].transform.position, Quaternion.identity);
                        Destroy(rooms[i]);
                        // Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                    }
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
