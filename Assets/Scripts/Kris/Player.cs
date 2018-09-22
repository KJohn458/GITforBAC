using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform player;
    public int speed;
    public float jumpHeight;
    private Vector2 finalPos;
    public GameObject rangedPrefab;
    public Transform rangedSpawn;
    public int rangedSpeed;

    public bool isGrounded;
    public float startJumpTime;
    public float maxJumpTime;
    public float jumpAcceleration;
    public float airJumpTime;

    private Rigidbody2D rb2d;



    // Use this for initialization
    void Start()
    {
        isGrounded = true;
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

        if (Input.GetKeyDown("space") && isGrounded == true && (startJumpTime + maxJumpTime > Time.time))
        {
            isGrounded = false;
            rb2d.AddForce(Vector2.up * jumpAcceleration, ForceMode2D.Impulse);

        }

        else if (Input.GetKeyDown("space") && isGrounded == true)
        {
            Debug.Log("entered statement");
            isGrounded = false;
            startJumpTime = Time.time;
            maxJumpTime = startJumpTime + airJumpTime;
            rb2d.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            
        }
        

        







        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
    
    void Fire()
    {
        var LightningBolt = (GameObject)Instantiate(rangedPrefab, rangedSpawn.position, rangedSpawn.rotation);
        //LightningBolt.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(rangedSpeed, 0), ForceMode2D.Impulse);
        LightningBolt.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(6, 0);

        Destroy(LightningBolt, 2.0f);
    }
}

