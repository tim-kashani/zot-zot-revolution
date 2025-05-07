using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBounce : MonoBehaviour
{
    float scale = 1, noteBounceScale = 1, beatBounceScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (noteBounceScale < 1)
        {
            noteBounceScale += Time.deltaTime * 1.5f;
        } else
        {
            noteBounceScale = 1;
        }

        if (beatBounceScale < 1)
        {
            beatBounceScale += Time.deltaTime * 1.5f;
        }
        else
        {
            beatBounceScale = 1;
        }

        float min = Mathf.Min(noteBounceScale, beatBounceScale);

        float lerpSpeed = 5f;

        if (scale > min)
        {
            lerpSpeed = 10f;
        }

        scale = Mathf.Lerp(scale, min, Time.deltaTime * lerpSpeed);

        transform.localScale = new(1, scale, 1);
    }

    public void NoteBounce()
    {
        noteBounceScale = 0.6f;
    }

    public void BeatBounce()
    {
        noteBounceScale = 0.8f;
    }
}
