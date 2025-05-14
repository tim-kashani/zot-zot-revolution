using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class NoteDataCreator : MonoBehaviour
{
    [SerializeField] string fileName;

    [SerializeField] List<Vector4> vectors;

    string path;

    MidiFile midiFile;

    // Start is called before the first frame update
    void Start()
    {
        /*path = Application.dataPath + "/MidiFiles/";

        midiFile = ReadFile(fileName);

        StartCoroutine(Wait());*/
    }

    public void ReadMidiFile(string filePath)
    {
        midiFile = ReadFile(filePath);

        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ConvertMidiToVectors()
    {
        ICollection<Melanchall.DryWetMidi.Interaction.Note> notes = midiFile.GetNotes();

        Debug.Log(notes.Count);

        List<Vector4> newVectors = new();

        foreach (Melanchall.DryWetMidi.Interaction.Note note in notes)
        {
            int noteNumber = note.NoteNumber - 35;

            int noteType = (noteNumber / 12) + 1;

            int noteTrack = noteNumber - ((noteType - 1) * 12);

            if (noteType > 1)
            {
                noteType++;
            }

            if (noteTrack == 5)
            {
                noteType = 2;
            }

            float noteTime = note.Time / 480f;

            float noteLength = note.Length / 480f;

            Debug.Log("Note at " + noteTime + " time and " + noteTrack + " number and " + noteType + " note type and " + noteLength / 480 + " length");

            Vector4 v = new(noteTime, noteTrack, noteType, noteLength);

            newVectors.Add(v);
        }

        vectors = newVectors;
    }

    MidiFile ReadFile(string s)
    {
        return MidiFile.Read(path + s);
    }

    IEnumerator Wait()
    {
        Debug.Log("Waiting");

        yield return new WaitForSeconds(1);

        ConvertMidiToVectors();

        yield return new WaitForSeconds(0.1f);

        NoteManager noteManager = FindAnyObjectByType<NoteManager>();

        noteManager.Spawn(vectors);
    }
}
