using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describe the behaviour of the zombie according to the simulation state.
/// </summary>
public class ZombiesController : MonoBehaviour, EnnemyController
{
    public int speed = 10;

    private float maxDistance = 0.2f;
    private GameObject player;
    private Animator anim;

    private bool aggro = false;
    private bool aggroOnAttack = false;
    private bool once = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // If the player is focused
        if (aggro || aggroOnAttack)
        {
            // Rotate the zombie in direction of the player
            transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(player.transform.position - transform.position),
            speed);

            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= maxDistance) {
                anim.SetBool("attack", true);
                anim.SetBool("walk", false);

                GetComponentInChildren<AudioSource>().gameObject.GetComponent<Zombie>().changemode(2);
            } else {
                anim.SetBool("attack", false);
                anim.SetBool("walk", true);
                GetComponentInChildren<AudioSource>().gameObject.GetComponent<Zombie>().changemode(1);
            }
        }
    }

    /// <summary>
    /// Switch the animation when the zombie has no more HP.
    /// </summary>
    public void die()
    {
        anim.SetBool("attack", false);
        anim.SetBool("walk", false);
        anim.SetBool("dead", true);

        aggro = false;
        aggroOnAttack = false;

        if (once)
        {
            once = false;
            GetComponentInChildren<AudioSource>().gameObject.GetComponent<Zombie>().changemode(4);
        }
    }
    /// <summary>
    /// Triggered when the player is too close from the zombie.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aggro = true;
        } 
    }
    /// <summary>
    /// Triggered when the player leaves the dangerous area.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aggro = false;
            anim.SetBool("walk", false);
            anim.SetBool("attack", false);
        }
    }
    /// <summary>
    /// Called when the player shoot the zombie. It's focusing the players
    /// </summary>
    public void respondOnAttack()
    {
        aggroOnAttack = true;
    }
}
