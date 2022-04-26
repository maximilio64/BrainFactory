using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTicker : MonoBehaviour, Tick
{
    public bool active = false;

    AudioSource audioSource;
    public AudioClip tick1;
    public AudioClip tick2;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void tick(int everyOther)
    {
        if (active)
        {
            if (everyOther != 1)
            {
                audioSource.clip = tick1;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = tick2;
                audioSource.Play();
            }
        }
    }
}
