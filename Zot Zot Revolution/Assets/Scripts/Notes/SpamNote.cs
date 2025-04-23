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

    public override void SetNoteLength(float f)
    {
        base.SetNoteLength(f);
    }
}
