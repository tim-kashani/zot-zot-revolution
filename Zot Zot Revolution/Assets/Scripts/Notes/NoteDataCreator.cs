using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class NoteDataCreator : MonoBehaviour
{
    [SerializeField] string fileName;

    [SerializeField] Vector4[] vectors;

    string path;

    MidiFile midiFile;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/MidiFiles/";

        midiFile = ReadFile(fileName);

        ConvertMidiToVectors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ConvertMidiToVectors()
    {
        ICollection<Melanchall.DryWetMidi.Interaction.Note> notes = midiFile.GetNotes();

        foreach (Melanchall.DryWetMidi.Interaction.Note note in notes)
        {
            Debug.Log("Note at " + note.Time + " time and " + note.NoteNumber + " number and " + note.Length + " length");
        }
    }

    MidiFile ReadFile(string s)
    {
        return MidiFile.Read(path + s);
    }
}
