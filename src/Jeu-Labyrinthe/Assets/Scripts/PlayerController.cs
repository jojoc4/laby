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

    public CharacterController _charCont;
    public AudioSource walkingSource;
    public AudioSource screamingSource;

    public Camera MainCamera;
    public Camera Level2Camera;

    private GameObject torch;

    private int health;
    private float gravity = 0.25f;
    private bool falling;
    private bool levelling;
    private int level;

    // Use this for initialization
    void Start()
    {
        this.health = this.startingHealth;
        this.torch = GameObject.FindWithTag("Torch");
        this.falling = true;
        this.levelling = false;
        this.level = 1;

        Level2Camera.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Lights"))
        {
            toggleTorch();
        }
    }
    
    void FixedUpdate()
    {
        float verticalVelocity = 0f;

        //Only fall when the player is not on the ground
        if (_charCont.isGrounded || !falling)
        {
            falling = false;
            //If advancing to the next level
            if(levelling)
            {
                levelling = false;

                //change camera
                MainCamera.gameObject.SetActive(true);
                Level2Camera.gameObject.SetActive(false);

                //Play next level's theme music
                MainCamera.gameObject.GetComponentInChildren<Music>().changeLevel(++this.level);
            }
        } else
        {
            verticalVelocity = -gravity;
            falling = true;
        }

        Vector3 movement = new Vector3(0f, verticalVelocity, 0f);

        //Allow movement only when on the ground
        if (!falling)
        {
            movement.x = Input.GetAxis("Horizontal") * speed;
            movement.z = Input.GetAxis("Vertical") * speed;
            movement = Vector3.ClampMagnitude(movement, speed); //Limits the max speed of the player

            movement *= Time.deltaTime;     //Ensures the speed the player moves does not change based on frame rate
            movement = transform.TransformDirection(movement);

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

        //apply movement
        _charCont.Move(movement);
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
        GameObject.FindWithTag("Level1").SetActive(false);

        falling = true;
        levelling = true;

        Level2Camera.gameObject.SetActive(true);
        MainCamera.gameObject.SetActive(false);
    }
}
