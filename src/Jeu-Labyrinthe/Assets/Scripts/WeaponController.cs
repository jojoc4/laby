using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's weapon
/// </summary>
public class WeaponController : MonoBehaviour
{
    public ParticleSystem flame;            // The flame particle to instantiate
    public float shotsPerSec = 10;          // The number of bullets fired per second
    public float range = 100;               // The weapon's max range
    public int power = 1;                   // The weapon's damage per shot
    public Transform gunEnd;                // Transform located at the end of the gun's barrel

    private float lastfired;                // The value of Time.time at the last firing moment
    private Camera fpsCam;                  // The main camera of the FPS view

    public Transform recoilMod;             // Transform that's used to calculate the recoil's rotation angle
    public GameObject weapon;               // The weapon that has to be rotated when recoil is applied
    public float maxRecoil_x = -10f;        // The maximum recoil angle
    public float recoilSpeed = 0.1f;        // The recoil's speed
    private float recoil;                   // The current recoil angle

    // Start is called before the first frame update
    void Start()
    {
        lastfired = 0f;
        fpsCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        recoil = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Allow shooting only when unpaused and only at maximum cadency
        if (Time.timeScale != 0 && (Input.GetButton("Fire1") && (Time.time - lastfired) > 1 / shotsPerSec))
        {
            //Instantiate the gun's flame
            ParticleSystem instantiatedFlame = Instantiate(flame, gunEnd.position, transform.rotation, this.transform) as ParticleSystem;
            instantiatedFlame.Play();

            //calculate recoil
            recoil += 1f;
            if (recoil < Mathf.Abs(maxRecoil_x))
                recoil = Mathf.Abs(maxRecoil_x);

            //Play shooting sound
            GameObject.FindWithTag("GunSound").GetComponent<AudioSource>().Play();

            //The point where the shot is aimed at (direct line from the center of the reticle)
            Vector3 shotOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

            //Check if the shot hit something or not, ignoring trigger colliders
            if (Physics.Raycast(shotOrigin, fpsCam.transform.forward, out RaycastHit hit, range, 1, QueryTriggerInteraction.Ignore))
            {
                //Hurt the hit ennemy if any
                Collider collider = hit.collider;
                if (collider.gameObject.tag == "Ennemy")
                    collider.GetComponent<LifeAndDeath>().hurt(this.power);
            }

            lastfired = Time.time;
        }

        //apply recoil
        recoiling();
    }

    /// <summary>
    /// Applys the weapon's recoil
    /// </summary>
    private void recoiling()
    {
        if (Time.timeScale != 0 && recoil > 0f)
        {
            float xDiff = recoilSpeed ;
            weapon.transform.localEulerAngles -= new Vector3(xDiff, 0, 0);

            recoil -= 1f;
        }
    }
}
