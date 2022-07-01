using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthHandler : MonoBehaviour
{
    public float health;
    public float jabDamage;
    public float crossDamage;
    
   
    public EnemyFightingAi forEnemy;// for guard detection.
    public Text healthText;
    public Text theLevelText;

    public AudioSource punchSound;
   
    
   
    void Start()
    {
        health = 100;
        punchSound = GetComponent<AudioSource>();
        punchSound.Stop();
        
    }

   
    void FixedUpdate()
    {
        KnockOutHandler();
        DamageHandler();
        GameOverHandler();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Jab") && !forEnemy.guard)
        {
            health -= jabDamage;

            punchSound.Play();
         
       
            

        }

        if (collision.collider.CompareTag("Cross") && !forEnemy.guard)
        {
            health -= crossDamage;

            punchSound.Play();

        }

        if (collision.collider.CompareTag("Jab") && forEnemy.guard)
        {
            health -= jabDamage;

            punchSound.Play();

          
          
           

        }

        if (collision.collider.CompareTag("Cross") && forEnemy.guard)
        {
            health -= crossDamage;

            punchSound.Play();


          
           
           
        }

    }

    
   
    public void KnockOutHandler()
    {
        if (health <= 0)
        {
            Debug.Log("knocked out");
        }
    }

    public void DamageHandler()
    {
        healthText.text = "Enemy Health: " + health.ToString("00");
        theLevelText.text = "Level is " + PlayerPrefs.GetInt("levelNumber");
        if (forEnemy.guard) // if enemy guards damages reduces.
        {
            jabDamage = 0.5f;
            crossDamage = 1;
            
        }
        else
        {
            jabDamage = 1;
            crossDamage = 2.5f;
           
        }
    }

    void GameOverHandler()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
