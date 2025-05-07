using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitTimingDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    float alpha, position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= Time.deltaTime * 2.5f;

        text.color = new(1, 1, 1, alpha);

        position += 150 * Time.deltaTime;

        text.rectTransform.localPosition = new(0, position);
    }

    public void SetHitTiming(NoteManager.HitTiming hitTiming)
    {
        string s = hitTiming switch
        {
            NoteManager.HitTiming.PERFECT => "Perfect",
            NoteManager.HitTiming.GREAT => "Great",
            NoteManager.HitTiming.GOOD => "Good",
            NoteManager.HitTiming.OK => "OK",
            NoteManager.HitTiming.MISS => "Miss",
            _ => "",
        };

        text.text = s;
    }
}
