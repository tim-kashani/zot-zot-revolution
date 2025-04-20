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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
