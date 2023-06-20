using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] MonsterSpawner spawner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            Transform eT = enemy.transform;
            enemy.transform.localScale = new Vector2(eT.transform.localScale.x * -1, eT.transform.localScale.y);
            enemy.GetComponent<Monster>().speed *= -1;
            // Destroy(collision.gameObject);
            // spawner.spawnedEnemies.Remove(collision.gameObject);

        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
