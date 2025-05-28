using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] int sampleSize;

    [SerializeField] LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = sampleSize;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float f = Mathf.PI * 2 * i / sampleSize;

            lineRenderer.SetPosition(i, new(Mathf.Sin(f), Mathf.Cos(f), 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
