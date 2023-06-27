using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{

    public GameObject[] ammoTypes; // ammo types
    public int minX;
    public int maxX;
    public int yPos = 20;
    public float minSpawnTime;
    public float maxSpawnTime;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(AmmoDrop());
  
    }

    IEnumerator AmmoDrop() {
        while(true){
            int xPos = Random.Range(minX, maxX);
            int ammoToSpawn = Random.Range(0,ammoTypes.Length - 1);
            GameObject n = Instantiate(ammoTypes[ammoToSpawn], new Vector2(xPos, yPos), Quaternion.identity);
            n.transform.parent = this.transform;
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime + 1)); // max is not included so we add one to include the actual target
        }
    }


}
