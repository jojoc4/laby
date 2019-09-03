using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Describe the behaviour of the rabbit according to the simulation state.
/// </summary>
public class RabbitController : MonoBehaviour, EnnemyController
{

    public int speed = 10;
    public int attackDamage;
    public double maxDistance = 0.05;
    public float smooth = 2.0F;

    public AudioSource attackAudioSource;
    public AudioSource ambiantAudioSource;

    private GameObject player;
    private PlayerController playerController;
    private Animator anim;
    private bool aggro = false;
    private bool aggroOnAttack = false;

    private bool once = true;
    private float lastFired;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();

    }

    void FixedUpdate()
    {
        // If the player is focused
        if (aggro || aggroOnAttack)
        {
            // Rotate the rabbit in direction of the player
            transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(player.transform.position - transform.position),
            speed);

            if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance && (Time.time - lastFired) > 1)
            {
                anim.SetBool("walk", false);
                playerController.hurt(attackDamage);
                if(!attackAudioSource.isPlaying)
                    attackAudioSource.Play();
                
                lastFired = Time.time;
            }
            else
            {
                anim.SetBool("walk", true);
            }
        } else
        {
            anim.SetBool("walk", true);
        }
    }
    /// <summary>
    /// Switch the animation when the rabbit has no more HP.
    /// </summary
    public void die()
    {
        anim.SetBool("walk", false);
        anim.SetBool("dead", true);
        aggro = false;
        aggroOnAttack = false;

        if (once)
        {
            once = false;
            ambiantAudioSource.Stop();
        }
    }

    /// <summary>
    /// Triggered when the player is too close from the rabbit. If the rabbit hits a wall, it rotates.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aggro = true;
        }
        if(other.CompareTag("Wall") && !aggro && !aggroOnAttack)
        {
            Rotate();
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
        }
    }
    /// <summary>
    /// Rotate the rabbit.
    /// </summary>
    private void Rotate()
    {
        transform.Rotate(0, Random.Range(80f, 180), 0);
    }
    /// <summary>
    /// Called when the player shoot the rabbit. It's focusing the players.
    /// </summary>
    public void respondOnAttack()
    {
        aggroOnAttack = true;
    }
}
