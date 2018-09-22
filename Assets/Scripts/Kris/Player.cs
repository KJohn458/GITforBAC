using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform player;
    public int speed;
    public int jumpHeight;
    private Vector2 finalPos;
    private bool hasJumped;
    public GameObject rangedPrefab;
    public Transform rangedSpawn;
    public int rangedSpeed;

    private Rigidbody2D rb2d;



    // Use this for initialization
    void Start()
    {
        hasJumped = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            finalPos.x = transform.position.x - speed;
            transform.position = Vector2.Lerp(transform.position, finalPos, Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            finalPos.x = transform.position.x + speed;
            transform.position = Vector2.Lerp(transform.position, finalPos, Time.deltaTime);
        }


        if (Input.GetKeyDown("space") && hasJumped != true)
        {
            rb2d.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            hasJumped = true;
        }







        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasJumped = false;
    }
    
    void Fire()
    {
        var LightningBolt = (GameObject)Instantiate(rangedPrefab, rangedSpawn);
        LightningBolt.GetComponent<Rigidbody2D>().AddForce(new Vector2(rangedSpeed, 0), ForceMode2D.Impulse);

        Destroy(LightningBolt, 2.0f);
    }
}

