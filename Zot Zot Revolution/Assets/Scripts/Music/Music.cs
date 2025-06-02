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

    [SerializeField] Light light1, light2;

    AudioSource audioSource;

    NoteDataCreator noteDataCreator;

    SongData songData;

    bool isFinished;

    NoteManager noteManager;

    float delayTimer;

    bool isDelayed;

    public float test;

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

        light1.color = songData.light1Color;

        light2.color = songData.light2Color;
    }

    // Update is called once per frame
    void Update()
    {
        test = GetCurrentBeat();

        if (isDelayed)
        {
            delayTimer -= Time.deltaTime;

            if (delayTimer <= 0)
            {
                isDelayed = false;

                PlayMusic();
            }

            return;
        }

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
        }

        audioSource.volume = songData.volume;

        delayTimer = (delay * 60 / bpm);

        Debug.Log("Delay Timer: " + delayTimer);

        if (delay <= 0)
        {
            PlayMusic();
        }
    }

    void PlayMusic()
    {
        audioSource.Play();
    }

    public void FinishLevel()
    {
        noteManager.Finish();
    }

    public float GetBPM()
    {
        return bpm;
    }

    public float GetCurrentBeat()
    {
        if (isDelayed)
        {
            return -delayTimer * bpm / 60;
        } else
        {
            return audioSource.time * bpm / 60;
        }
    }

    public SongData GetSongData()
    {
        return songData;
    }
}
