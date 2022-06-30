using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthHandlerForPlayer : MonoBehaviour
{
    public float health;
    public float jabDamage;
    public float crossDamage;
    public ProperPunch forPlayer;//for guard detection.
    public Text healthTextPlayer;
    public AudioSource punchSound;
   
    
    
    void Start()
    {
        health = 100;
        punchSound = GetComponent<AudioSource>();
        punchSound.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DamageHandler();
        KnockOutHandler();
        GameOverHandler();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("JabEnemy") && !forPlayer.canGuard)
        {
            health -= jabDamage;
            punchSound.Play();
            
            
        }

        if (collision.collider.CompareTag("CrossEnemy") && !forPlayer.canGuard)
        {
            health -= crossDamage;
            punchSound.Play();
           
           
        }
        if (collision.collider.CompareTag("JabEnemy") && forPlayer.canGuard)
        {
            health -= jabDamage;
            punchSound.Play();
          
            
        }
        if (collision.collider.CompareTag("CrossEnemy") && forPlayer.canGuard)
        {
            health -= crossDamage;
            punchSound.Play();
            
        }
    }

    

    void DamageHandler()
    {
        healthTextPlayer.text = "Your Health: " + health.ToString("00");
        if (forPlayer.canGuard)
        {
            jabDamage = .5f;
            crossDamage = 1;
        }
        else
        {
            jabDamage = 1;
            crossDamage = 2.5f;
        }
    }

    void KnockOutHandler()
    {
        if (health <= 0)
        {
            Debug.Log("player knocked out!!!");
        }
    }

    void GameOverHandler()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
       
    }
}
