using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] Vector4 testNotes;

    Music music;

    // Start is called before the first frame update
    void Start()
    {
        music = FindAnyObjectByType<Music>();

        StartCoroutine(StartMusicDelayed());
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
