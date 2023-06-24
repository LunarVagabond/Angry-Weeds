using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed;
    public float direction;
    
    private Rigidbody2D myBody;
    public int mType { get; set; }
    bool isGrounded;
    public float jumpPercentage;
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;
    public string objName;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        // If this is not a bat monster
        if (mType != 2) StartCoroutine(MonsterJump());
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        // We are changing the x velocity but the Y will be the same
        // from the Inspector which is 0.
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }

    IEnumerator MonsterJump()
    {
        Debug.Log($"{objName} jump chance {jumpPercentage}");
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 21));
            //  Every X seconds there is a 30% chance to jump
            if (isGrounded && Random.value < jumpPercentage)
            {
                Debug.Log($"<color=orange>Monster {objName} Jumped</color>");
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);
            }
        }
    }
} // Class
