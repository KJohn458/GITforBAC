using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    public float damage = 1;

    void HitObject (GameObject g)
    {
        HealthController health = g.GetComponent<HealthController>();
        if (health != null)
        {
            health.TakeDamage(damage);

        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        HitObject(c.gameObject);
        
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        HitObject(c.gameObject);
        
    }
}
