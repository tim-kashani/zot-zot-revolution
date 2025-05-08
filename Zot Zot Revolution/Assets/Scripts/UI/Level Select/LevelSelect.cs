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

    int currrentSongIndex;

    float currentRotation, lerpedRotation;

    // Start is called before the first frame update
    void Start()
    {
        SpawnLevelButtons();
    }

    // Update is called once per frame
    void Update()
    {
        lerpedRotation = Mathf.Lerp(lerpedRotation, currentRotation, Time.deltaTime * 10);

        levelButtonParent.localRotation = Quaternion.Euler(0, 0, lerpedRotation);
    }

    public void SelectLevel(SongData songData)
    {
        SetLevelNameText(songData.songName, songData.composerName);

        currrentSongIndex = GetSongDataIndex(songData);

        currentRotation = (currrentSongIndex * 360 / songDatas.Length) + 90;
    }

    void SetLevelNameText(string levelName, string composerName)
    {
        // TODO add rich text
        levelNameText.text = levelName + " - " + composerName;
    }

    void SpawnLevelButtons()
    {
        for (int i = 0; i < songDatas.Length; i++)
        {
            LevelSelectButton button = Instantiate(levelSelectButton, levelButtonParent);

            button.transform.localRotation = Quaternion.Euler(0, 0, (i * 360 / songDatas.Length) + 90);

            button.SetSongData(songDatas[i]);
        }
    }

    int GetSongDataIndex(SongData songData)
    {
        for (int i = 0; i < songDatas.Length; i++)
        {
            if (songDatas[i] == songData)
            {
                return i;
            }
        }

        return -1;
    }
}
