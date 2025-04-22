using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteManager : MonoBehaviour
{
    [Header("Debug")]

    // x is time
    // y is position
    // z is note type
    // w is additional data (hold time, etc.)

    [SerializeField] List<Vector4> testNotes;

    [Header("Note Types")]
    [SerializeField] Note defaultNote;

    [SerializeField] Note spaceNote;

    [SerializeField] Note holdNote;

    [SerializeField] Note spamNote;

    [SerializeField] Note ghostNote;

    [SerializeField] Note cloudNote;

    [Header("UI")]
    [SerializeField] RectTransform noteParent;

    Music music;

    // Start is called before the first frame update
    void Start()
    {
        music = FindAnyObjectByType<Music>();

        // testing
        SpawnNotes(testNotes);

        StartCoroutine(StartMusicDelayed());
    }

    public void SpawnNotes(List<Vector4> notes)
    {
        for (int i = 0; i < notes.Count; i++)
        {
            int noteType = (int)notes[i].z;

            Note noteToSpawn = noteType switch
            {
                0 => defaultNote,
                1 => spaceNote,
                2 => holdNote,
                3 => spamNote,
                4 => ghostNote,
                5 => cloudNote,
                _ => defaultNote
            };

            Note note = Instantiate(noteToSpawn, noteParent);

            note.SetXPosition((int)notes[i].y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void ReleaseNote(int i)
    {
        Debug.Log("Released note " + i);
    }

    IEnumerator StartMusicDelayed()
    {
        // this delays the music start by a second so the scene can load before the song starts

        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1);

        music.StartMusic();
    }
}
