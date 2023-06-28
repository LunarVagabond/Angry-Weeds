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

    [SerializeField]
    public Text numOfMonsters;

    [SerializeField] public Text playerScoreText;
    public int playerScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        waveText.text = "Wave: 1";
        shotPotato.MonsterDecrementEvent += DecrementMonsterTracker;
        StartCoroutine(SpawnMonsters());
        StopCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        int numberOfEnemies = Mathf.RoundToInt(baseEnemiesPerWave + ((GameManager.instance.CurrentWave - 1) * 2));
        monstersLeftTracker = numberOfEnemies;
        UpdateTotalMonsterText(numberOfEnemies);
        UpdateRemainingMonsterText();
        spawnedEnemies.Clear();
        for (int i = 1; i <= numberOfEnemies; i++)
        {
            yield return new WaitForSeconds(Random.Range(2, 8));
            // Wait between a range of 1 and 5 seconds 
            int maxRange = monsterReference.Length - 1;
            randomEnemy = Random.Range(0, maxRange);
            if (randomEnemy == 2) i--; // spawned a bat don't count against the iterations 

            // Spawns a copy the object determined by the index 0,1,2.
            spawnedMonster = Instantiate(monsterReference[randomEnemy]);
            GameObject groundCheck = new GameObject("GroundCheck");
            groundCheck.transform.SetParent(spawnedMonster.transform);
            // [Chris]: Ask I'll explain =)
            spawnedMonster.transform.name = $"Monster {i}: {spawnedMonster.name.Substring(0, spawnedMonster.name.Length - 7)}";
            groundCheck.transform.localPosition = new Vector2(0f, -gameObject.transform.localScale.y / 2f);
            spawnedMonster.gameObject.GetComponent<Monster>().groundCheck = groundCheck.transform;
            spawnedMonster.gameObject.GetComponent<Monster>().mType = randomEnemy;
            spawnedMonster.gameObject.GetComponent<Monster>().objName = $"Monster {i}: {spawnedMonster.name.Substring(0, spawnedMonster.name.Length - 7)}";
            spawnedMonster.gameObject.GetComponent<Monster>().groundLayer = LayerMask.GetMask("Ground");
            spawnedMonster.gameObject.GetComponent<Monster>().groundCheckRadius = 0.15f;
            spawnedMonster.gameObject.GetComponent<Monster>().jumpPercentage = Random.Range(20, 51) / 100f; // set the jump rate to some random percentage (min = 0, max = 40)
            spawnedEnemies.Add(spawnedMonster);
            // End my knowledge
            spawnedMonster.transform.parent = gameObject.transform; // dump spawned monsters under the spawner
                                                                    // Check if we spawned a bat, bats do not count as enemies that must be killed
                                                                    // So we don't add this to the list 

            spawnedEnemies.Add(spawnedMonster); // Enemies added to the list, except for bats 

            spawnedMonster.transform.parent = gameObject.transform; // dump spawned monsters under the spawner


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

            }
            else if (randomSpawn == 1) // RIGHT BOTTOM - If randomSpawn is 1 we spawn on the bottom right side of platform 1 
            {


                spawnedMonster.transform.position = bottomRightPos.position; // Spawns in the bottom right
                spawnedMonster.transform.localScale = new Vector2(spawnedMonster.transform.localScale.x * -1, spawnedMonster.transform.localScale.y);

                spawnedMonster.GetComponent<Monster>().speed = -Random.Range(3, 10);
                // We spawn a random negative number between -4 and -10 so the enemy from
                // the right will travel to the left side of the screen.
            }
            else if (randomSpawn == 2 || randomSpawn == 3)// MIDDLE - This means we spawned a middle position / Try to even out the likelihood
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
                    default:
                        break;

                }


                spawnedMonster.GetComponent<Monster>().speed = Random.Range(3, 10);
                // Speed will range between 4 and 10.
                // Monster in this sense is the class that is tagged onto the Ghost
                // Red Monster and Green Monster. So Monster is the parent.
                // So whatever Monster spawns it will have a random speed between
                // 4 and 10.

            }
        } // End of While Loop

    }

    void OnDestroy()
    {
        shotPotato.MonsterDecrementEvent -= DecrementMonsterTracker;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DecrementMonsterTracker(string tag)
    {
        if (tag != "Bat")
        {
            monstersLeftTracker--;
            UpdateRemainingMonsterText();

            playerScore += 10;
            UpdatePlayerScoreText(playerScore);

            if (monstersLeftTracker < 1)
            {
                GameManager.instance.UpWave();
                UpdateWaveText();
                StartCoroutine(SpawnMonsters());
            }
        }
    }

    void UpdateRemainingMonsterText() => numOfMonsters.text = "Monsters Left: " + monstersLeftTracker.ToString();
    void UpdateWaveText() => waveText.text = "Wave: " + GameManager.instance.CurrentWave.ToString();
    void UpdateTotalMonsterText(int numberOfEnemies) => totalMonstersText.text = "Total Monsters: " + numberOfEnemies.ToString();
    void UpdatePlayerScoreText(int playerScore) => playerScoreText.text = "Score: " + playerScore.ToString();
}
