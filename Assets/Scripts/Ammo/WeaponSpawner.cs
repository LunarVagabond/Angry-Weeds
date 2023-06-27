using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner: MonoBehaviour
{

    public GameObject[] gunTypes; // VPC - in case we want future expansion
    public int minX;
    public int maxX;
    public int yPos = 20;
    public Vector2 gunVector;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(PotatoGunDrop());
    }

    IEnumerator PotatoGunDrop()
    {

        gunVector.Set(Random.Range(minX, maxX), yPos);
        int gunToSpawn = Random.Range(0, gunTypes.Length - 1);
        GameObject Gun = Instantiate(gunTypes[gunToSpawn], gunVector, Quaternion.identity);
        Gun.transform.parent = this.transform;
        yield return new WaitForSeconds(2);
    }

}
