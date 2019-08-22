using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public AudioClip[] musics;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playNewMusic();
    }


    // Start is called before the first frame update
    void Start()
    {
        
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

    private void playNewMusic()
    {
        //choose a random music and play it
        int randClip = Random.Range(0, musics.Length);
        audioSource.clip = musics[randClip];
        audioSource.Play();
    }
}
