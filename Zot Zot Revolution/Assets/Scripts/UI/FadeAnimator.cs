using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();

        yield return new WaitForEndOfFrame();

        animator.Play("In");
    }
}
