using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class NoteDataCreator : MonoBehaviour
{
    [SerializeField] string fileName;

    string path;

    MidiFile midiFile;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/MidiFiles/";

        midiFile = ReadFile(fileName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    MidiFile ReadFile(string s)
    {
        return MidiFile.Read(path + s);
    }
}
