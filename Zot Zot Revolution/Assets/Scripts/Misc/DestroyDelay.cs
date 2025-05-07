using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] float delay = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay(delay));
    }

    IEnumerator Delay(float f)
    {
        yield return new WaitForSeconds(f);

        Destroy(gameObject);
    }
}
