using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Vector3 = UnityEngine.Vector3;

public class shotPotato : MonoBehaviour
{
    Player player;
    Camera cam;
    private int shotDirection; // 1 for to the right, -1 for to the left
    private float potatoShotForce = 20.0f;
    public int enemiesKilled = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //need to determine if this potato is going to the right or left
        shotDirection = player.playerFaceDirection;
    }

    public delegate void DecrementMonsterEventHandler(GameObject tag);
    public static event DecrementMonsterEventHandler MonsterDecrementEvent;

    //These variables are used to detect screen edge in environment 
    public Matrix4x4 cameraPos;
    private float screenLeftEdge;
    private float screenRightEdge;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        moveTater();
        checkBoundaries();
    }

    void moveTater()
    {
        // shot moves to the right
        if (shotDirection == 1)
        {
            transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime * potatoShotForce;
        }
        // shot moves to the left
        else if (shotDirection == -1)
        {
            transform.position += new Vector3(-1f, 0f, 0f) * Time.deltaTime * potatoShotForce;
        }
        else
            Debug.Log("The tater did not get the direction... womp womp!");
    }

    // VPC 6/27 - this checks if a fired potato reaches the edge of the camera and destroys it if so
    void checkBoundaries()
    {
        cameraPos = cam.cameraToWorldMatrix;
        //Debug.Log("Camera transform matrix = " + cameraPos.ToString());

        float checkval = cameraPos.GetRow(0).w; //VPC 6/27 - not sure why the x value is stored as
                                                //'w' in this matrix, but can't find much documentation
                                                //Debug.Log("right side = " + (checkval+10) + "    left side = " + (checkval-10));

        screenLeftEdge = checkval - 10;
        screenRightEdge = checkval + 10;
        float curPosition = this.transform.position.x;
        if (curPosition > screenRightEdge || curPosition < screenLeftEdge)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            MonsterDecrementEvent?.Invoke(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

}
