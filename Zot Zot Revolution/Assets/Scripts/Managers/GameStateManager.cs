using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager gameStateManager;

    static SongData songData;

    // Start is called before the first frame update
    void Start()
    {
        if (gameStateManager == null)
        {
            gameStateManager = this;

            transform.parent = null;

            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public static void SetSongData(SongData data)
    {
        songData = data;
    }

    public static SongData GetSongData()
    {
        return songData;
    }
}
