using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMovement PlayerMovement;

    private float health = 0f;
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] public float invokeTime = 2f;

    public Animator animator;
    private bool dead = false; 

    private void Start()
    {
        health = maxHealth;
        PlayerMovement = GetComponent<PlayerMovement>();
    }
    public void UpdateHealth(float mod)
    {

        if (!dead)
        {
            
            health += mod;

            if (health > maxHealth)
            {
                health = maxHealth;
            }
            else if (health <= 0)
            {
                health = 0f;
                PlayerDied();

            }
        }
    }

    public void PlayerDied()
    {
        PlayerMovement.enabled = false;
        
        FindObjectOfType<AudioManager>().Play("EvilLaugh");
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        if (!dead) 
        { 
            animator.SetTrigger("Dead");
            
        }
        dead = true;
        Invoke("GameOver", invokeTime);
    }

    private void GameOver()
    {
        
        gameObject.SetActive(false);
        LevelManager.instance.GameOver();
    }
}
