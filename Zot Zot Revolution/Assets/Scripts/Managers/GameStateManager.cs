using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager gameStateManager;

    static SongData songData;

    static int score;

    static NoteManager.LetterGrade grade;

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

    public static void SetScore(int i)
    {
        score = i;
    }

    public static void SetGrade(NoteManager.LetterGrade letterGrade)
    {
        grade = letterGrade;
    }

    public static int GetScore()
    {
        return score;
    }

    public static NoteManager.LetterGrade GetGrade()
    {
        return grade;
    }

    public static SongData GetSongData()
    {
        return songData;
    }
}
