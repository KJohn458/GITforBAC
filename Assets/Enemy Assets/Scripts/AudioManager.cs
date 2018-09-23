using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioClip[] sfxClips;
    public int maxSFXSources = 10;
    private AudioSource[] sfxPool;
    private int currentSFX;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        sfxPool = new AudioSource[maxSFXSources];
        for (int i = 0; i < maxSFXSources; i++)
        {
            GameObject g = new GameObject("sfx" + i);
            AudioSource sfx = g.AddComponent<AudioSource>();
            sfx.playOnAwake = false;
            sfxPool[i] = sfx;
        }
    }

    public void  PlaySFX (string clipName)
    {
        for (int i = 0; i < sfxClips.Length; i++)
        {
            if (clipName == sfxClips[i].name)
            {
                AudioSource sfx = sfxPool[currentSFX];
                sfx.clip = sfxClips[i]; 
                sfx.Play();
                currentSFX++;
                currentSFX %= maxSFXSources;
                break;
            }
        }
    }
}
