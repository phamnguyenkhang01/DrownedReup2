using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    
    Animator animator;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    public Transform player;

    public float killRadius;

    // bool following = false;
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }

     void calcuateNewMovementVector(){
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        Vector3 direction = player.position - transform.position;
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
        animator.SetFloat("MoveX", movementDirection.x);
        animator.SetFloat("MoveY", movementDirection.y);
        // Debug.Log(direction);
        // Debug.Log(movementDirection);
    }

    void calcuateFollowMovementVector(){
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        Vector3 direction = player.position - transform.position;
        // Debug.Log(direction);
        movementDirection = new Vector2(direction.x, direction.y).normalized;
        movementPerSecond = movementDirection * characterVelocity;
        animator.SetFloat("MoveX", movementDirection.x);
        animator.SetFloat("MoveY", movementDirection.y);
        // Debug.Log(movementDirection);
        // Debug.Log("Following");
    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if(col.gameObject.tag == "Player"){
    //         following = true;
    //         Debug.Log("Following");
    //     }
    // }

    // void OnTriggerExit2D(Collider2D col)
    // {
    //     if(col.gameObject.tag == "Player"){
    //         following = false;
    //         Debug.Log("Not Following");
    //     }
    // }

    void Update()
    {
        // Vector3 direction = player.position - transform.position;
        // Debug.Log(direction);
        // Debug.Log(direction.x);
        // Debug.Log(direction.y);
    }
    
    void FixedUpdate()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime){
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        if (transform.position != player.position) {
            Vector3 direction = player.position - transform.position;
            // Debug.Log(direction);
            if (System.Math.Abs(direction.x) < killRadius || System.Math.Abs(direction.y) < killRadius) {
                calcuateFollowMovementVector();
                // transform.position = Vector2.MoveTowards(transform.position, player.position, (movementPerSecond.x + movementPerSecond.y) * Time.deltaTime);
                transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y + (movementPerSecond.y * Time.deltaTime));
            } else {
                //move enemy: 
                transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y + (movementPerSecond.y * Time.deltaTime));
            }
        } else {
            movementPerSecond.x = 0;
            movementPerSecond.y = 0;
        }
    }
}
