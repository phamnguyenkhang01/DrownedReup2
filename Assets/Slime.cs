using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public PatrolDetectionZone patrolDetectionZone;
        
    public float moveSpeed = 1000f;
    Rigidbody2D rb;

    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    // float stunTime = 2.0f; check thissssss**********88

    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;

    public PatrolEnemy PatrolEnemy;
    public BackPatrol BackPatrol;
    public EnemyAI EnemyAI;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        PatrolEnemy = GetComponent<PatrolEnemy>();
    }
    void FixedUpdate()
    {
        if (patrolDetectionZone.detectedObjs.Count == 1)
        {
            EnemyAI.enabled = true;
            animator.SetFloat("speed", moveSpeed);
            PatrolEnemy.enabled = false;
            BackPatrol.enabled = false;
        }
        else
        {
            EnemyAI.enabled = false;
            animator.SetFloat("speed", 0);
            if (!PatrolEnemy.enabled)
            { 
                BackPatrol.enabled = true; 
            }
            
                
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
        }
    }

        // Change from here ---------------------------------------------------

        // Start is called before the first frame update

        public void TakeDamage(int damage)
    {
        if (moveSpeed > 200)
        {
            moveSpeed -= damage;
        }

        
        animator.SetTrigger("Hurt");
        // Play hurt animation                
    }
    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }


}