using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
{


    Rigidbody2D rb;
    public Transform player;
    SpriteRenderer sr;
   // public HealthController health;
    public Animator anim;


    private float timer;
    private float timeActual = 1;
    public float attackDist = 1;
    public float chaseDist = 3;
    public float speed = 1.5f;

    public bool charging = false;

    public bool otherWay;
    public bool fryingPan;
    bool dead=false;


    public enum State
    {
        Idle,
        Charging,
        Dead
    }
    public State state = State.Idle;

   


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      //  player = ColorChangeController.instance.transform;
        anim = GetComponent<Animator>();
       // health = GetComponent<HealthController>();
        timer = timeActual;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                IdleUpdate();
                break;

            case State.Charging:
                ChargingUpdate();

                break;

            case State.Dead:
                DeadUpdate();
                break;

            default:
                Debug.LogWarning("Undefined enemy state");
                break;

        }



      //  if (charging == true)
      //  {
      //      timer -= Time.deltaTime;
      //
      //      if (timer < 0)
      //      {
      //
      //          state = State.Idle;
      //          charging = false;
      //
      //          timer = timeActual;
      //      }
      //  }
    }

    void IdleUpdate()
    {

             rb.velocity = Vector2.zero; 


        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < chaseDist && !charging)
        {
            state = State.Charging;
          //  charging = true;
        }
    }

    void ChargingUpdate()
    {

        Vector2 diff = player.transform.position - transform.position;
        rb.velocity = diff.normalized * speed;
    }


    void DeadUpdate()
    { }

    private void OnCollisionEnter2D(Collision2D c)
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "PlayerDamage")
        {
            gameObject.SetActive(false);
        }
    }

}
