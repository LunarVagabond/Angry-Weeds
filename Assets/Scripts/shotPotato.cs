using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shotPotato : MonoBehaviour
{
    Player player;
    private int shotDirection; // 1 for to the right, -1 for to the left
    private float potatoShotForce = 20.0f;
    public int enemiesKilled = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        //need to determine if this potato is going to the right or left
        shotDirection = player.playerFaceDirection;
    }

    public delegate void DecrementMonsterEventHandler(Collision2D collision);
    public static event DecrementMonsterEventHandler MonsterDecrementEvent;

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

    void checkBoundaries()
    {
        // VPC 6/21 - We should fill in this to check if the projectile has reached the edge of the camera view
        // We probably do not want the ammo rounds to continue off screen and kill enemies that the 
        // player cannot see. 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bat")
        {
            Destroy(collision.gameObject); // Kill all enemies including bats 
            Destroy(this.gameObject);
            // FIXME: this is not the best way to do this 
            MonsterSpawner ms = collision.gameObject.GetComponentInParent<MonsterSpawner>();
            ms.spawnedEnemies.Remove(collision.gameObject);
            ms.numOfMonsters.text = "Monsters Left: " + ms.spawnedEnemies.Count;
            MonsterDecrementEvent?.Invoke(collision);
        }
    }

}
