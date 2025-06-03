using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] SongData[] songDatas;

    [SerializeField] LevelSelectButton levelSelectButton;

    [SerializeField] TMP_Text levelNameText, dialogueText, difficultyText, scoreText, missesText, gradeText;

    [SerializeField] RectTransform levelButtonParent, levelPanel;

    [SerializeField] Image characterImage;

    [SerializeField] Animator fadeAnimator;

    [SerializeField] AudioSource music;

    int currentSongIndex;

    float currentRotation, lerpedRotation;

    SongData currentSongData;

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = 1;

        SpawnLevelButtons();

        SelectLevel(songDatas[0]);
    }

    // Update is called once per frame
    void Update()
    {
        lerpedRotation = Mathf.Lerp(lerpedRotation, currentRotation, Time.deltaTime * 10);

        levelButtonParent.localRotation = Quaternion.Euler(0, 0, lerpedRotation);
    }

    public void SelectLevel(SongData songData)
    {
        if (currentSongData == songData)
        {
            return;
        }

        StopCoroutine(SwitchLevelPanel(currentSongData));

        currentSongData = songData;

        SetLevelNameText(songData.songName, songData.composerName);

        currentSongIndex = GetSongDataIndex(songData);

        currentRotation = (currentSongIndex * -360 / songDatas.Length) + 90;

        if (currentRotation > 360)
        {
            currentRotation -= 360;
        }

        StartCoroutine(SwitchLevelPanel(currentSongData));

        difficultyText.text = "Difficulty: " + songData.difficulty;

        FileManager.LevelSave save = FileManager.GetLevelSave(songData.songName);

        scoreText.text = "Score: " + save.score;

        missesText.text = "Misses: " + save.misses;

        gradeText.text = save.grade.ToString();
    }

    public void StartButton()
    {
        fadeAnimator.Play("Out");

        StartCoroutine(LoadLevel(currentSongData));

        StartCoroutine(FadeMusic());
    }

    public void BackButton()
    {
        fadeAnimator.Play("Out");

        StartCoroutine(Back());

        StartCoroutine(FadeMusic());
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

    IEnumerator SwitchLevelPanel(SongData songData)
    {
        float f = 0;

        while (f < 1)
        {
            f += Time.deltaTime * 5;

            if (f > 1)
            {
                f = 1;
            }

            float pow = Mathf.Pow(f, 0.75f);

            levelPanel.anchoredPosition = new(0, pow * -750);

            yield return new WaitForEndOfFrame();
        }

        characterImage.sprite = songData.characterSprite;

        dialogueText.text = songData.unbeatenLevelSelectDialogue;

        while (f > 0)
        {
            f -= Time.deltaTime * 5;

            if (f < 0)
            {
                f = 0;
            }

            float pow = Mathf.Pow(f, 1.5f);

            levelPanel.anchoredPosition = new(0, pow * -750);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator LoadLevel(SongData songData)
    {
        GameStateManager.SetSongData(songData);

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Game");
    }

    IEnumerator Back()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Title Screen");
    }

    IEnumerator FadeMusic()
    {
        float f = 1;

        while (f > 0)
        {
            f -= Time.deltaTime / 2;

            if (f < 0)
            {
                f = 0;
            }

            AudioListener.volume = f;

            yield return new WaitForEndOfFrame();
        }
    }
}
