using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Music : MonoBehaviour
{
    [SerializeField] float bpm = 120;

    [SerializeField] SongData testSongData;

    [SerializeField] Image[] trackImages, noteIndicatorImages, trackBarImages;

    [SerializeField] MeshRenderer character, sign;

    [SerializeField] Light light1, light2;

    [SerializeField] LineRenderer visualizer;

    [SerializeField] TMP_Text[] inputIndicators;

    AudioSource audioSource;

    NoteDataCreator noteDataCreator;

    SongData songData;

    bool isFinished;

    NoteManager noteManager;

    float delayTimer;

    bool isDelayed, paused;

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

        sign.material.SetColor("_Color_1", songData.gradient1Color);

        sign.material.SetColor("_Color_2", songData.gradient2Color);

        visualizer.material.SetColor("_Color_1", songData.gradient1Color);

        visualizer.material.SetColor("_Color_2", songData.gradient2Color);

        StartCoroutine(FadeIndicators());
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

        if ((audioSource.time + 0.01f) >= audioSource.clip.length && !isFinished)
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

    public void Pause()
    {
        paused = true;

        audioSource.Pause();
    }

    public void Unpause()
    {
        paused = false;

        audioSource.UnPause();
    }

    public bool IsPaused()
    {
        return paused;
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

    IEnumerator FadeIndicators()
    {
        yield return new WaitForEndOfFrame();

        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1);

        float f = 1;

        while (f > 0)
        {
            f -= Time.deltaTime / 3;

            if (f < 0)
            {
                f = 0;
            }

            for (int i = 0; i < inputIndicators.Length; i++)
            {
                inputIndicators[i].color = new(1, 1, 1, f);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
