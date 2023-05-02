using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy2 : MonoBehaviour
{
    public DetectionZone detectionZone;

    public float moveSpeed = 1000f;
    Rigidbody2D rb;

    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    // float stunTime = 2.0f; ********* CHECK THISSSSSS

    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;

    public EnemyAI EnemyAI;
    public BackPosition BackPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        EnemyAI = GetComponent<EnemyAI>();
    }
    void FixedUpdate()
    {
        if (detectionZone.detectedObjs.Count > 0)
        {
            EnemyAI.enabled = true;
            animator.SetFloat("speed", moveSpeed);
            BackPosition.enabled = false;
        }
        else
        {
            EnemyAI.enabled = false;
            animator.SetFloat("speed", 0);
            BackPosition.enabled = true;
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
