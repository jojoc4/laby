using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Manages ambiant music during main game
/// </summary>
public class Music : MonoBehaviour
{

    public AudioClip[] musics;          //normal audio collection
    private AudioSource audioSource;    //audio source
    public AudioClip level2;            //special audio for level 2
    private int level = 1;              //level number

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playNewMusic();
    }

    // Update is called once per frame
    void Update()
    {
        //play new music if current is finished
        if (!audioSource.isPlaying)
        {
            playNewMusic();
        }
    }

    /// <summary>
    /// change level
    /// </summary>
    /// <param name="nbr">level number</param>
    public void changeLevel(int nbr)
    {
        level = nbr;
        playNewMusic();
    }

    /// <summary>
    /// choose next music
    /// </summary>
    private void playNewMusic()
    {
        switch (level)
        {
            case 1:
                //choose a random music and play it
                int randClip = Random.Range(0, musics.Length);
                audioSource.clip = musics[randClip];
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = level2;
                audioSource.volume = 0.5f;
                audioSource.Play();
                break;
        }
    }
}
