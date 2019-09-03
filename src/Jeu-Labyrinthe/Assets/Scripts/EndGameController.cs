using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If the player hits the end game detector, end the game
        if (other.tag == "Player")
        {
            Timer.stopTimer();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<SceneChanger>().openEnd();
        }
    }
}
