using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the player and his interaction with the world
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;
    public int startingHealth = 20;

    public CharacterController charCont;
    public AudioSource walkingSource;
    public AudioSource screamingSource;

    public Camera mainCamera;
    public Camera level2Camera;

    private GameObject torch;

    private int health;
    private float gravity = 0.25f;
    private bool levelling;
    private bool reachedLevel;
    private int level;
    private Vector3 movement;

    // Use this for initialization
    void Start()
    {
        this.health = this.startingHealth;
        this.torch = GameObject.FindWithTag("Torch");
        this.levelling = false;
        this.reachedLevel = false;
        this.level = 1;

        this.movement = new Vector3(0f, 0f, 0f);

        level2Camera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetButtonDown("Lights"))
            {
                toggleTorch();
            }

            if (levelling)
            {
                //change camera
                level2Camera.gameObject.SetActive(true);
                mainCamera.gameObject.SetActive(false);
            }
            else if (reachedLevel)
            {
                //change camera
                mainCamera.gameObject.SetActive(true);
                level2Camera.gameObject.SetActive(false);

                //stop levelling
                levelling = false;
                reachedLevel = false;

                //Play next level's theme music
                mainCamera.gameObject.GetComponentInChildren<Music>().changeLevel(++this.level);
            }

            //apply movement once per frame, so charCont.isGrounded doesn't get all f****d up
            charCont.Move(movement);
        }
    }
    
    void FixedUpdate()
    {
        //Only move when on the ground
        if (charCont.isGrounded)
        {
            if (levelling)
            {
                levelling = false;
                reachedLevel = true;
            }

            movement.x = Input.GetAxis("Horizontal") * speed;
            movement.z = Input.GetAxis("Vertical") * speed;
            movement = Vector3.ClampMagnitude(movement, speed); //Limits the max speed of the player

            movement *= Time.deltaTime;     //Ensures the speed the player moves does not change based on frame rate
            movement = transform.TransformDirection(movement);

            //Walking sound management
            if (movement.x != 0 || movement.z != 0)
            {
                if (!walkingSource.isPlaying)
                    walkingSource.Play();
            }
            else
            {
                if (walkingSource.isPlaying)
                    walkingSource.Stop();
            }
        }
        else
        {
            movement.y = -gravity;
            movement.x = 0f;
            movement.z = 0f;
        }
    }

    /// <summary>
    /// Toggles the player's torch
    /// </summary>
    private void toggleTorch()
    {
        torch.SetActive(!torch.activeSelf);
    }

    public int getHealth() { return this.health; }

    /// <summary>
    /// Hurts the player when an ennemy attacks
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public void hurt(int damage)
    {
        this.health -= damage;
        if (!screamingSource.isPlaying)
        {
            screamingSource.Play();
        }
    }

    /// <summary>
    /// Heals the player when using a health pack
    /// </summary>
    /// <param name="health">Healing amount</param>
    public void heal(int health)
    {
        this.health = ((this.health + health) > this.startingHealth) ? this.startingHealth : (this.health + health);
    }


    private void LateUpdate()
    {
        //Open the game over scene if the player dies
        if (this.health <= 0)
            GetComponent<SceneChanger>().openGameOver();
    }

    /// <summary>
    /// Makes the player fall to the next level and activates the external camera
    /// </summary>
    public void fallToNextLevel()
    {
        string levelName = "Level" + this.level;
        GameObject.FindWithTag(levelName).SetActive(false);

        levelling = true;
        reachedLevel = false;

        //Stop walking sound as we can't walk while falling
        if (walkingSource.isPlaying)
            walkingSource.Stop();
    }
}
