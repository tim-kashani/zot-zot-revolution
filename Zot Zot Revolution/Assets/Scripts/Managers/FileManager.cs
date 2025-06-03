using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public class LevelSave
    {
        public string levelName;

        public int score;

        public char grade;
    }

    public static FileManager fileManager;

    // Start is called before the first frame update
    void Start()
    {
        if (fileManager == null)
        {
            transform.parent = null;

            fileManager = this;

            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public static LevelSave GetLevelSave(string levelName)
    {
        LevelSave save = new();

        save.levelName = levelName;

        save.score = PlayerPrefs.GetInt(levelName + " Score");

        save.grade = PlayerPrefs.GetString(levelName + " Grade")[0];

        return save;
    }
}
