using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour {


    Rigidbody2D rb;
    public HealthController health;
    public Transform player;
    public Transform gun;
    public Animator anim;

    private float timer;
    private float timeActual = 5;
    public float attackDist = 5;
    public float chaseDist = 9;
    public float dist2;

    bool resting=false;
    bool lefty = false;
    bool righty = true;
    bool dead = true;
    

    public enum State
    {
        Idle,        
        Ready,
        Recovering,
        Dead        
    }

    public State state = State.Idle;

   
    void Start ()
    {        
            rb = GetComponent<Rigidbody2D>();
       // player = ColorChangeController.instance.transform;
            anim = GetComponent<Animator>();
            health = GetComponent<HealthController>();            
            timer = timeActual;        
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
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                IdleUpdate();
                break;
            
            case State.Ready:
                      ReadyUpdate();

                break;
            case State.Recovering:
                RecoveringUpdate();
                break;
            case State.Dead:
                DeadUpdate();
                break;
            
            default:
                Debug.LogWarning("Undefined enemy state");
                break;

        }

        if (Input.GetButtonDown("Jump")) { ShootMissile(); }

        if (resting == true)
        {
            timer -= Time.deltaTime;
            
            if (timer < 0)
            {
               
                state = State.Idle;
                resting = false;
               
                timer = timeActual;
            }
        }
    }

    void IdleUpdate()
    {
       // if (transform.position.x < player.transform.position.x && lefty==false ) { transform.Rotate(new Vector3(0, 180, 0)); lefty = true; righty = false; }
       // else if (transform.position.x > player.transform.position.x && righty==false ) { transform.Rotate(new Vector3(0, 0, 0)); lefty = false; righty = true; }
        //else { transform.Rotate(new Vector3(0, 0, 0)); lefty = false; }

        float dist = Vector3.Distance(transform.position, player.position);
        dist2 = dist;
        if (dist < chaseDist && !resting)
        {
            state = State.Idle;
           // anim.SetTrigger("Ready");
            anim.SetTrigger("Attack");
            Debug.Log("Ready");
        }
    }

    void ReadyUpdate()
    {
      //  float dist = Vector3.Distance(transform.position, player.position);
      //  dist2 = dist;
      //  if (dist < attackDist && !resting)
      //  {
      //      // ShootMissile();
      //       anim.SetTrigger("Attack");
      //      Debug.Log("tack");
      //  }
      //  else if (dist > chaseDist )
      //  {
      //      state = State.Idle;
      //      anim.SetTrigger("Idle");
      //      Debug.Log("Retreat");
      //  }
    }

    void RecoveringUpdate()
    { }

    void DeadUpdate()
    { }

    public void ShootMissile()
    {
        resting = true;
        GameObject bullet = Spawner.instance.Spawn("Bullet");
        bullet.transform.position = gun.transform.position;
       // AudioManager.instance.PlaySFX("GunPew");        
        bullet.GetComponent<BulletController>().Sombrero(gun.right);
        
    }

    void HealthChanged(float previousHealth, float health)
    {
        if (previousHealth > 0 && health == 0)
        {
           // anim.SetTrigger("Death");
            state = State.Dead;
            // gameObject.SetActive(false);
            chaseDist = 0;
            attackDist = 0;
            dead = true;
            gameObject.layer = 16;

        }
        else if (previousHealth > health)
        {

            // anim.SetTrigger("ow");

            GameObject pow = Spawner.instance.Spawn("HurtSplode");
            pow.transform.position = gameObject.transform.position;

        }
    }

}
