using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] float bpm;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMusic()
    {
        audioSource.Play();
    }

    public float GetBPM()
    {
        return bpm;
    }

    public float GetCurrentBeat()
    {
        return audioSource.time * bpm / 60;
    }
}
