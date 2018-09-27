using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGameObject()
    {

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void CloseGameObject()
    {
        Application.Quit();
    }

    public void CreditsGameObject()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }   

    public void MainMenuObject()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
