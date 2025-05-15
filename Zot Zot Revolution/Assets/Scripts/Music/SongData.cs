using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SongData : ScriptableObject
{
    public string songName;

    public string composerName;

    public string characterArtistName;

    public string difficulty = "Easy";

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

    public Color bgColor = Color.white;

    public Color trackColor = new(0.5f, 0.5f, 0.5f, 0.5f);

    public Color noteColor = Color.white;

    public Color noteIndicatorColor = Color.gray;
}
