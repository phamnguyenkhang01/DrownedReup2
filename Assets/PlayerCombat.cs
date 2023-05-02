using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;  
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 400;

    public float attackRate = 2f;
    float nextAttackTime = 0f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time>= nextAttackTime )
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate; 
            }
    }

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,enemyLayers );

        // Damage them
        foreach(Collider2D slime in hitEnemies)
        {
            slime.GetComponent<Slime>().TakeDamage(attackDamage);
        }

    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
