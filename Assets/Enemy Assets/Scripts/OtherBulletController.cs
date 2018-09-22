using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBulletController : MonoBehaviour {

    Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Sombrero(Vector2 right)
    {        
        StartCoroutine(Fwoosh());

       
         rb.velocity = right * 2;

    }

    private void OnCollisionEnter2D(Collision2D c)
    {
       // Debug.Log("Yosh!");
        StartCoroutine(Vanish());
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        // Debug.Log("Yosh!");
        StartCoroutine(Vanish());
    }

    IEnumerator Fwoosh()
    {

        while (enabled)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(Vanish());
        }
    }

    IEnumerator Vanish()
    {
       // yield return new WaitForEndOfFrame();
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
        yield break;
    }
}
