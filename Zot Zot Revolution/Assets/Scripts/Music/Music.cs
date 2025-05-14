using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] float bpm = 120;

    [SerializeField] SongData testSongData;

    AudioSource audioSource;

    NoteDataCreator noteDataCreator;

    SongData songData;

    // Start is called before the first frame update
    void Start()
    {
        songData = testSongData;

        audioSource = GetComponent<AudioSource>();

        bpm = songData.bpm;

        audioSource.clip = songData.song;

        noteDataCreator = FindAnyObjectByType<NoteDataCreator>();

        noteDataCreator.ReadMidiFile(Application.dataPath + "/" + songData.midiFilePath);
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
