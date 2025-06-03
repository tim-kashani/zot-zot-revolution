using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public class LevelSave
    {
        public string levelName;

        public int score;

        public int misses;

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

    public static void SaveLevel(LevelSave save)
    {
        // if saved score is higher, don't save the new score
        if (PlayerPrefs.GetInt(save.levelName + " Score") > save.score)
        {
            return;
        }

        PlayerPrefs.SetInt(save.levelName + " Score", save.score);

        PlayerPrefs.SetInt(save.levelName + " Misses", save.misses);

        PlayerPrefs.SetString(save.levelName + " Grade", save.grade.ToString());
    }

    public static LevelSave GetLevelSave(string levelName)
    {
        LevelSave save = new();

        save.levelName = levelName;

        save.score = PlayerPrefs.GetInt(levelName + " Score");

        save.misses = PlayerPrefs.GetInt(levelName + " Misses");

        save.grade = PlayerPrefs.GetString(levelName + " Grade")[0];

        return save;
    }
}
