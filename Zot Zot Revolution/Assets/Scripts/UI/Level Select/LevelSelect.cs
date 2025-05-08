using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] SongData[] songDatas;

    [SerializeField] LevelSelectButton levelSelectButton;

    [SerializeField] TMP_Text levelNameText;

    [SerializeField] RectTransform levelButtonParent;

    [SerializeField] Image characterImage;

    int currentSongIndex;

    float currentRotation, lerpedRotation;

    SongData currentSongData;

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
        currentSongData = songData;

        SetLevelNameText(songData.songName, songData.composerName);

        currentSongIndex = GetSongDataIndex(songData);

        currentRotation = (currentSongIndex * -360 / songDatas.Length) + 90;

        if (currentRotation > 360)
        {
            currentRotation -= 360;
        }

        characterImage.sprite = songData.characterSprite;
    }

    void SetLevelNameText(string levelName, string composerName)
    {
        // TODO add rich text
        levelNameText.text = levelName + "<size=75>" + " - " + composerName + "</size>";
    }

    void SpawnLevelButtons()
    {
        for (int i = 0; i < songDatas.Length; i++)
        {
            LevelSelectButton button = Instantiate(levelSelectButton, levelButtonParent);

            float rotation = (i * 360 / songDatas.Length) + 270;

            if (rotation > 360)
            {
                rotation -= 360;
            }

            button.transform.localRotation = Quaternion.Euler(0, 0, rotation);

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
