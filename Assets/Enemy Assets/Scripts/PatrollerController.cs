﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerController : MonoBehaviour {

    Rigidbody2D rb;
    public bool otherWay;
    public bool fryingPan;
   // public HealthController health;
    SpriteRenderer sr;
    public Animator anim;
    bool dead;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();        
       // health = GetComponent<HealthController>();
        anim = GetComponent<Animator>();
    }

   


    // Update is called once per frame
    void Update ()
    {
        if (!otherWay && !dead) { rb.velocity = transform.right; }
        else if (!dead) { rb.velocity = -transform.right; }
        else { rb.velocity = Vector2.zero; }
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

   

}
