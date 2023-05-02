using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator animator;
    //private Inventory inventory;
    private Vector2 movement;

    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool IsCrouching => canCrouch && Input.GetKey(crouchKey);

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canCrouch = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Parameters")]

    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] public float walkSpeed = 4f;
    [SerializeField] private float crouchSpeed = 2f;

    //Logic and Data types for moving the flashlight
    public GameObject viewLight;
    private Vector3 noChange = new Vector3(0.2f, -0.2f, 0.0f);

    //These values are from the local viewLight object which starts at 0
    Quaternion left = new Quaternion(0, 0, 0.707106829f, 0.707106829f);
    Quaternion right = new Quaternion(0,0,0.707106829f,-0.707106829f);
    Quaternion up = new Quaternion(0,0,0,1);
    Quaternion down = new Quaternion(0,0,1,0);
    Quaternion upLeft = new Quaternion(0,0,0.382683426f,0.923879564f);
    Quaternion upRight = new Quaternion(0,0,0.382683605f,-0.923879445f);
    Quaternion downLeft = new Quaternion(0,0,0.923879564f,0.382683426f);
    Quaternion downRight = new Quaternion(0,0,-0.923879445f,0.382683605f);

    Quaternion lastView;

    void Start(){
        viewLight.transform.SetLocalPositionAndRotation(noChange, down);
    }

    private void Awake()
    {
        //inventory = new Inventory();
    }
    // Update is called once per frame
    void Update()
    {
        // input
        Input.GetAxisRaw("Horizontal");
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");



        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);


    }

    void FixedUpdate()
    {

        if (IsSprinting)
            movementMethod(sprintSpeed,"Sprint");
        if (IsCrouching)
            movementMethod(crouchSpeed, "Crouch");
        if (!IsSprinting && !IsCrouching)
            movementMethod(walkSpeed,"Normal");
        // movement




        //This is for the flashlight
        //If you know how to translate this to a switch statement, please do so
        if(movement.x > 0){
            if(movement.y > 0){
                viewLight.transform.SetLocalPositionAndRotation(noChange, upRight);
                lastView = upRight;
            }else if(movement.y < 0){
                viewLight.transform.SetLocalPositionAndRotation(noChange, downRight);
                lastView = downRight;
            }else{
                viewLight.transform.SetLocalPositionAndRotation(noChange, right);
                lastView = right;
            }
        }else if(movement.x < 0){
            if(movement.y > 0){
                viewLight.transform.SetLocalPositionAndRotation(noChange, upLeft);
                lastView = upLeft;
            }else if(movement.y < 0){
                viewLight.transform.SetLocalPositionAndRotation(noChange, downLeft);
                lastView = downLeft;
            }else{
                viewLight.transform.SetLocalPositionAndRotation(noChange, left);
                lastView = left;
            }
        }else if(movement.x == 0){
            if(movement.y > 0){
                viewLight.transform.SetLocalPositionAndRotation(noChange, up);
                lastView = up;
            }else if(movement.y < 0){
                viewLight.transform.SetLocalPositionAndRotation(noChange, down);
                lastView = down;
            }
        }else{
            viewLight.transform.SetLocalPositionAndRotation(noChange, lastView);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if(collectible != null){
            collectible.Collect();
        }

    }
    void movementMethod (float moveSpeed, string moveType )
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        animator.SetTrigger(moveType);
    }
    void playFootStep ()
    {
        FindObjectOfType<AudioManager>().Play("McFootStep");
    }
}
