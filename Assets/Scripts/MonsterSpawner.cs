using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;

    [SerializeField] public int totalMonstersToSpawn;
    [SerializeField] private Text totalMonstersText;

    [SerializeField]
    private Transform bottomLeftPos, bottomRightPos;
    [SerializeField]
    private Transform[] middlePos;

    private GameObject spawnedMonster;
    public List<GameObject> spawnedEnemies;

    private int randomEnemy;
    private int randomSpawn;

    private float x_scale = 0.0f;
    private float y_scale = 0.0f;
    private float z_scale = 0.0f;

    private int batMax = 0;
    private float totalBatsSpawn;

    [SerializeField]
    public Text numOfMonsters;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    void Awake() {
        totalMonstersText.text = "Total Monsters: " + totalMonstersToSpawn.ToString();
        numOfMonsters.text = "Monsters Left: " + totalMonstersToSpawn;

        totalBatsSpawn = (totalMonstersToSpawn / 3);
        // Have a max of bats that can spawn on the bottom two layers, depending on the total number of monsters
        // which is determined by the wave number we are on

    }

    IEnumerator SpawnMonsters() {
        spawnedEnemies.Clear();
        for (int index = 1; index <= totalMonstersToSpawn; index++) { //always true while loop
            yield return new WaitForSeconds(Random.Range(1, 5));
            // Wait between a range of 1 and 5 seconds 

            randomEnemy = Random.Range(0, monsterReference.Length);
            //  A random number between 0 and the Monster reference range
            // minus 1. So between 0,1,and 2.
            //Keeps track of how many monsters have been spawned

            // If we spawn a bat, and go over our max don't even instantiate the bat object
            if (randomEnemy == 2 && batMax >= totalBatsSpawn)
            {
                Debug.Log("We reached our limit for bats to spawn");
                index--;
            }

            else
            {
                // Create enemy, could be a bat if it is we won't add it to the monster list 
                spawnedMonster = Instantiate(monsterReference[randomEnemy]);

                // Check if we spawned a bat, bats do not count as enemies that must be killed
                // So we don't add this to the list 
                if (spawnedMonster.tag == "Bat")
                {

                    Debug.Log("A Bat was instatiated but not added to the list");
                    batMax++; // Keep track of bats added for the bottom two platforms
                    index--; // If we spawn a bat, we decrement from the count because it doesn't count as an enemy we have to kil


                }
                else
                {

                    spawnedEnemies.Add(spawnedMonster); // Enemies added to the list, except for bats 
                }


                spawnedMonster.transform.parent = gameObject.transform; // dump spawned monsters under the spawner
            }



            //numOfMonsters.text = "Monsters Left: " + totalMonstersToSpawn;

            // VPC 6/14 - The new sprites have different default scales between enemy types. Getting them here after
            // creation so that the Vector3 operation below doesn't set them back to 1.0f
            x_scale = spawnedMonster.transform.localScale.x;
            y_scale = spawnedMonster.transform.localScale.y;
            z_scale = spawnedMonster.transform.localScale.z;


            randomSpawn = Random.Range(0, 4); // 0, 1, 2, 3



            if (randomSpawn == 0) // LEFT BOTTOM -  If randomSpawn is 0 we spawn on the bottom left side of platform 1 
            {
        

                spawnedMonster.transform.position = bottomLeftPos.position; // Spawns in the bottom left 




                spawnedMonster.GetComponent<Monster>().speed = Random.Range(3, 10);
                // Speed will range between 4 and 10.
                // Monster in this sense is the class that is tagged onto the Ghost
                // Red Monster and Green Monster. So Monster is the parent.
                // So whatever Monster spawns it will have a random speed between
                // 4 and 10.

                if (randomEnemy == 2)
                {
                    x_scale *= -1;
                    spawnedMonster.transform.localScale = new Vector3(x_scale, y_scale, z_scale);
                }
            }
            else if(randomSpawn == 1) // RIGHT BOTTOM - If randomSpawn is 1 we spawn on the bottom right side of platform 1 
            {


                spawnedMonster.transform.position = bottomRightPos.position; // Spawns in the bottom right


                spawnedMonster.GetComponent<Monster>().speed = -Random.Range(3, 10);
                // We spawn a random negative number between -4 and -10 so the enemy from
                // the right will travel to the left side of the screen.

                if (randomEnemy != 2)
                {
                    x_scale *= -1;
                    spawnedMonster.transform.localScale = new Vector3(x_scale, y_scale, z_scale);
                    // Flip the enemy to face the left direction
                }
            }
            else if(randomSpawn == 2 || randomSpawn == 3)// MIDDLE - This means we spawned a middle position / Try to even out the likelihood
            {
                int randomMiddleSpawn = Random.Range(0, 9);

                switch (randomMiddleSpawn)
                {
                    case 0:
                        spawnedMonster.transform.position = middlePos[0].position;
                        break;
                     
                    case 1:
                        spawnedMonster.transform.position = middlePos[1].position;
                        break;
                    case 2:
                        spawnedMonster.transform.position = middlePos[2].position;
                        break;
                    case 3:
                        spawnedMonster.transform.position = middlePos[3].position;
                        break;
                    case 4:
                        spawnedMonster.transform.position = middlePos[4].position;
                        break;
                    case 5:
                        spawnedMonster.transform.position = middlePos[5].position;
                        break;
                    case 6:
                        spawnedMonster.transform.position = middlePos[6].position;
                        break;
                    case 7:
                        spawnedMonster.transform.position = middlePos[7].position;
                        break;
                    case 8:
                        spawnedMonster.transform.position = middlePos[8].position;
                        break;
                    default:
                        break;

                }


                spawnedMonster.GetComponent<Monster>().speed = Random.Range(3, 10);
                // Speed will range between 4 and 10.
                // Monster in this sense is the class that is tagged onto the Ghost
                // Red Monster and Green Monster. So Monster is the parent.
                // So whatever Monster spawns it will have a random speed between
                // 4 and 10.

                if (randomEnemy == 2)
                {
                    x_scale *= -1;
                    spawnedMonster.transform.localScale = new Vector3(x_scale, y_scale, z_scale);
                }

            }
        } // End of While Loop
   
    }


    // Update is called once per frame
    void Update()
    {
        
    }

  

}
