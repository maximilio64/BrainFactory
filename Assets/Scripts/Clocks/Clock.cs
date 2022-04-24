using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour, Tick
{
    public bool isOn;

    MeshRenderer[] meshRenderers;
    MeshCollider[] meshColliders;

    AudioSource audioSource;
    public AudioClip tick1;
    public AudioClip tick2;

    // Start is called before the first frame update
    void Start()
    {
        meshColliders = GetComponentsInChildren<MeshCollider>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        audioSource = GetComponent<AudioSource>();

        if (!isOn)
        {
            foreach (MeshCollider m in meshColliders)
            {
                m.enabled = false;
            }
            foreach (MeshRenderer m in meshRenderers)
            {
                m.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tick(int everyOther)
    {
        if (everyOther != 1)
        {
            audioSource.clip = tick1;
            audioSource.Play();
        } else
        {
            audioSource.clip = tick2;
            audioSource.Play();
            isOn = !isOn;
            if (isOn)
            {
                foreach (MeshCollider m in meshColliders)
                {
                    m.enabled = true;
                }
                foreach (MeshRenderer m in meshRenderers)
                {
                    m.enabled = true;
                }
            }
            else
            {
                foreach (MeshCollider m in meshColliders)
                {
                    m.enabled = false;
                }
                foreach (MeshRenderer m in meshRenderers)
                {
                    m.enabled = false;
                }
            }
        }
    }
}
