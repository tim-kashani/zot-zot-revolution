using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
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

    IEnumerator StartMusicDelayed()
    {
        // this delays the music start by a second so the scene can load before the song starts

        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1);

        music.StartMusic();
    }
}
