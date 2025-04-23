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

    public override void SetNoteLength(float f)
    {
        base.SetNoteLength(f);

        holdBar.sizeDelta = new(holdBar.sizeDelta.x, f * ySpacing);
    }
}
