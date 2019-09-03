using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages an ennemy's life, hurts and heals him and kills him
/// </summary>
public class LifeAndDeath : MonoBehaviour
{
    public int startingHP;

    private int hp;

    // Start is called before the first frame update
    void Start()
    {
        this.hp = this.startingHP;
    }

    private void LateUpdate()
    {
        if(this.hp <= 0)
        {
            die();
        }
    }

    /// <summary>
    /// Hurts an ennemy by the specified amount of damage
    /// </summary>
    /// <param name="damage">Damage amount</param>
    public void hurt(int damage)
    {
        this.hp -= damage;
        this.GetComponent<EnnemyController>().respondOnAttack();
    }

    /// <summary>
    /// Heals the ennemy by the specified amaount of health points
    /// </summary>
    /// <param name="health">Health amount</param>
    public void heal(int health)
    {
        this.hp = (this.hp + health >= this.startingHP) ? this.startingHP : this.hp + health;
    }

    /// <summary>
    /// Kills the ennemy
    /// </summary>
    public void die()
    {
        this.GetComponent<EnnemyController>().die();
    }
}
