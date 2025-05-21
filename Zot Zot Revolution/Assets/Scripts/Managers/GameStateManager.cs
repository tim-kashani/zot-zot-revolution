using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager gameStateManager;

    // Start is called before the first frame update
    void Start()
    {
        if (gameStateManager == null)
        {
            gameStateManager = this;

            transform.parent = null;

            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
