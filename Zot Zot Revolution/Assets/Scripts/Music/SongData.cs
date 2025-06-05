using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SongData : ScriptableObject
{
    public string songName;

    public List<Vector4> notes;

    public string composerName;

    public string characterArtistName;

    public string difficulty = "Easy";

    public AudioClip song;

    public float volume = 1;

    public float initialDelay;

    public string midiFilePath;

    public Sprite characterSprite;

    public Texture2D characterTexture;

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

    public Color trackBarColor = Color.white;

    public Color noteColor = Color.white;

    public Color noteBarColor = Color.gray;

    public Color noteIndicatorColor = Color.gray;

    public Color light1Color = Color.white;

    public Color light2Color = Color.white;

    public Color gradient1Color = Color.gray;

    public Color gradient2Color = Color.white;
}
