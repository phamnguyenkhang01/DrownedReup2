using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public float speed;
    public Transform[] patrolPoints;
    public float waitTime;
    int currentPointIndex;

    public Animator animator;

    bool once; 
    private void Update()
    {
        if(transform.position != patrolPoints[currentPointIndex].position)
        {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        animator.SetFloat("speed", speed);
        }
        else
        {
            if(once == false)
            {
                once = true;                            
                StartCoroutine(Wait());

            }
            animator.SetFloat("speed", 0);
        }
    }
    IEnumerator Wait ()
    {
        yield return new WaitForSeconds(waitTime);
        if(currentPointIndex + 1 < patrolPoints.Length)
        {
        currentPointIndex++;

        }
        else
        {
            currentPointIndex = 0; 
        }
        once = false;

    }
}
