using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    // press time is when the note should be pressed, note length is for hold and spam notes
    float pressTime, noteLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void OnPress()
    {

    }

    public virtual void OnRelease()
    {

    }
}
