using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;

    [SerializeField] private int totalMonstersToSpawn = 10;
    [SerializeField] private Text totalMonstersText;

    [SerializeField]
    private Transform leftPos, rightPos;
    [SerializeField]
    private Transform leftPos2, rightPos2;
    [SerializeField]
    private Transform leftPos3, rightPos3;

    private GameObject spawnedMonster;
    public List<GameObject> spawnedEnemies;

    private int randomIndex;
    private int randomSide;

    private float x_scale = 0.0f;
    private float y_scale = 0.0f;
    private float z_scale = 0.0f;

    [SerializeField]
    public Text numOfMonsters;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    void Awake() {
        totalMonstersText.text = "Total Monsters: " + totalMonstersToSpawn.ToString();
    }

    IEnumerator SpawnMonsters() {
        spawnedEnemies.Clear();
        for (int i = 1; i <= totalMonstersToSpawn; i++) { //always true while loop
            yield return new WaitForSeconds(Random.Range(3, 8));
            // Wait between a range of 1 and 5 seconds 

            randomIndex = Random.Range(0, monsterReference.Length);
            //  A random number between 0 and the Monster reference range
            // minus 1. So between 0,1,and 2.
            //Keeps track of how many monsters have been spawned


            randomSide = Random.Range(0, 2); // 0, 1

            // Spawns a copy the object determined by the index 0,1,2.
            spawnedMonster = Instantiate(monsterReference[randomIndex]);
            GameObject groundCheck = new GameObject("GroundCheck");
            groundCheck.transform.SetParent(spawnedMonster.transform);
            // [Chris]: Ask I'll explain =)
            spawnedMonster.transform.name =   $"Monster {i}: {spawnedMonster.name.Substring(0, spawnedMonster.name.Length - 7)}";
            groundCheck.transform.localPosition = new Vector2(0f, -gameObject.transform.localScale.y / 2f);
            spawnedMonster.gameObject.GetComponent<Monster>().groundCheck = groundCheck.transform;
            spawnedMonster.gameObject.GetComponent<Monster>().mType = randomIndex;
            spawnedMonster.gameObject.GetComponent<Monster>().objName = $"Monster {i}: {spawnedMonster.name.Substring(0, spawnedMonster.name.Length - 7 )}";
            spawnedMonster.gameObject.GetComponent<Monster>().groundLayer = LayerMask.GetMask("Ground");
            spawnedMonster.gameObject.GetComponent<Monster>().groundCheckRadius = 0.15f;
            spawnedMonster.gameObject.GetComponent<Monster>().jumpPercentage = Random.Range(0, 41) / 100f; // set the jump rate to some random percentage (min = 0, max = 40)
            spawnedEnemies.Add(spawnedMonster);
            // End my knowledge
            spawnedMonster.transform.parent = gameObject.transform; // dump spawned monsters under the spawner
            numOfMonsters.text = "Monsters Left: " + spawnedEnemies.Count;

            // VPC 6/14 - The new sprites have different default scales between enemy types. Getting them here after
            // creation so that the Vector3 operation below doesn't set them back to 1.0f
            x_scale = spawnedMonster.transform.localScale.x;
            y_scale = spawnedMonster.transform.localScale.y;
            z_scale = spawnedMonster.transform.localScale.z;

            if (randomSide == 0) // Left Side 
            {
                int randomLeftIndex = Random.Range(0,3); // Create a random numbers between: 0, 1, 2

                // 0 = Bottom Left
                // 1 = Middle Left
                // 2 = Top Left

                if(randomLeftIndex == 0)
                {
                    spawnedMonster.transform.position = leftPos.position; // Spawns in the bottom left 
                }
                else if(randomLeftIndex == 1)
                {
                    spawnedMonster.transform.position = leftPos2.position; // Spawns in the middle left 

                }
                else if(randomLeftIndex == 2)
                {
                    spawnedMonster.transform.position = leftPos3.position; // Spawns in the topleft 

                }


                spawnedMonster.GetComponent<Monster>().speed = Random.Range(3, 10);
                // Speed will range between 4 and 10.
                // Monster in this sense is the class that is tagged onto the Ghost
                // Red Monster and Green Monster. So Monster is the parent.
                // So whatever Monster spawns it will have a random speed between
                // 4 and 10.

                if (randomIndex == 2) { 
                    x_scale *= -1;
                    spawnedMonster.transform.localScale = new Vector3(x_scale, y_scale, z_scale);
                }
            }
            else // Right Side
            {


                int randomRightIndex = Random.Range(0, 3); // Create a random numbers between: 0, 1, 2

                // 0 = Bottom Right
                // 1 = Middle Right
                // 2 = Top Right

                if (randomRightIndex == 0)
                {
                    spawnedMonster.transform.position = rightPos.position; // Spawns in the bottom right
                }
                else if (randomRightIndex == 1)
                {
                    spawnedMonster.transform.position = rightPos2.position; // Spawns in the middle right

                }
                else if (randomRightIndex == 2)
                {
                    spawnedMonster.transform.position = rightPos3.position; // Spawns in the top right

                }
             
                spawnedMonster.GetComponent<Monster>().speed = -Random.Range(3, 10);
                // We spawn a random negative number between -4 and -10 so the enemy from
                // the right will travel to the left side of the screen.

                if (randomIndex != 2)
                {
                    x_scale *= -1;
                    spawnedMonster.transform.localScale = new Vector3(x_scale, y_scale, z_scale);
                    // Flip the enemy to face the left direction
                }
            }
        } // End of While Loop
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
