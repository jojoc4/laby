using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the hurting of the player when he is attacked by an ennemy
/// </summary>
public class EnnemyAttack : MonoBehaviour
{
    public int attackDamage = 1;

    private void OnTriggerEnter(Collider other)
    {
        //Hurt the player when he gets hit by the ennemy
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().hurt(this.attackDamage);
        }
    }
}
