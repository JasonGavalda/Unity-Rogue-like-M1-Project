using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class getRooms : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("enemygentest", LoadSceneMode.Additive);
    }
}
