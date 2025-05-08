using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] RectTransform rotate;

    [SerializeField] TMP_Text levelNameText;

    SongData songData;

    LevelSelect levelSelect;

    // Start is called before the first frame update
    void Start()
    {
        levelSelect = FindAnyObjectByType<LevelSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        rotate.rotation = Quaternion.identity;
    }

    public void SetSongData(SongData data)
    {
        songData = data;

        levelNameText.text = songData.songName;
    }

    public void SelectLevel()
    {
        levelSelect.SelectLevel(songData);
    }
}
