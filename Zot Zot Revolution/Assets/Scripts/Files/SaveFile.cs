using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
    public struct Level
    {
        string levelName;

        int score;

        char grade;
    }

    public List<Level> levels;
}
