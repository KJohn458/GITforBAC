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
    public Transform leftRangedSpawn;
    public Transform rightRangedSpawn;
    public int rangedSpeed;
    private bool rangedWait;

    public bool IsGrounded;
    [SerializeField] private float airTime = 0f; 
    private float minAirTime = .5f;
    private float maxAirTime = 2f;

    private Rigidbody2D rb2d;
    private Animator a2d;
    private SpriteRenderer sprite;

    private float timer = 0;

    private bool IsWalking = false;


    // Use this for initialization
    void Start()
    {
        IsGrounded = true;
        rb2d = GetComponent<Rigidbody2D>();
        a2d = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (((timer % 60) > 1) && rangedWait == true)
        {
            timer = 0.0f;
            a2d.SetBool("IsAttacking", false);
            rangedWait = false;
        }
        

        if (Input.GetKey("a"))
        {
            a2d.SetBool("IsWalking", true);
            finalPos.x = transform.position.x - speed;
            transform.position = Vector2.Lerp(transform.position, finalPos, Time.deltaTime);
            sprite.flipX = true;
        }

        if (Input.GetKey("d"))
        {
            if(sprite.flipX == true)
            {
                sprite.flipX = false;
            }
            a2d.SetBool("IsWalking", true);
            finalPos.x = transform.position.x + speed;
            transform.position = Vector2.Lerp(transform.position, finalPos, Time.deltaTime);
        }
        if(Input.GetKey("a") == false && Input.GetKey("d") == false)
        {
            a2d.SetBool("IsWalking", false);
        }

        
        if(Input.GetKey(KeyCode.Space) && IsGrounded == true)
        { 
            if(airTime <= maxAirTime)
            {
                airTime += .05f;
            }
            
            else
            {
                Jump();
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space) && IsGrounded == true) 
        {
            Jump();
        }







        if(Input.GetKeyDown(KeyCode.Mouse0) && rangedWait == false)
        {
            a2d.SetBool("IsAttacking", true);
            rangedWait = true;
            if(sprite.flipX == true)
            {
                FireLeft();
            }
            else
            {
                FireRight();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor") 
        {
            IsGrounded = true;
            a2d.SetBool("IsGrounded", true);
        }
    }
    
    void FireRight()
    {
        var LightningBolt = (GameObject)Instantiate(rangedPrefab, rightRangedSpawn.position, rightRangedSpawn.rotation);
        //LightningBolt.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(rangedSpeed, 0), ForceMode2D.Impulse);
        LightningBolt.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(6, 0);
        Destroy(LightningBolt, 2.0f);
        
    }

    void FireLeft()
    {
        var LightningBolt = (GameObject)Instantiate(rangedPrefab, leftRangedSpawn.position, leftRangedSpawn.rotation);
        //LightningBolt.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(rangedSpeed, 0), ForceMode2D.Impulse);
        LightningBolt.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(-6, 0);
        Destroy(LightningBolt, 2.0f);

    }

    void Jump()
    {
        if (airTime < minAirTime)
        {
            rb2d.AddForce(new Vector2(0, jumpHeight * minAirTime), ForceMode2D.Impulse);
        }

        else if (airTime > maxAirTime)
        {
            rb2d.AddForce(new Vector2(0, jumpHeight * maxAirTime), ForceMode2D.Impulse);
        }

        else
        {
            rb2d.AddForce(new Vector2(0, jumpHeight * airTime), ForceMode2D.Impulse);
        }

        a2d.SetBool("IsGrounded", false);
        IsGrounded = false;
        airTime = 0f;
    }

    
}

