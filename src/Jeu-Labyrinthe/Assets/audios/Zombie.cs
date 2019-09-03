using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manages the zombie sounds
/// </summary>
public class Zombie : MonoBehaviour
{

    public AudioClip[] roar;
    public AudioClip die;
    public AudioClip[] attack;
    public AudioClip pain;
    public AudioSource audioSource;

    private int mode = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            switch (mode)
            {
                case 1:
                    Roar();
                    break;
                case 2:
                    Attack();
                    break;
                case 3:
                    changemode(0);
                    Pain();
                    break;
                case 4:
                    changemode(0);
                    Die();
                    break;
            }
        }
    }

    public void changemode(int m)
    {
        mode = m;
    }

    //mode1
    private void Roar()
    {
        //choose a random music and play it
        int randClip = Random.Range(0, roar.Length);
        audioSource.clip = roar[randClip];
        audioSource.Play();
    }
    
    //mode2
    private void Attack()
    {
        //choose a random music and play it
        int randClip = Random.Range(0, attack.Length);
        audioSource.clip = attack[randClip];
        audioSource.Play();
    }

    //mode3
    private void Pain()
    {
        //choose a random music and play it
        audioSource.clip = pain;
        audioSource.Play();
    }


    //mode4
    private void Die()
    {
        //choose a random music and play it
        audioSource.clip = die;
        audioSource.Play();
    }
}
