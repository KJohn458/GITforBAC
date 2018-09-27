using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour {
    public int continues;
   

    // Use this for initialization
    void Start () {
        continues = 3;
	}

    private void Update()
    {
        if (continues < 0)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void LoseALife()
    {
        continues--;
    }
}
