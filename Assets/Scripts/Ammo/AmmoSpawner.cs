using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{

    public GameObject[] ammoTypes; // ammo types
    public GameObject[] gunTypes; // VPC - in case we want future expansion
    public int xPos;
    public int yPos = 20;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int ammoCount; // probably used later to clean up the scene
    public Vector2 gunVector;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(AmmoDrop());
        StartCoroutine(PotatoGunDrop());
    }

    IEnumerator AmmoDrop() {
        while(true){
            xPos = Random.Range(-80, 80);
            int ammoToSpawn = Random.Range(0,ammoTypes.Length - 1);
            GameObject n = Instantiate(ammoTypes[ammoToSpawn], new Vector2(xPos, yPos), Quaternion.identity);
            n.transform.parent = this.transform;
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime + 1)); // max is not included so we add one to include the actual target
        }
    }

    IEnumerator PotatoGunDrop(){
       
        gunVector.Set(Random.Range(-90, 90), yPos) ;
        int gunToSpawn = Random.Range(0, gunTypes.Length - 1);
        GameObject Gun = Instantiate(gunTypes[gunToSpawn], gunVector, Quaternion.identity);
        Gun.transform.parent = this.transform;
        yield return new WaitForSeconds(2);
    }

}
