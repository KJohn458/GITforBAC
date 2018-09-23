using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausemenu : MonoBehaviour {

    bool paused = false;
    public GameObject pMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            paused = togglePause();
            

    }

    private void OnGUI()
    {
        if(paused == true)
        {
            if (GUILayout.Button("Click me to quit game"))
                Application.Quit();
        }
        
    }


    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            pMenu.SetActive(false);
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            pMenu.SetActive(true);
            return (true);
        }
    }
}
