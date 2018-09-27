using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour {

    public int lives = 3;
    public static Lives instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (this.gameObject == null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
    {
        if (lives < 0)
        {
            lives = 3;
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
