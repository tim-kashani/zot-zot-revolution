using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NoteManager : MonoBehaviour
{
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

    Music music;

    float score;

    // Start is called before the first frame update
    void Start()
    {
        music = FindAnyObjectByType<Music>();

        // testing
        StartCoroutine(SpawnNotes(testNotes));

        //StartCoroutine(StartMusicDelayed());
    }

    public IEnumerator SpawnNotes(List<Vector4> notes)
    {
        yield return new WaitForEndOfFrame();

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

        for (int i = 0; i < notes.Count; i++)
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
        }

        yield return new WaitForSeconds(1);

        music.StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        noteParent.anchoredPosition = new(0, music.GetCurrentBeat() * Note.ySpacing * -1);
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

        Note note = GetTrackList(i)[0];

        note.OnPress();
    }

    public void ReleaseNote(int i)
    {
        Debug.Log("Released note " + i);

        Note note = GetTrackList(i)[0];

        note.OnRelease();
    }

    public void AddScore(float f)
    {
        score += f;

        UpdateScoreText();
    }

    public void RemoveNote(Note note, int position)
    {
        List<Note> listToRemove = GetTrackList(position);

        if (listToRemove.Contains(note))
        {
            listToRemove.Remove(note);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString("00000");
    }

    public float CalculateOffset(float beat)
    {
        float offset = music.GetCurrentBeat() - beat;

        return offset / music.GetBPM();
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
