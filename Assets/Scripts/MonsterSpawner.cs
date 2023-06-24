using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;
    [SerializeField] Text waveText;

    [SerializeField] private int baseEnemiesPerWave = 15;
    public float scalingFactor = 1.2f;
    [SerializeField] private Text totalMonstersText;

    [SerializeField]
    private Transform bottomLeftPos, bottomRightPos;
    [SerializeField]
    private Transform[] middlePos;

    private GameObject spawnedMonster;
    public List<GameObject> spawnedEnemies;
    public int monstersLeftTracker;

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
        waveText.text = "Wave: 1";
        shotPotato.MonsterDecrementEvent += DecrementMonsterTracker;
        StartCoroutine(SpawnMonsters());
        StopCoroutine(SpawnMonsters());
    }

    void Awake() {
        totalMonstersText.text = "Total Monsters: " + baseEnemiesPerWave.ToString();
        // numOfMonsters.text = "Monsters Left: " + totalMonstersToSpawn;

        totalBatsSpawn = (Mathf.RoundToInt((baseEnemiesPerWave / 3) * Mathf.Pow(scalingFactor, GameManager.instance.CurrentWave - 1)) );
        // Have a max of bats that can spawn on the bottom two layers, depending on the total number of monsters
        // which is determined by the wave number we are on

    }

    IEnumerator SpawnMonsters() {
        int numberOfEnemies = Mathf.RoundToInt(baseEnemiesPerWave * Mathf.Pow(scalingFactor, GameManager.instance.CurrentWave - 1));
        monstersLeftTracker = numberOfEnemies;
        totalMonstersText.text = "Total Monsters: " + baseEnemiesPerWave.ToString();
        spawnedEnemies.Clear();
        for (int i = 1; i <= numberOfEnemies; i++) { 
            yield return new WaitForSeconds(Random.Range(2, 8));
            // Wait between a range of 1 and 5 seconds 
            int maxRange = batMax >= totalBatsSpawn ? monsterReference.Length -1 : monsterReference.Length;
            randomEnemy = Random.Range(0, maxRange);

            // Spawns a copy the object determined by the index 0,1,2.
            spawnedMonster = Instantiate(monsterReference[randomEnemy]);
            GameObject groundCheck = new GameObject("GroundCheck");
            groundCheck.transform.SetParent(spawnedMonster.transform);
            // [Chris]: Ask I'll explain =)
            spawnedMonster.transform.name =   $"Monster {i}: {spawnedMonster.name.Substring(0, spawnedMonster.name.Length - 7)}";
            groundCheck.transform.localPosition = new Vector2(0f, -gameObject.transform.localScale.y / 2f);
            spawnedMonster.gameObject.GetComponent<Monster>().groundCheck = groundCheck.transform;
            spawnedMonster.gameObject.GetComponent<Monster>().mType = randomEnemy;
            spawnedMonster.gameObject.GetComponent<Monster>().objName = $"Monster {i}: {spawnedMonster.name.Substring(0, spawnedMonster.name.Length - 7 )}";
            spawnedMonster.gameObject.GetComponent<Monster>().groundLayer = LayerMask.GetMask("Ground");
            spawnedMonster.gameObject.GetComponent<Monster>().groundCheckRadius = 0.15f;
            spawnedMonster.gameObject.GetComponent<Monster>().jumpPercentage = Random.Range(0, 41) / 100f; // set the jump rate to some random percentage (min = 0, max = 40)
            spawnedEnemies.Add(spawnedMonster);
            // End my knowledge
            spawnedMonster.transform.parent = gameObject.transform; // dump spawned monsters under the spawner
            numOfMonsters.text = "Monsters Left: " + spawnedEnemies.Count;
                // Check if we spawned a bat, bats do not count as enemies that must be killed
                // So we don't add this to the list 

                if (spawnedMonster.tag == "Bat")
                    batMax++; // Keep track of bats added for the bottom two platforms
                else 
                    spawnedEnemies.Add(spawnedMonster); // Enemies added to the list, except for bats 

                spawnedMonster.transform.parent = gameObject.transform; // dump spawned monsters under the spawner
            



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

    void OnDestroy() {
        shotPotato.MonsterDecrementEvent -= DecrementMonsterTracker;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DecrementMonsterTracker(Collision2D collision) {
        if (collision.gameObject.tag == "Bat") return;
        monstersLeftTracker--;
        if (monstersLeftTracker <= 1) {
            GameManager.instance.CurrentWave++;
            waveText.text = "Wave: " + GameManager.instance.CurrentWave.ToString();
            StartCoroutine(SpawnMonsters());
        }
    }

}
