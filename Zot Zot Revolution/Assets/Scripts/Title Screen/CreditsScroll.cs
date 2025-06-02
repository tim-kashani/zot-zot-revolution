using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField] RectTransform text;

    float max = 2400;

    // Start is called before the first frame update
    void Start()
    {
        text.anchoredPosition = new(0, -max);
    }

    public void Scroll(float f)
    {
        float y = Mathf.Lerp(-max, max, f);

        text.anchoredPosition = new(0, y);
    }
}
