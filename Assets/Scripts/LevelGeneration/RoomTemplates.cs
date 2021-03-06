using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.IO;

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
    

    void Start()
    {
        Debug.Log("seed" + GameObject.Find("Seed").GetComponent<Seed>().GetSeedValue());
        Random.InitState(GameObject.Find("Seed").GetComponent<Seed>().GetSeedValue());
    }

    public List<GameObject> GetRooms()
    {
        return rooms;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = rooms.Count-1; i >=0; i--)
            {
                    if (rooms[i].name == "B(Clone)" || rooms[i].name == "B2(Clone)")
                    {
                        Instantiate(bossRooms[0], rooms[i].transform.position, Quaternion.identity);
                        Destroy(rooms[i]);
                        if (PhotonNetwork.IsMasterClient)
                            PhotonNetwork.Instantiate(Path.Combine("Prefab","Entities","Grabbit"), rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                        break;
                    }
                    else if (rooms[i].name == "L(Clone)" || rooms[i].name == "L2(Clone)")
                    {
                        Instantiate(bossRooms[1], rooms[i].transform.position, Quaternion.identity);
                        Destroy(rooms[i]);
                        if (PhotonNetwork.IsMasterClient)
                            PhotonNetwork.Instantiate(Path.Combine("Prefab", "Entities", "Grabbit"), rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                        break;
                    }
                    else if (rooms[i].name == "R(Clone)" || rooms[i].name == "R2(Clone)")
                    {
                        Instantiate(bossRooms[2], rooms[i].transform.position, Quaternion.identity);
                        Destroy(rooms[i]);
                        if (PhotonNetwork.IsMasterClient)
                            PhotonNetwork.Instantiate(Path.Combine("Prefab", "Entities", "Grabbit"), rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                        break;
                    }
                    else if (rooms[i].name == "T(Clone)" || rooms[i].name == "T2(Clone)")
                    {
                        Instantiate(bossRooms[3], rooms[i].transform.position, Quaternion.identity);
                        Destroy(rooms[i]);
                        if (PhotonNetwork.IsMasterClient)
                            PhotonNetwork.Instantiate(Path.Combine("Prefab", "Entities", "Grabbit"), rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                        break;
                    }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
