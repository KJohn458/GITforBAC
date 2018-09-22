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
    private float airTime;

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

        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Debug.Log("in looperboy");
            airTime = 1.1f;
            rb2d.AddForce(new Vector2(0, jumpHeight * airTime), ForceMode2D.Impulse);
            airTime -= .1f;
            isGrounded = false;
        }






        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") ;
        {
            isGrounded = true;
        }
    }
    
    void Fire()
    {
        var LightningBolt = (GameObject)Instantiate(rangedPrefab, rangedSpawn.position, rangedSpawn.rotation);
        //LightningBolt.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(rangedSpeed, 0), ForceMode2D.Impulse);
        LightningBolt.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(6, 0);

        Destroy(LightningBolt, 2.0f);
    }
}

