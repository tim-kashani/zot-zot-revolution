using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // needed for fading

public class GhostNote : Note
{
    // Ghost Note Guidelines

    // Like a default note, except it should slowly fade out to where it's invisible before the judgement zone

    [SerializeField] Image noteImage;
}
