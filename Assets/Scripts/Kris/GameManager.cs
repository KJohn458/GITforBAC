using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool created = false;
    public int health = 1;

    

    private void Awake()
    {
        if (created != true)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}
}
