using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    // this is for spacing notes equally from 1-4, can be changed but is static so it stays the same across notes
    public static float xSpacing = 200, ySpacing = 250;

    public int notePosition;

    [SerializeField] protected float scoreMultiplier = 1, fadeScaleMultiplier = 1;

    [SerializeField] protected Image noteImage;

    // press time is when the note should be pressed, note length is for hold and spam notes
    protected float pressTime, noteLength;

    protected NoteManager noteManager;

    protected Music music;

    protected bool fadedOut, isRemoved;

    private void Start()
    {
        noteManager = FindAnyObjectByType<NoteManager>();

        music = FindAnyObjectByType<Music>();

        noteImage.color = music.GetSongData().noteColor;

        NoteStart();
    }

    protected virtual void NoteStart()
    {

    }

    private void Update()
    {
        if (noteManager.CalculateOffset(pressTime) > 0.5f && !isRemoved)
        {
            Debug.Log("Too Late");

            fadedOut = true;

            RemoveNote();
        }
    }

    // this is called when the player presses the note
    public virtual void OnPress()
    {
        if (noteManager == null)
        {
            noteManager = FindAnyObjectByType<NoteManager>();
        }

        noteManager.AddScore(CalculateScoreMultiplier(pressTime) * 100);

        RemoveNote();
    }

    // this is called when the player releases the note
    public virtual void OnRelease()
    {

    }

    // this is when a note should not be pressable, either after pressing it or having it pass the judgement zone
    public virtual void RemoveNote()
    {
        if (noteManager == null)
        {
            noteManager = FindAnyObjectByType<NoteManager>();
        }

        noteManager.RemoveNote(this, notePosition);

        if (!fadedOut)
        {
            StartCoroutine(FadeOutNote());
        }

        StartCoroutine(WaitToDestroy());

        isRemoved = true;
    }

    public float CalculateScoreMultiplier(float f)
    {
        if (noteManager == null)
        {
            noteManager = FindAnyObjectByType<NoteManager>();
        }

        float offset = noteManager.CalculateOffset(f);

        float abs = Mathf.Abs(offset);

        float subtraction = abs - 0.05f;

        if (subtraction < 0)
        {
            subtraction = 0;
        }

        if (subtraction > 1)
        {
            subtraction = 1;
        }

        float multiplier = 1 - subtraction;

        float score = scoreMultiplier * multiplier * multiplier;

        noteManager.DisplayTiming(multiplier);

        return score;
    }

    public virtual void SetXPositionAndTime(int x, float y)
    {
        notePosition = x;

        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new(CalculateXPosition(x), y * ySpacing);

        pressTime = y;
    }

    public virtual void SetNoteLength(float f)
    {
        noteLength = f;

        if (noteManager == null)
        {
            noteManager = FindAnyObjectByType<NoteManager>();
        }

        noteManager.AddMaxScore(100);
    }

    public float GetPressTime()
    {
        return pressTime;
    }

    protected float CalculateXPosition(int i)
    {
        if (i > 4)
        {
            return 0;
        }

        float x = ((i - 1) * xSpacing) - (xSpacing * 1.5f);

        return x;
    }

    protected IEnumerator FadeOutNote()
    {
        fadedOut = true;

        float f = 1;

        Vector3 position = noteImage.rectTransform.position;

        noteImage.transform.parent = FindAnyObjectByType<Canvas>().transform;

        float scale = noteImage.transform.localScale.x * 1.25f;

        noteManager.HitNote();

        while (f > 0)
        {
            f -= Time.deltaTime * 2;

            if (f < 0)
            {
                f = 0;
            }

            noteImage.color = new(noteImage.color.r, noteImage.color.g, noteImage.color.b, f);

            noteImage.rectTransform.position = position;

            noteImage.transform.localScale = (1 + ((1 - f) * fadeScaleMultiplier)) * scale * Vector3.one;

            yield return new WaitForEndOfFrame();
        }
    }

    protected IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
