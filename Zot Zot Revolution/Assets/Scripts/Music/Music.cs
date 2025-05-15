using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] float bpm = 120;

    [SerializeField] SongData testSongData;

    [SerializeField] Image[] trackImages, noteIndicatorImages, trackBarImages;

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

        foreach (Image trackImage in trackImages)
        {
            trackImage.color = songData.trackColor;
        }

        foreach (Image noteIndicatorImage in noteIndicatorImages)
        {
            noteIndicatorImage.color = songData.noteIndicatorColor;
        }

        foreach (Image trackBarImage in trackBarImages)
        {
            trackBarImage.color = songData.trackBarColor;
        }

        Camera.main.backgroundColor = songData.bgColor;
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

    public SongData GetSongData()
    {
        return songData;
    }
}
