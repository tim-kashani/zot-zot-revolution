using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpamNote : Note
{
    // Hold Note Guidelines

    // Has to be spammed constantly by the player
    // If the player doesn't keep hiting the note in a window of time (around half a second), treat it as letting go of the note

    // indicator for how long the player should spam
    [SerializeField] RectTransform spamBar, spamBarBG;

    bool isBeingPressed;

    float maxHoldPoints, currentHoldPoints, lastFrame, spamTimer, maxYSpacing;

    protected override void NoteStart()
    {
        base.NoteStart();

        spamBar.GetComponent<Image>().color = music.GetSongData().noteColor;

        spamBarBG.GetComponent<Image>().color = music.GetSongData().noteIndicatorColor;
    }

    private void Update()
    {
        float delta = music.GetCurrentBeat() - lastFrame;

        lastFrame = music.GetCurrentBeat();

        if (!isBeingPressed)
        {
            return;
        }

        if (isRemoved)
        {
            return;
        }

        spamTimer -= Time.deltaTime;

        if (spamTimer <= 0)
        {
            RemoveNote();

            return;
        }

        if (pressTime <= music.GetCurrentBeat())
        {
            float pointsDelta = delta * 50;

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

            spamBar.sizeDelta = new(spamBar.sizeDelta.x, maxYSpacing * currentHoldPoints / maxHoldPoints);

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

        maxYSpacing = f * ySpacing;

        spamBarBG.sizeDelta = new(spamBar.sizeDelta.x, maxYSpacing);

        spamBar.sizeDelta = new(spamBar.sizeDelta.x, 0);

        maxHoldPoints = noteLength * 50;

        noteManager.AddMaxScore(maxHoldPoints);
    }

    public override void OnPress()
    {
        spamTimer = 0.33f;

        if (!isBeingPressed)
        {
            noteManager.AddScore(CalculateScoreMultiplier(pressTime) * 100);

            isBeingPressed = true;

            StartCoroutine(FadeOutNote());
        } else
        {
            noteManager.HitNote();
        }
    }
}
