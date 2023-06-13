using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // ******* Global Variables *******

    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;
    private float movementY;

    [SerializeField]
    private Rigidbody2D myBody;

    private Animator anim;
    private SpriteRenderer spriteR;

    private string WALK_ANIMATION = "Walk"; //VPC 6/13 "run_side" changing from "Walk"; to accomodate new asset animation
    private string GROUND_TAG = "Ground";

    private bool isGrounded;
    private const float rightSideOfScreen = 98.47478f;
    private const float leftSideOfScreen  = -98.47478f;


    // ******* Global Variables *******

    private void Awake()
    {
        // Getting Components so we can manipulate them in code
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyBoard();
        animatePlayer();
        PlayerJump();
    }


    private void PlayerMoveKeyBoard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        // Gets A and D keys or left and right arrow keys

        movementY = Input.GetAxisRaw("Vertical");

        //Transform is built in for the game object 


        if(transform.position.x < -99)

        {
            transform.position = new Vector3(rightSideOfScreen, transform.position.y, 0f);

        }
        else if(transform.position.x > 99)
        {
            transform.position = new Vector3(leftSideOfScreen, transform.position.y, 0f);

        }
        else
        {
            transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;

        }

    }

    void animatePlayer()
    {
        if (movementX > 0) // Going to the right side
        {
            anim.SetBool(WALK_ANIMATION, true);
            spriteR.flipX = true; // Going to the right side, VPC 6/13 - have to flip t/f for new sprite
        }
        else if (movementX < 0) // Going to the left 
        {

            anim.SetBool(WALK_ANIMATION, true);
            spriteR.flipX = false; // Going to the left size, VPC 6/13 - have to flip t/f for new sprite
        }
        else // The player is not moving 
        {
            anim.SetBool(WALK_ANIMATION, false);

        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false; // Allows us to not jump two times
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }
    }

    // Checks to see if the player is colliding onto the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player and the ground collides 
        if (collision.gameObject.CompareTag(GROUND_TAG))
            isGrounded = true; // The player is on the ground
    }

}
