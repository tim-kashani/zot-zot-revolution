using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int notePosition;

    // this is for spacing notes equally from 1-4, can be changed but is static so it stays the same across notes
    static float xSpacing = 200, ySpacing = 200;

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

    public virtual void SetXPositionAndTime(int x, float y)
    {
        notePosition = x;

        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new(CalculateXPosition(x), y * ySpacing);
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
