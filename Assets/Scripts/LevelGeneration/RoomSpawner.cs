using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 => porte Sud
    // 2 => porte Nord
    // 3 => porte Ouest
    // 4 => porte Est

    private RoomTemplates templates; // Template conteant les listes de rooms.
    private int rand; // Index aléatoire pour le choix d'une room dans une liste donnée.
    public bool spawned = false;

    public float waitTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, waitTime); // Détruit les points de spawn superflus pour alléger l'utilisation de mémoire.
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.02f); // Spawn des pièces.
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                // On créé une Room avec une porte Sud.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                // On créé une Room avec une porte Nord.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // On créé une Room avec une porte Ouest.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // On créé une Room avec une porte Est.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) // Appelée à chaque fois que le point de spawn entre en collision.
    {
        if (other.CompareTag("SpawnPoint"))
        {
            try
            {
                if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    try
                    {
                        //Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                    }
                    catch (System.NullReferenceException e)
                    {
                        other.GetComponent<RoomSpawner>().spawned = false;
                        //Destroy(gameObject);
                    }
                }
                spawned = true;
            }
            catch (System.NullReferenceException e)
            { 
                            
            }
            
        }
    }
}
