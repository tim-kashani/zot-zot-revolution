using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] SongData[] songDatas;

    [SerializeField] LevelSelectButton levelSelectButton;

    [SerializeField] TMP_Text levelNameText;

    [SerializeField] RectTransform levelButtonParent;

    // Start is called before the first frame update
    void Start()
    {
        SpawnLevelButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(SongData songData)
    {
        SetLevelNameText(songData.songName, songData.composerName);
    }

    void SetLevelNameText(string levelName, string composerName)
    {
        // TODO add rich text
        levelNameText.text = levelName + " - " + composerName;
    }

    void SpawnLevelButtons()
    {

    }
}
