using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {

   // public static PauseMenuManager instance;

    public RectTransform pauseMenuRoot;

    private void Awake()
    {
       // if (instance == null) { instance = this; }
        ResumeGame();
    }

    private void PauseGame()
    {
        pauseMenuRoot.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        
        pauseMenuRoot.gameObject.SetActive(false);
        Time.timeScale = 1;
        //PlayerController.instance.paused = false;

    }

	public void QuitGame()
    {
        Application.Quit();
    }

   

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pauseMenuRoot.gameObject.activeSelf)
            {
                PauseGame();
               // PlayerController.instance.paused = true;
            }
            else
            {
                ResumeGame();
                // PlayerController.instance.paused = false;
                
            }
        }
        }
}
