using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // needed for fading

public class GhostNote : Note
{
    // Ghost Note Guidelines

    // Like a default note, except it should slowly fade out to where it's invisible before the judgement zone

    // music.GetCurrentBeat() is your friend

    //[SerializeField] Image noteImage;

    bool hasBeenPressed;

    private void Update()
    {
        if (hasBeenPressed)
        {
            return;
        }

        float beatsToJudgement = pressTime - music.GetCurrentBeat();

        if (beatsToJudgement > 6)
        {
            return;
        }

        float alpha = Mathf.Clamp01((beatsToJudgement - 2) / 3);

        SetImageAlpha(alpha);
    }

    public override void OnPress()
    {
        base.OnPress();

        hasBeenPressed = true;

        SetImageAlpha(1);
    }

    void SetImageAlpha(float f)
    {
        Debug.Log("Alpha: " + f);

        noteImage.color = new(noteImage.color.r, noteImage.color.g, noteImage.color.b, f);
    }
}
