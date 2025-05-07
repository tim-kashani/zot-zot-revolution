using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NoteManager : MonoBehaviour
{
    public enum HitTiming
    {
        PERFECT, GREAT, GOOD, OK, MISS
    }

    [Header("Debug")]

    // x is time
    // y is position
    // z is note type
    // w is note length for hold and spam notes

    [SerializeField] List<Vector4> testNotes;

    // this is serialized so it's easier to see that it's working in editor
    [SerializeField] List<Note> track1Notes, track2Notes, track3Notes, track4Notes, track5Notes, allNotes;

    [Header("Note Types")]
    [SerializeField] Note defaultNote;

    [SerializeField] Note spaceNote;

    [SerializeField] Note holdNote;

    [SerializeField] Note spamNote;

    [SerializeField] Note ghostNote;

    [SerializeField] Note cloudNote;

    [Header("UI")]
    [SerializeField] RectTransform noteParent;

    [SerializeField] TMP_Text scoreText;

    [SerializeField] TMP_Text maxScoreText;

    [SerializeField] RectTransform hitTimingDisplayParent;

    [SerializeField] HitTimingDisplay hitTimingDisplay;

    [Header("Sound Effects")]
    [SerializeField] AudioClip hitSFX;

    Music music;

    float score, maxScore;

    List<Vector4> noteList;

    int currentNoteIndex;

    bool spawned;

    AudioSource audioSource;

    CharacterBounce characterBounce;

    int currentBeatInt = -1;

    // Start is called before the first frame update
    void Start()
    {
        music = FindAnyObjectByType<Music>();

        // testing
        //StartCoroutine(SpawnNotes(testNotes));

        //StartCoroutine(StartMusicDelayed());

        audioSource = GetComponent<AudioSource>();

        characterBounce = FindAnyObjectByType<CharacterBounce>();
    }

    public void Spawn(List<Vector4> notes)
    {
        StartCoroutine(SpawnNotes(notes));
    }

    public IEnumerator SpawnNotes(List<Vector4> notes)
    {
        //yield return new WaitForEndOfFrame();

        track1Notes = new();

        track2Notes = new();

        track3Notes = new();

        track4Notes = new();

        track5Notes = new();

        allNotes = new();

        Note[] notesToDestroy = FindObjectsByType<Note>(FindObjectsSortMode.None);

        // destroys old notes in case some were in the scene before

        foreach (Note noteToDestroy in notesToDestroy)
        {
            Destroy(noteToDestroy.gameObject);
        }

        // this should sort all notes by time
        notes.Sort((a, b) => a.x.CompareTo(b.x));

        noteList = notes;

        /*for (int i = 0; i < notes.Count; i++)
        {
            int noteType = (int)notes[i].z;

            Note noteToSpawn = noteType switch
            {
                1 => defaultNote,
                2 => spaceNote,
                3 => holdNote,
                4 => spamNote,
                5 => ghostNote,
                6 => cloudNote,
                _ => defaultNote
            };

            Note note = Instantiate(noteToSpawn, noteParent);

            int noteXPosition = (int)notes[i].y;

            if (noteType == 2)
            {
                noteXPosition = 5;
            }

            note.SetXPositionAndTime(noteXPosition, notes[i].x);

            note.SetNoteLength(notes[i].w);

            List<Note> listToAdd = GetTrackList(noteXPosition);

            listToAdd.Add(note);

            allNotes.Add(note);

            yield return new WaitForEndOfFrame();
        }*/

        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.1f);

        spawned = true;

        music.StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            return;
        }

        if (currentBeatInt != (int)music.GetCurrentBeat())
        {
            currentBeatInt = (int)music.GetCurrentBeat();

            characterBounce.BeatBounce();
        }

        noteParent.anchoredPosition = new(0, music.GetCurrentBeat() * Note.ySpacing * -1);

        if (currentNoteIndex >= noteList.Count)
        {
            return;
        }

        if (noteList[currentNoteIndex].x <= (music.GetCurrentBeat() + 10))
        {
            SpawnNote(currentNoteIndex);

            currentNoteIndex++;
        }
    }

    void SpawnNote(int i)
    {
        int noteType = (int)noteList[i].z;

        Note noteToSpawn = noteType switch
        {
            1 => defaultNote,
            2 => spaceNote,
            3 => holdNote,
            4 => spamNote,
            5 => ghostNote,
            6 => cloudNote,
            _ => defaultNote
        };

        Note note = Instantiate(noteToSpawn, noteParent);

        int noteXPosition = (int)noteList[i].y;

        if (noteType == 2)
        {
            noteXPosition = 5;
        }

        note.SetXPositionAndTime(noteXPosition, noteList[i].x);

        note.SetNoteLength(noteList[i].w);

        List<Note> listToAdd = GetTrackList(noteXPosition);

        listToAdd.Add(note);

        allNotes.Add(note);
    }

    public void OnNote1(InputValue v)
    {
        SendNoteInput(1, v.isPressed);
    }

    public void OnNote2(InputValue v)
    {
        SendNoteInput(2, v.isPressed);
    }

    public void OnNote3(InputValue v)
    {
        SendNoteInput(3, v.isPressed);
    }

    public void OnNote4(InputValue v)
    {
        SendNoteInput(4, v.isPressed);
    }

    public void OnSpaceNote(InputValue v)
    {
        SendNoteInput(5, v.isPressed);
    }

    void SendNoteInput(int i, bool isPressed)
    {
        if (isPressed)
        {
            PressNote(i);
        } else
        {
            ReleaseNote(i);
        }
    }

    public void PressNote(int i)
    {
        Debug.Log("Pressed note " + i);

        List<Note> trackList = GetTrackList(i);

        if (trackList.Count < 1)
        {
            return;
        }

        Note note = trackList[0];

        if (CalculateOffset(note.GetPressTime()) < -0.5f)
        {
            return;
        }

        note.OnPress();
    }

    public void ReleaseNote(int i)
    {
        Debug.Log("Released note " + i);

        List<Note> trackList = GetTrackList(i);

        if (trackList.Count < 1)
        {
            return;
        }

        Note note = trackList[0];

        note.OnRelease();
    }

    public void AddScore(float f)
    {
        score += f;

        UpdateScoreText();
    }

    public void AddMaxScore(float f)
    {
        maxScore += f;

        UpdateMaxScoreText();
    }

    public void RemoveNote(Note note, int position)
    {
        List<Note> listToRemove = GetTrackList(position);

        if (listToRemove.Contains(note))
        {
            listToRemove.Remove(note);
        }
    }

    public void HitNote()
    {
        audioSource.PlayOneShot(hitSFX);

        characterBounce.NoteBounce();
    }

    public void DisplayTiming(float multiplier)
    {
        HitTiming hitTiming = HitTiming.PERFECT;

        if (multiplier < 1)
        {
            hitTiming = HitTiming.GREAT;

            if (multiplier < 0.95f)
            {
                hitTiming = HitTiming.GOOD;

                if (multiplier < 0.9f)
                {
                    hitTiming = HitTiming.OK;
                }
            }
        }

        SpawnTimingDisplay(hitTiming);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString("00000");
    }

    void UpdateMaxScoreText()
    {
        maxScoreText.text = "Max Score (Testing): " + maxScore.ToString("00000");
    }

    void SpawnTimingDisplay(HitTiming hitTiming)
    {
        HitTimingDisplay display = Instantiate(hitTimingDisplay, hitTimingDisplayParent);

        display.SetHitTiming(hitTiming);
    }

    public float CalculateOffset(float beat)
    {
        float offset = music.GetCurrentBeat() - beat;

        float seconds = offset * 60 / music.GetBPM();

        Debug.Log("Offset of " + music.GetCurrentBeat() + " - " + beat + " = " + offset + ",  seconds = " + seconds);

        return seconds;
    }

    List<Note> GetTrackList(int i)
    {
        List<Note> list = i switch
        {
            1 => track1Notes,
            2 => track2Notes,
            3 => track3Notes,
            4 => track4Notes,
            5 => track5Notes,
            _ => track1Notes
        };

        return list;
    }

    IEnumerator StartMusicDelayed()
    {
        // this delays the music start by a second so the scene can load before the song starts

        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1);

        music.StartMusic();
    }
}
