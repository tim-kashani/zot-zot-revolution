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

    public Texture2D characterTexture;
}
