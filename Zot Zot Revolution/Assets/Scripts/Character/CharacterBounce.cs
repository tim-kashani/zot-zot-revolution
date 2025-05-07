using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBounce : MonoBehaviour
{
    float scale = 1, noteBounceScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (noteBounceScale < 1)
        {
            noteBounceScale += Time.deltaTime;
        } else
        {
            noteBounceScale = 1;
        }

        scale = Mathf.Lerp(scale, noteBounceScale, Time.deltaTime * 5);

        transform.localScale = new(1, scale, 1);
    }

    public void NoteBounce()
    {
        noteBounceScale = 0.5f;
    }
}
