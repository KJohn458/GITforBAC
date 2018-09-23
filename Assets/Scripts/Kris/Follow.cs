using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public GameObject objToFollow;

    public float speedOfFOllow = 2.0f;

	
	// Update is called once per frame
	void Update () {
        float interpolation = speedOfFOllow * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, objToFollow.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, objToFollow.transform.position.x, interpolation);

        this.transform.position = position; 
	}
}
