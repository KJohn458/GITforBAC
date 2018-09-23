using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Transform player;
    public int speed;
    public float maxJumpHeight;
    public float minJumpHeight;
    private Vector2 finalPos;

    public GameObject rangedPrefab;
    public Transform leftRangedSpawn;
    public Transform rightRangedSpawn;
    public int rangedSpeed;
    private bool rangedWait;
    public Transform GroundCheckOrigin;

    public bool IsGrounded;
    private float speedForce = 0f;
    private float minAirTime = .5f;
    private float maxAirTime = 2f;

    private Rigidbody2D rb2d;
    private Animator a2d;
    private SpriteRenderer sprite;
    private float timer = 0;

    private bool IsWalking = false;

    public HealthController health;


    private void Awake()
    {
        if (instance == null) { instance = this; }
    }


    // Use this for initialization
    void Start()
    {
        IsGrounded = true;
        rb2d = GetComponent<Rigidbody2D>();
        a2d = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        if (health == null) Debug.Log("Health is null");
        health.onHealthChanged += HealthChanged;
    }

    void OnDisable()
    {
        health.onHealthChanged -= HealthChanged;
    }

    private void Update()
    {
        if(timer <= 60 && rangedWait == true)
        {
           // Debug.Log(timer);
            timer += Time.deltaTime;
        }
        if (((timer % 60) > 1))
        {
           // Debug.Log(timer);
            timer = 0.0f;
            a2d.SetBool("IsAttacking", false);
            rangedWait = false;
        }



    }


    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckOrigin.position, transform.up * -1, .01f);
        if (hit.collider != null)
        {
            if(hit.collider.tag == "floor")
            {
               // Debug.Log("Hit!");
                IsGrounded = true;
                a2d.SetBool("IsGrounded", true);
            }
            
        }

         


        if (Input.GetKey("a"))
        {
            a2d.SetBool("IsWalking", true);
            finalPos.y = transform.position.y;
            finalPos.x = transform.position.x - speed;
            Vector3 hi = Vector3.Lerp(transform.position, finalPos, Time.deltaTime);
            transform.position = hi;
            sprite.flipX = true;
        }

        if (Input.GetKey("d"))
        {
            if (sprite.flipX == true)
            {
                sprite.flipX = false;
            }
            a2d.SetBool("IsWalking", true);
            finalPos.y = transform.position.y;
            finalPos.x = transform.position.x + speed;
            transform.position = Vector2.Lerp(transform.position, finalPos, Time.deltaTime);
        }
        if (Input.GetKey("a") == false && Input.GetKey("d") == false)
        {
            a2d.SetBool("IsWalking", false);
        }


        if (Input.GetKey(KeyCode.Space) && IsGrounded == true)
        {
            if (speedForce <= maxAirTime)
            {
                speedForce += .1f;
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







        if (Input.GetKeyDown(KeyCode.Mouse0) && rangedWait == false)
        {
            a2d.SetBool("IsAttacking", true);
            rangedWait = true;
            if (sprite.flipX == true)
            {
                FireLeft();
            }
            else
            {
                FireRight();
            }
        }
    }

    
    
    void FireRight()
    {
        var LightningBolt = (GameObject)Instantiate(rangedPrefab, rightRangedSpawn.position, rightRangedSpawn.rotation);
        //LightningBolt.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(rangedSpeed, 0), ForceMode2D.Impulse);
        LightningBolt.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(6, 0);
        Destroy(LightningBolt, 2.0f);
        AudioManager.instance.PlaySFX("attack");
        // StartCoroutine("WaitForShoot");
    }

    void FireLeft()
    {
        var LightningBolt = (GameObject)Instantiate(rangedPrefab, leftRangedSpawn.position, leftRangedSpawn.rotation);
        //LightningBolt.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(rangedSpeed, 0), ForceMode2D.Impulse);
        LightningBolt.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(-6, 0);
        Destroy(LightningBolt, 2.0f);
        AudioManager.instance.PlaySFX("attack");
        //StartCoroutine("WaitForShoot");
    }

    void Jump()
    {
        if (speedForce < minAirTime)
        {
            rb2d.AddForce(new Vector2(0, minJumpHeight * minAirTime), ForceMode2D.Impulse);
        }

        else if (speedForce > maxAirTime)
        {
            rb2d.AddForce(new Vector2(0, maxJumpHeight * maxAirTime), ForceMode2D.Impulse);
        }

        else
        {
            rb2d.AddForce(new Vector2(0, minJumpHeight * minAirTime), ForceMode2D.Impulse);
        }

        a2d.SetBool("IsGrounded", false);
        IsGrounded = false;
        speedForce = 0f;
    }

    void HealthChanged(float previousHealth, float health)
    {
        if (previousHealth > 0 && health == 0)
        {
            a2d.SetTrigger("Death");

            StartCoroutine(Restart());
            rb2d.velocity = Vector3.zero;
        }
        else if (previousHealth > health)
        {
            a2d.SetTrigger("Hurt");
            AudioManager.instance.PlaySFX("PlayerHit");
            rb2d.velocity = Vector3.zero;
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SteamCity")
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

        if (collision.tag == "Bossfight")
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
    }
    /*
    IEnumerator WaitForShoot()
    {
        yield return new WaitForSeconds(.3f);
        a2d.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(.7f);
        rangedWait = false;
        yield break;
    }
    */
}

