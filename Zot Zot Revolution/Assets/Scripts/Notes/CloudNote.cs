using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // needed for fading

public class CloudtNote : Note
{
    // Cloud Note Guidelines

    // Cloud image should cover the note and fade out to reveal the note soon before the judgement zone
    // Opacity is half in the prefab just so you can see the note while testing, but it should be fully opaque until close to judgement zone

    // music.GetCurrentBeat() is your friend

    [SerializeField] Image cloudImage;

    [SerializeField] RectTransform noteOffset;

    public override void SetXPositionAndTime(int x, float y)
    {
        base.SetXPositionAndTime(x, y);

        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new(0, y * GetYSpacing());

        noteOffset.anchoredPosition = new(CalculateXPosition(x), 0);
    }

    protected override void NoteStart()
    {
        base.NoteStart();

        cloudImage.color = music.GetSongData().noteColor;
    }

    protected override void NoteUpdate()
    {
        if (tooLate && !isRemoved)
        {
            Debug.Log("Too Late");

            fadedOut = true;

            RemoveNote();

            return;
        }

        float beatsToJudgement = pressTime - music.GetCurrentBeat();

        if (beatsToJudgement > 6)
        {
            return;
        }

        float alpha = Mathf.Clamp01((beatsToJudgement - 1) / 3);

        alpha = Mathf.Pow(alpha, 0.5f);

        SetImageAlpha(alpha);
    }

    void SetImageAlpha(float f)
    {
        Debug.Log("Alpha: " + f);

        cloudImage.color = new(cloudImage.color.r, cloudImage.color.g, cloudImage.color.b, f);
    }
}
