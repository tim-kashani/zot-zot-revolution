using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : Note
{
    // Hold Note Guidelines

    // Has to be pressed initially like a default note
    // Has to be tracked as being held and give you points for holding
    // If let go, stop giving points and can't re-press the note

    // music.GetCurrentBeat() and noteLength is your friend

    // indicator for how long the player should hold
    [SerializeField] RectTransform holdBar;

    bool isBeingPressed;

    float maxHoldPoints, currentHoldPoints, lastFrame;

    private void Update()
    {
        float delta = music.GetCurrentBeat() - lastFrame;

        lastFrame = music.GetCurrentBeat();

        if (!isBeingPressed)
        {
            if (noteManager.CalculateOffset(pressTime) > 0.5f && !isRemoved)
            {
                Debug.Log("Too Late");

                fadedOut = true;

                RemoveNote();
            }

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

        holdBar.sizeDelta = new(holdBar.sizeDelta.x, f * ySpacing);

        maxHoldPoints = noteLength * 50;
    }

    public override void OnPress()
    {
        noteManager.AddScore(CalculateScoreMultiplier(pressTime) * 100);

        isBeingPressed = true;

        StartCoroutine(FadeOutNote());
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
