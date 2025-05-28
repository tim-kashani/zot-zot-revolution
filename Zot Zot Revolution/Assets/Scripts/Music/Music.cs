using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] float bpm = 120;

    [SerializeField] SongData testSongData;

    [SerializeField] Image[] trackImages, noteIndicatorImages, trackBarImages;

    [SerializeField] MeshRenderer character;

    AudioSource audioSource;

    NoteDataCreator noteDataCreator;

    SongData songData;

    bool isFinished;

    NoteManager noteManager;

    float time;

    bool isDelayed;

    // Start is called before the first frame update
    void Start()
    {
        songData = GameStateManager.GetSongData();

        if (songData == null)
        {
            songData = testSongData;
        }

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

        noteManager = FindAnyObjectByType<NoteManager>();

        character.material.SetTexture("_Texture2D", songData.characterTexture);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDelayed)
        {
            // when the song loops
            if (audioSource.time < time)
            {
                isDelayed = false;
            }

            time = audioSource.time;

            audioSource.volume = songData.volume;

            return;
        }

        time = audioSource.time;

        if ((audioSource.time + 2) >= audioSource.clip.length && !isFinished)
        {
            isFinished = true;

            FinishLevel();
        }
    }

    public void StartMusic()
    {
        float delay = songData.initialDelay;

        if (delay > 0)
        {
            isDelayed = true;

            Debug.Log("Delay: " + delay);

            audioSource.volume = 0;
        } else
        {
            audioSource.volume = songData.volume;
        }

        audioSource.Play();

        time = audioSource.clip.length - (delay * bpm / 60);

        Debug.Log("Delay Time: " + time);

        audioSource.time = time;
    }

    public void FinishLevel()
    {
        Debug.Log("Finished level with a grade of " + noteManager.GetLetterGrade());
    }

    public float GetBPM()
    {
        return bpm;
    }

    public float GetCurrentBeat()
    {
        float beat = audioSource.time * bpm / 60;

        float delay = isDelayed ? (songData.initialDelay * bpm / 60) : 0;

        if (isDelayed)
        {
            return -delay;
        } else
        {
            return beat;
        }
    }

    public SongData GetSongData()
    {
        return songData;
    }
}
