using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] RectTransform[] characters;

    [SerializeField] AudioSource music;

    [SerializeField] Animator fadeAnimator;

    float characterBounce, currentCharacterBounce;

    static float bpm = 120;

    int currentBeat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float beat = music.time * bpm / 60;

        if (currentBeat != (int)beat)
        {
            currentBeat = (int)beat;

            characterBounce = 0.9f;
        }

        if (characterBounce < 1)
        {
            characterBounce += Time.deltaTime / 2;

            if (characterBounce >= 1)
            {
                characterBounce = 1;
            }
        }

        currentCharacterBounce = Mathf.Lerp(currentCharacterBounce, characterBounce, Time.deltaTime * 10);

        BounceCharacters();
    }

    void BounceCharacters()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].localScale = new(characters[i].localScale.x, currentCharacterBounce, 1);
        }
    }

    public void StartButton()
    {
        fadeAnimator.Play("Out");

        StartCoroutine(StartButtonCoroutine());
    }

    IEnumerator StartButtonCoroutine()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Level Select");
    }
}
