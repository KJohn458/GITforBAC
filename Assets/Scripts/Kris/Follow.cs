using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public GameObject objToFollow;

    public float speedOfFOllow = 2.0f;

    public float height;

    Vector3 vel;
	
	// Update is called once per frame
	void FixedUpdate () {
        float interpolation = speedOfFOllow * Time.deltaTime;

        Vector3 position = objToFollow.transform.position;
        position.z = transform.position.z;
        position.y += height;


        //position.y = Mathf.Lerp(this.transform.position.y + height, objToFollow.transform.position.y + height, interpolation);
       // position.x = Mathf.Lerp(this.transform.position.x, objToFollow.transform.position.x, interpolation);

        //this.transform.position = position;
        this.transform.position = Vector3.SmoothDamp(transform.position, position, ref vel, speedOfFOllow);
    }
}
