using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Gem : MonoBehaviour, ICollectible
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    Vector2 targetPosition;
    public float speed;
    //public Transform player;
    public static event HandleGemCollected OnGemCollected;
    public delegate void HandleGemCollected(ItemData itemData);
    public ItemData gemData;
    public int keyValue = 1;
    public AudioClip keySound;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 1f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    public GameObject keySpawn;

    public Vector2[] possibleSpawnPositions =
        {
            new Vector2(12, 26),  // Top-left big room
            new Vector2(32, 24),   // in the top-middle
            new Vector2(59, 30),    // top-right corner
            new Vector2(28, -3),    // bottom-left
            new Vector2(40, 8),     // upper small room
            new Vector2(40, 1),     // bottom small room
            new Vector2(59, -3),     // bottom-right corner

        };
    //public int[] isTrue = new int[7] {0,0,0,0,0,0,0};
    public int keycount = 1;

    Rigidbody2D rb;
    public float movesp = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    // Start is called before the first frame update
    void Start()
    {
        if(keycount < 4){
            keycount++;
            int z = GetRandomNumber();
            Instantiate(keySpawn, possibleSpawnPositions[z], Quaternion.identity);
        }
        //targetPosition = GetRandomPosition();
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();

    }

    void FixedUpdate()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime){
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));


    }

    int GetRandomNumber(){
        int randomNum = Random.Range(0, 7);

        return randomNum;
    }

    void calcuateNewMovementVector(){

        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;

    }
    public void Collect(){
        KeyManager.instance.ChangeKey(keyValue);
        AudioSource.PlayClipAtPoint(keySound, transform.position);
        Destroy(gameObject);
        OnGemCollected?.Invoke(gemData);
    }
    void OnCollisionEnter2D (Collision2D col){
        if (col.gameObject.tag.Equals("Player")){
            Collect();
        }
    }

}


