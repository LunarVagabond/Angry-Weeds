using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSpawner : MonoBehaviour
{
    [SerializeField] int MaxSpawns;
    [SerializeField] Transform[] spawnLocations;
    [SerializeField] GameObject[] monsterPrefabs;
    private List<GameObject> spawnedMonsters = new List<GameObject>();
    int maxAllowedSpawnCount {get; set;}
    // Start is called before the first frame update
    void Awake()
    {
        shotPotato.MonsterDecrementEvent += DecrementCurrentCount;
        GameManager.WaveUpEvent += ResetSpawns;
        StartCoroutine(SpawnMonsters());
    }
    
    void ResetSpawns() {
        StopCoroutine(SpawnMonsters());
        StartCoroutine(SpawnMonsters());
    }

    void OnDestroy() {
        shotPotato.MonsterDecrementEvent -= DecrementCurrentCount;
    }

    void DecrementCurrentCount(GameObject go) {
        maxAllowedSpawnCount--;
        Destroy(go);
    } 


    IEnumerator SpawnMonsters()
    {
        maxAllowedSpawnCount = Mathf.RoundToInt(MaxSpawns + (GameManager.instance.CurrentWave - 1) * 1.2f);
        while (spawnedMonsters.Count <= maxAllowedSpawnCount && spawnedMonsters.Count < 12)
        {
            Debug.Log($"{maxAllowedSpawnCount}");
            int mIndex = Random.Range(0, monsterPrefabs.Length);
            Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
            GameObject monster = Instantiate(monsterPrefabs[mIndex]);
            monster.transform.name = $"Monster {maxAllowedSpawnCount}: {monster.name.Substring(0, monster.name.Length - 7)}";
            monster.gameObject.GetComponent<Monster>().mType = mIndex;
            monster.gameObject.GetComponent<Monster>().objName = $"Monster {maxAllowedSpawnCount}: {monster.name.Substring(0, monster.name.Length - 7)}";
            monster.transform.parent = spawnLocation;
            SetGroundCheck(monster);
            spawnedMonsters.Add(monster);
            monster.transform.position = spawnLocation.position;

            // We are on the right side of map monster needs to go left
            if (spawnLocation.position.x > 0)
            {
                monster.GetComponent<Monster>().speed = -Random.Range(3, 10);
            }
            // Left Side need to go right
            else
            {
                monster.GetComponent<Monster>().speed = Random.Range(3, 10);
                monster.transform.localScale = new Vector2(monster.transform.localScale.x * -1, monster.transform.localScale.y);
            }
            yield return new WaitForSeconds(Random.Range(2, 8));
        }
    }

    void SetGroundCheck(GameObject monster)
    {
        GameObject groundCheck = new GameObject("GroundCheck");
        groundCheck.transform.SetParent(monster.transform);
        groundCheck.transform.localPosition = new Vector2(0f, -gameObject.transform.localScale.y / 2f);
        monster.gameObject.GetComponent<Monster>().groundCheck = groundCheck.transform;
        monster.gameObject.GetComponent<Monster>().groundLayer = LayerMask.GetMask("Ground");
        monster.gameObject.GetComponent<Monster>().groundCheckRadius = 0f;
        monster.gameObject.GetComponent<Monster>().jumpPercentage = 0f;
    }


}
