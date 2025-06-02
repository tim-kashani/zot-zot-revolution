using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeScreen : MonoBehaviour
{
    SongData songData;

    // Start is called before the first frame update
    void Start()
    {
        songData = GameStateManager.GetSongData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
