using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SongData : ScriptableObject
{
    public string songName;

    public string composerName;

    public string characterArtistName;

    public AudioClip song;

    public string midiFilePath;

    public Sprite characterSprite;

    public float bpm;

    public string characterName;

    public string unbeatenLevelSelectDialogue;

    public string beatenLevelSelectDialogue;

    public string gradeSDialogue;

    public string gradeADialogue;

    public string gradeBDialogue;

    public string gradeCDialogue;

    public string gradeDDialogue;

    public string gradeFDialogue;
}
