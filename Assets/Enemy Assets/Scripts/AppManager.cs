using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour {

    public static AppManager instance;
    public bool winner = false;
    bool ding = false;
    public GameObject text;
    public GameObject confetti;
    

    private void Awake()
    {
        if (instance == null) { instance = this; }
       // DontDestroyOnLoad(this.gameObject);
    }

    public void Wnn()
    {
        StartCoroutine(HappyEnd());
    }


    IEnumerator HappyEnd()
    {
        yield return new WaitForSeconds(3);
        AppManager.instance.confetti.SetActive(true);
        AppManager.instance.text.SetActive(true);
        yield break;
    }
}
