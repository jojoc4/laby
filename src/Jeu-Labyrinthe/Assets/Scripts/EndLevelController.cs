using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the end of a level and sends the player to the next level
/// </summary>
public class EndLevelController : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Send the player to the next level when he hits the end level detector
        if (other.tag == "Player")
        {
            player.GetComponent<PlayerController>().fallToNextLevel();
        }
    }
}
