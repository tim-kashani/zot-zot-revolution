using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] int sampleSize;

    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float intensity = 0.1f;

    float[] samples;

    Vector2[] positions;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = sampleSize;

        positions = new Vector2[sampleSize];

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float f = Mathf.PI * 2 * i / sampleSize;

            positions[i] = new(Mathf.Sin(f), Mathf.Cos(f));

            lineRenderer.SetPosition(i, positions[i]);
        }

        samples = new float[sampleSize];
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetOutputData(samples, 0);

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float f = 1 - (samples[i] * intensity);

            lineRenderer.SetPosition(i, f * positions[i]);
        }
    }
}
