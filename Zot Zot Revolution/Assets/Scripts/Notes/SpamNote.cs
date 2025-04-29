using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamNote : Note
{
    // Hold Note Guidelines

    // Has to be spammed constantly by the player
    // If the player doesn't keep hiting the note in a window of time (around half a second), treat it as letting go of the note

    // indicator for how long the player should spam
    [SerializeField] RectTransform spamBar;

    bool isBeingPressed;

    float maxHoldPoints, currentHoldPoints, lastFrame;

    private void Update()
    {
        float delta = music.GetCurrentBeat() - lastFrame;

        lastFrame = music.GetCurrentBeat();

        if (!isBeingPressed)
        {
            return;
        }

        if (pressTime <= music.GetCurrentBeat())
        {
            float pointsDelta = delta * 25;

            if ((currentHoldPoints + pointsDelta) > maxHoldPoints)
            {
                pointsDelta = maxHoldPoints - currentHoldPoints;
            }

            if (pointsDelta < 0)
            {
                pointsDelta = 0;
            }

            currentHoldPoints += pointsDelta;

            noteManager.AddScore(pointsDelta);

            if (pressTime + noteLength <= music.GetCurrentBeat())
            {
                isBeingPressed = false;

                if (currentHoldPoints < maxHoldPoints)
                {
                    noteManager.AddScore(maxHoldPoints - currentHoldPoints);
                }

                RemoveNote();

                return;
            }
        }
    }

    public override void SetNoteLength(float f)
    {
        base.SetNoteLength(f);

        spamBar.sizeDelta = new(spamBar.sizeDelta.x, f * ySpacing);

        maxHoldPoints = noteLength * 50;
    }

    public override void OnPress()
    {
        noteManager.AddScore(CalculateScoreMultiplier(pressTime) * 100);

        isBeingPressed = true;

        StartCoroutine(FadeOutNote(true));
    }

    public override void OnRelease()
    {
        if (!isBeingPressed)
        {
            return;
        }

        base.OnRelease();

        isBeingPressed = false;

        RemoveNote();
    }
}
