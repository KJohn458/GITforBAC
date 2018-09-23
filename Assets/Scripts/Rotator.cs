using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float num;

	// Use this for initialization
	void Start () {
        num = Random.Range(1, 6);
	}
	
	// Update is called once per frame
	void Update () {
        
       
        if (num == 1)      { transform.Rotate(0,  0, - 20 * Time.deltaTime); }
        else if (num == 2) { transform.Rotate(0, 0, -40 * Time.deltaTime); }
        else if (num == 3) { transform.Rotate(0, 0, 10 * Time.deltaTime); }
        else if (num == 4) { transform.Rotate(0, 0, 20 * Time.deltaTime); }
        else if (num == 5) { transform.Rotate(0, 0, -70 * Time.deltaTime); }
        else if (num == 6) { transform.Rotate(0, 0, 50 * Time.deltaTime); }


    }
}
