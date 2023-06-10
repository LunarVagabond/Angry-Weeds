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

    [SerializeField]
    private Rigidbody2D myBody;

    private Animator anim;
    private SpriteRenderer spriteR;

    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";

    private bool isGrounded;


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

        //Transform is built in for the game object 

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;

    }

    void animatePlayer()
    {
        if (movementX > 0) // Going to the right side
        {
            anim.SetBool(WALK_ANIMATION, true);
            spriteR.flipX = false; // Going to the right side
        }
        else if (movementX < 0) // Going to the left 
        {

            anim.SetBool(WALK_ANIMATION, true);
            spriteR.flipX = true; // Going to the left size 
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
