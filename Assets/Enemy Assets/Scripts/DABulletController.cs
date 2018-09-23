using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DABulletController : MonoBehaviour {

    Rigidbody2D rb;
    public Transform player;
    float speed = 3;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        player = Player.instance.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Sombrero(Vector2 right)
    {        
        StartCoroutine(Fwoosh());


        Vector2 diff = player.transform.position - transform.position;
        rb.velocity = diff.normalized * speed;

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
            yield return new WaitForSeconds(5f);
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
