using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {


    Rigidbody2D rb;
    
    public Transform player;
    public Transform gun;
    public Animator anim;

    private float timer;
    private float timeActual = 1;
    public float attackDist = 5;
    public float chaseDist = 9;
    public float dist2;

    bool resting=false;
   
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
                
            timer = timeActual;        
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

       // if (Input.GetButtonDown("Jump")) { ShootMissile(); }

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
        

        Vector3 diff = player.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 0);


        float dist = Vector3.Distance(transform.position, player.position);
        dist2 = dist;
        if (dist < chaseDist && !resting)
        {
            state = State.Idle;
            // anim.SetTrigger("Ready");
            // anim.SetTrigger("Attack");
            //Debug.Log("Ready");
            ShootMissile();
        }
    }

    void ReadyUpdate()
    {
      
    }

    void RecoveringUpdate()
    { }

    void DeadUpdate()
    { }

    public void ShootMissile()
    {
        resting = true;
        GameObject bullet = Spawner.instance.Spawn("Bullet 1");
        bullet.transform.position = gun.transform.position;
       // AudioManager.instance.PlaySFX("GunPew");        
        bullet.GetComponent<OtherBulletController>().Sombrero(gun.right);
        
    }

    IEnumerator Shootalot()
    {
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "PlayerDamage")
        {
            GameObject poof = Spawner.instance.Spawn("Poof");
            poof.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);

        }
    }

}
