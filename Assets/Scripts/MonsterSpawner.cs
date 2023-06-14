using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MonsterSpawner : MonoBehaviour
{
    public static int monsterCount = 1;

    [SerializeField]
    private GameObject[] monsterReference;

    [SerializeField]
    private Transform leftPos, rightPos;

    private GameObject spawnedMonster;

    private int randomIndex;
    private int randomSide;

    private float x_scale = 0.0f;
    private float y_scale = 0.0f;
    private float z_scale = 0.0f;

    [SerializeField]
    private Text numOfMonsters;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters() {

        while(true) { //always true while loop
            yield return new WaitForSeconds(Random.Range(3, 8));
            // Wait between a range of 1 and 5 seconds 

            randomIndex = Random.Range(0, monsterReference.Length);
            //  A random number between 0 and the Monster reference range
            // minus 1. So between 0,1,and 2.
            //Keeps track of how many monsters have been spawned
            numOfMonsters.text = "Monsters to kill: " + MonsterSpawner.monsterCount.ToString();

            randomSide = Random.Range(0, 2); // 0, 1

            // Spawns a copy the object determined by the index 0,1,2.
            spawnedMonster = Instantiate(monsterReference[randomIndex]);

            // VPC 6/14 - The new sprites have different default scales between enemy types. Getting them here after
            // creation so that the Vector3 operation below doesn't set them back to 1.0f
            x_scale = spawnedMonster.transform.localScale.x;
            y_scale = spawnedMonster.transform.localScale.y;
            z_scale = spawnedMonster.transform.localScale.z;

            if (randomSide == 0) // Left Side 
            {
                spawnedMonster.transform.position = leftPos.position;
                spawnedMonster.GetComponent<Monster>().speed = Random.Range(3, 10);
                // Speed will range between 4 and 10.
                // Monster in this sense is the class that is tagged onto the Ghost
                // Red Monster and Green Monster. So Monster is the parent.
                // So whatever Monster spawns it will have a random speed between
                // 4 and 10.
            }
            else // Right Side 
            {
                spawnedMonster.transform.position = rightPos.position;
                spawnedMonster.GetComponent<Monster>().speed = -Random.Range(3, 10);
                // We spawn a random negative number between -4 and -10 so the enemy from
                // the right will travel to the left side of the screen.

                x_scale *= -1;
                spawnedMonster.transform.localScale = new Vector3(x_scale, y_scale, z_scale);
                // Flip the enemy to face the left direction
            }
        MonsterSpawner.monsterCount++;
        } // End of While Loop
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
