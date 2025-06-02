using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GradeScreen : MonoBehaviour
{
    [SerializeField] Image character;

    [SerializeField] TMP_Text scoreText, gradeText, gradeLetterText, dialogueText;

    [SerializeField] GameObject returnButton;

    [SerializeField] Animator fadeAnimator;

    SongData songData;

    NoteManager.LetterGrade grade;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        songData = GameStateManager.GetSongData();

        if (songData == null)
        {
            songData = new();
        }

        character.sprite = songData.characterSprite;

        grade = GameStateManager.GetGrade();

        score = GameStateManager.GetScore();

        StartCoroutine(Grade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnButton()
    {
        StartCoroutine(Fade());
    }

    string GetDialogue()
    {
        switch (grade)
        {
            case NoteManager.LetterGrade.S:

                return songData.gradeSDialogue;

            case NoteManager.LetterGrade.A:

                return songData.gradeADialogue;

            case NoteManager.LetterGrade.B:

                return songData.gradeBDialogue;

            case NoteManager.LetterGrade.C:

                return songData.gradeCDialogue;

            case NoteManager.LetterGrade.D:

                return songData.gradeDDialogue;

            case NoteManager.LetterGrade.F:

                return songData.gradeFDialogue;
        }

        return "Erm this dialogue is not supposed to happen";
    }

    IEnumerator Grade()
    {
        scoreText.gameObject.SetActive(false);

        gradeText.gameObject.SetActive(false);

        gradeLetterText.gameObject.SetActive(false);

        dialogueText.gameObject.SetActive(false);

        character.gameObject.SetActive(false);

        returnButton.SetActive(false);

        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(1.5f);

        scoreText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        float f = 0;

        while (f < 1)
        {
            f += Time.deltaTime / 3;

            if (f > 1)
            {
                f = 1;
            }

            int i = (int)Mathf.Lerp(0f, score, f);

            scoreText.text = "Score: " + i.ToString("00000");

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        gradeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        gradeLetterText.gameObject.SetActive(true);

        gradeLetterText.text = grade.ToString();

        yield return new WaitForSeconds(1);

        character.gameObject.SetActive(true);

        dialogueText.gameObject.SetActive(true);

        dialogueText.text = GetDialogue();

        yield return new WaitForSeconds(1);

        returnButton.SetActive(true);
    }

    IEnumerator Fade()
    {
        fadeAnimator.Play("Out");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Level Select");
    }
}
