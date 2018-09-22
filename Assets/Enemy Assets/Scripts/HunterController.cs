using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
{


    Rigidbody2D rb;
    public Transform player;
    SpriteRenderer sr;
    public HealthController health;
    public Animator anim;


    private float timer;
    private float timeActual = 1;
    public float attackDist = 1;
    public float chaseDist = 3;

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

    void OnEnable()
    {
        if (health == null) Debug.Log("Health is null");
        health.onHealthChanged += HealthChanged;
    }

    void OnDisable()
    {
        health.onHealthChanged -= HealthChanged;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      //  player = ColorChangeController.instance.transform;
        anim = GetComponent<Animator>();
        health = GetComponent<HealthController>();
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



        if (charging == true)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {

                state = State.Idle;
                charging = false;

                timer = timeActual;
            }
        }
    }

    void IdleUpdate()
    {

        
        
            if (!otherWay && !dead) { rb.velocity = transform.up; }
            else if (otherWay && !dead) { rb.velocity = -transform.up; }
        else { rb.velocity = Vector2.zero; }


        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < chaseDist && !charging)
        {
            state = State.Charging;
            charging = true;
        }
    }

    void ChargingUpdate()
    {

        if (!otherWay && !dead) { rb.velocity = transform.up * 3; }
        else if (otherWay && !dead) { rb.velocity = -transform.up *3; }
        else { rb.velocity = Vector2.zero; }


        // float dist = Vector3.Distance(transform.position, player.position);

        if (charging == false) { state = State.Idle; }
    }


    void DeadUpdate()
    { }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "TurnAround" && !otherWay && !fryingPan) {  otherWay = true;   fryingPan = true;  }
        if (c.gameObject.tag == "TurnAround" && otherWay && !fryingPan) {  otherWay = false;  fryingPan = true; }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        fryingPan = false;
    }


    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "TurnAround" && !otherWay && !fryingPan) { otherWay = true; fryingPan = true; }
        if (c.gameObject.tag == "TurnAround" && otherWay && !fryingPan) { otherWay = false; fryingPan = true; }
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        fryingPan = false;
    }

    void HealthChanged(float previousHealth, float health)
    {
        if (previousHealth > 0 && health == 0)
        {
            anim.SetTrigger("Death");
            state = State.Dead;
          //  AudioManager.instance.PlaySFX("bell");
            dead = true;
            gameObject.layer = 16;
        }
        else if (previousHealth > health)
        {



            anim.SetTrigger("Hurt");

            charging = false;

            timer = timeActual;
            GameObject pow = Spawner.instance.Spawn("HurtSplode");
            pow.transform.position = gameObject.transform.position;
        }
    }

}
