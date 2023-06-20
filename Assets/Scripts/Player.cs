using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Vars
    // ******* Global Variables *******

    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    [SerializeField]
    private AudioSource jumpSFX;

    [SerializeField]
    private AudioSource landingSFX;
    [SerializeField] private AudioSource runningSFX;

    private float movementX;
    private float movementY;

    [SerializeField]
    private Rigidbody2D myBody;

    private Animator anim;
    private SpriteRenderer spriteR;

    [SerializeField] private SpriteRenderer[] PlayerSprites;
    private SpriteRenderer SpriteGun;

    private string WALK_ANIMATION = "Walk"; 
    private string JUMP_ANIMATION = "isJumping"; 
    private string GROUND_TAG = "Ground";
    private string GUN_ANIMATION = "hasPGun";

    public Transform groundCheck;
    public float groundCheckRadius = 0f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private const float rightSideOfScreen = 98.47478f;
    private const float leftSideOfScreen  = -98.47478f;
    [SerializeField] private AudioSource pickUpSFX;
    public int ammoCount = 0;

    [SerializeField]  private bool hasPGUN = false;

    private Vector2 rightFace = new Vector2(0.25f, 0.11f),
                    rightFaceJump = new Vector2(-0.51f, 0.3f),
                    leftFace = new Vector2(-0.25f, 0.11f),
                    leftFaceJump = new Vector2(0.51f, 0.3f);

    // ******* Global Variables *******
    #endregion

    private void Awake()
    {
        // Getting Components so we can manipulate them in code
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();

        //VPC 6/19 - puts all sprite renderers in game object and children, even if inactive (which the gun is to start)
        // and puts into an array
        PlayerSprites = GetComponentsInChildren<SpriteRenderer>(true);
        // This is horrible, but works for now. Maybe fix in future to find it by name
        SpriteGun = PlayerSprites[1];
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
            if (!runningSFX.isPlaying && isGrounded)
                runningSFX.Play();
            anim.SetBool(WALK_ANIMATION, true);
            spriteR.flipX = true; // Going to the right side, VPC 6/13 - have to flip t/f for new sprite
            
            // VPC 6/19 - flipping and re-centering the gun 
            SpriteGun.flipX = true;

            if (anim.GetBool(JUMP_ANIMATION))
            {
                SpriteGun.transform.SetLocalPositionAndRotation(rightFaceJump, Quaternion.Euler(0f, 0f, 270f));
            }
            else 
            {
                SpriteGun.transform.SetLocalPositionAndRotation(rightFace, Quaternion.identity);
            }
        }
        else if (movementX < 0) // Going to the left 
        {
            if (!runningSFX.isPlaying && isGrounded)
                runningSFX.Play();
            anim.SetBool(WALK_ANIMATION, true);
            spriteR.flipX = false; // Going to the left size, VPC 6/13 - have to flip t/f for new sprite

            // VPC 6/19 - flipping and re-centering the gun 
            SpriteGun.flipX = false;
            
            if (anim.GetBool(JUMP_ANIMATION))
            {
                SpriteGun.transform.SetLocalPositionAndRotation(leftFaceJump, Quaternion.Euler(0f, 0f, 90f));
            }
            else
            {
                SpriteGun.transform.SetLocalPositionAndRotation(leftFace, Quaternion.identity);
            }
        }
        else // The player is not moving 
        {
            runningSFX.Pause();
            anim.SetBool(WALK_ANIMATION, false);

        }
    }

    void PlayerJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            runningSFX.Pause();
            jumpSFX.Play();
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            anim.SetBool(JUMP_ANIMATION, true); // VPC 6/14 - adding the setting of jump animation for characters
            if (spriteR.flipX)
            {
                SpriteGun.transform.SetLocalPositionAndRotation(rightFaceJump, Quaternion.Euler(0f, 0f, 270f));
            }
            else
            {
                SpriteGun.transform.SetLocalPositionAndRotation(leftFaceJump, Quaternion.Euler(0f, 0f, 90f));
            }
        }
    }

    // Checks to see if the player is colliding onto the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player and the ground collides 
        if (isGrounded)
        {
            if (!isGrounded) landingSFX.Play(); // need the if to stop constant collision boops
            anim.SetBool(JUMP_ANIMATION, false); // VPC 6/14 - turning off jump animation when hitting ground

            if (spriteR.flipX)
            {
                SpriteGun.transform.SetLocalPositionAndRotation(rightFace, Quaternion.identity);
            }
            else
            {
                SpriteGun.transform.SetLocalPositionAndRotation(leftFace, Quaternion.identity);
            }
            
        }
        if (collision.gameObject.tag == "Ammo")
        {
            pickUpSFX.Play();
            Destroy(collision.gameObject);
            ammoCount += Random.Range(1, 5);
        }
        if (collision.gameObject.tag == "PotatoGun") {
            pickUpSFX.Play();
            Destroy(collision.gameObject);
            hasPGUN = true;
            anim.SetBool(GUN_ANIMATION, true);
        }
    }
}
