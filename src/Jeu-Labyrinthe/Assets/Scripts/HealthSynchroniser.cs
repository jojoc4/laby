using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates the GUI based on the player's remaining health
/// </summary>
public class HealthSynchroniser : MonoBehaviour
{
    private float health;
    public Text text;
    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        text.text = "100%";
        img.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        health = (float)GameObject.FindWithTag("Player").GetComponent<PlayerController>().getHealth() / (float)GameObject.FindWithTag("Player").GetComponent<PlayerController>().startingHealth * 100f;
        text.text = health + "%";
        img.fillAmount = health / 100;
    }
}
