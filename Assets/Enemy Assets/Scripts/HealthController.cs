using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour {

    public float maxHealth=3;
    public float health;

    public delegate void OnHealthChanged(float previousHealth, float health);
    public event OnHealthChanged onHealthChanged = delegate { };

    void Awake()
    {
        health = maxHealth;
    }

    
    public void TakeDamage(float damage)
    {
        float oldhealth = health;
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        onHealthChanged(oldhealth,health);
    }




}
