using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {




    public Image healthBar;
    public HealthController health;
    public float animDuration = .5f;
   
    public Color healthy;
    public Color okay;
    public Color hurt;

   

    private void OnEnable()
    {
        health.onHealthChanged += UpdateImage;
    }

    private void OnDisable()
    {
        health.onHealthChanged -= UpdateImage;
    }

    
    void UpdateImage(float previousHealth, float health)
    {
        StopCoroutine("AnimateHealth");
        StartCoroutine("AnimateHealth");
        
    }


    void Start()
    {
        StopCoroutine("AnimateHealth");
        StartCoroutine("AnimateHealth");

    }

    private void Update()
    {
        float halfHealth = (healthBar.fillAmount-.5f )* 2;

        if (healthBar.fillAmount>=.5f)
        {
            healthBar.color = Color.Lerp(okay, healthy, halfHealth );
        }        
        else 
        {
             healthBar.color = Color.Lerp(hurt, okay, healthBar.fillAmount*2);
        }

    }


    IEnumerator AnimateHealth()
        {           

            float start = healthBar.fillAmount;
            float end = health.health / this.health.maxHealth;
            for (float t = 0; t < animDuration; t += Time.deltaTime)
            {
            
                float frac = t / animDuration;
                healthBar.fillAmount = Mathf.Lerp(start, end, frac);
                
                yield return new WaitForEndOfFrame();
            }

        }


   //     healthText.text = health.health + " / " + health.maxHealth;
    }


