﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiperController : MonoBehaviour {

    Rigidbody2D rb;
    public bool otherWay;
    public bool fryingPan;
   // public HealthController health;
    SpriteRenderer sr;
    public Animator anim;
    bool dead;

    public Transform player;
    private float timer;
    private float timeActual = 5;
    public float attackDist = 5;
    public float chaseDist = 4;
    bool resting = false;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();        
       // health = GetComponent<HealthController>();
        anim = GetComponent<Animator>();
        player = Player.instance.transform;
    }

   


    // Update is called once per frame
    void Update ()
    {
        if (!otherWay && !dead) { rb.velocity = -transform.right; }
        else if (!dead) { rb.velocity = transform.right; }
        else { rb.velocity = Vector2.zero; }

        float dist = Vector3.Distance(transform.position, player.position);
        
        if (dist < chaseDist && !resting)
        {
            StartCoroutine(Attack());
            anim.SetTrigger("Attack");
            AudioManager.instance.PlaySFX("Slice");
            resting = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "TurnAround" && !otherWay&&!fryingPan) { transform.Rotate(new Vector3(0, 180, 0));/* otherWay = true; */  fryingPan = true; /* sr.flipX = false;*/ }
        if (c.gameObject.tag == "TurnAround" && otherWay&&!fryingPan) { transform.Rotate(new Vector3(0, 0, 0)); /* otherWay = false; */ fryingPan = true;/* sr.flipX = true;*/ }

        if (c.gameObject.tag == "PlayerDamage")
        {
            GameObject poof = Spawner.instance.Spawn("Poof");
            poof.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);

        }
    }
        
    private void OnTriggerExit2D(Collider2D c)
    {
        fryingPan = false;
    }


    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "TurnAround" && !otherWay && !fryingPan) { transform.Rotate(new Vector3(0, 180, 0));/* otherWay = true; */  fryingPan = true; /* sr.flipX = false;*/ }
        if (c.gameObject.tag == "TurnAround" && otherWay && !fryingPan) { transform.Rotate(new Vector3(0, 0, 0)); /* otherWay = false; */ fryingPan = true;/* sr.flipX = true;*/ }
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        fryingPan = false;
    }

   IEnumerator Attack()
    {
        while (enabled) {
            yield return new WaitForSeconds(3);
            resting = false;
            yield break;
        }

        
    }

}
