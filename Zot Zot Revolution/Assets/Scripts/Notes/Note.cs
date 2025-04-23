using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    // this is for spacing notes equally from 1-4, can be changed but is static so it stays the same across notes
    public static float xSpacing = 200, ySpacing = 150;

    public int notePosition;

    [SerializeField] float scoreMultiplier = 1;

    // press time is when the note should be pressed, note length is for hold and spam notes
    float pressTime, noteLength;

    NoteManager noteManager;

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
    public void RemoveNote()
    {

    }

    public float CalculateScoreMultiplier(float f)
    {
        if (noteManager == null)
        {
            noteManager = FindAnyObjectByType<NoteManager>();
        }

        float offset = noteManager.CalculateOffset(f);

        float abs = Mathf.Abs(offset);

        float subtraction = abs - 0.1f;

        float score = scoreMultiplier * (1 - subtraction);

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
}
