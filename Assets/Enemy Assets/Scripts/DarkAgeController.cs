using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkAgeController : MonoBehaviour {



    Rigidbody2D rb;

    public Transform player;
    public Transform gun;
    public Animator anim;
   public HealthController health;
    public GameObject evilmusic;

    private float timer;
    private float timeActual = 5;
    public float attackDist = 5;
    public float chaseDist = 9;
    public float dist2;

    bool resting = false;
    bool lefty = false;
    bool righty = true;
    bool dead = false;

    public Transform alpha;
    public Transform beta;

    public enum State
    {
        Idle,
        Ready,
        Recovering,
        Dead
    }

    public State state = State.Idle;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = Player.instance.transform;
        anim = GetComponent<Animator>();
        health = GetComponent<HealthController>();
        timer = timeActual;
        StartCoroutine(Movement());
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

        //if (Input.GetButtonDown("Jump")) { GameObject poof = Spawner.instance.Spawn("Poof"); }

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
        if ( !resting && !dead) //dist < chaseDist &&
        {
            state = State.Idle;
            // anim.SetTrigger("Ready");
            anim.SetTrigger("Attack"); resting = true;
            // Debug.Log("Ready");
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
        GameObject bullet = Spawner.instance.Spawn("Bullet 2");
        bullet.transform.position = gun.transform.position;
         AudioManager.instance.PlaySFX("woosh");        
        bullet.GetComponent<DABulletController>().Sombrero(gun.right);

    }

    void spooked() { AudioManager.instance.PlaySFX("Spook"); }
    void FinalDeath() {
        GameObject poof = Spawner.instance.Spawn("Poof");
        poof.transform.position = gameObject.transform.position; gameObject.SetActive(false);
        AppManager.instance.Wnn();
    }


    void HealthChanged(float previousHealth, float health)
    {
        if (previousHealth > 0 && health == 0)
        {
           // AudioManager.instance.PlaySFX("Spook");
            anim.SetTrigger("Death");
            dead = true;
            state = State.Dead;
            rb.velocity = Vector2.zero;
            evilmusic.SetActive(false);
            
        }
        else if (previousHealth > health)
        {
          // anim.SetTrigger("Hurt");
            AudioManager.instance.PlaySFX("Heartbeat");
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator Movement()
    {

        while (enabled)
        {
            yield return new WaitForSeconds(.5f);
            for (float t = 0; t < 5f; t += Time.deltaTime)
            {
                float frac = t / 5f;

                gameObject.transform.position = Vector2.Lerp(alpha.position, beta.position, frac);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(.5f);
            
            for (float t = 0; t < 5f; t += Time.deltaTime)
            {
                float frac = t / 5f;

                gameObject.transform.position = Vector2.Lerp(beta.position, alpha.position, frac);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    
}
