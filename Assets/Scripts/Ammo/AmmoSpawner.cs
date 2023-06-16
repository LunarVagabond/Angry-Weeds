using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{

    public GameObject ammo;
    public int xPos;
    public int yPos = 20;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int ammoCount; // probably used later to clean up the scene

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(AmmoDrop());
    }

    IEnumerator AmmoDrop() {
        while(true){
            xPos = Random.Range(-80, 80);
            GameObject n = Instantiate(ammo, new Vector2(xPos, yPos), Quaternion.identity);
            n.transform.parent = this.transform;
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime + 1)); // max is not included so we add one to include the actual target
        }
    }

}
