using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    Rigidbody2D rigidBody;

    private bool isInvulnerable = false;
    public float invulnerabilityDuration = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (!isInvulnerable)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ReappearingBricksManager.instance.StartCoroutine("spawnPlatform", new Vector2(transform.position.x, transform.position.y));


                isInvulnerable = true;

                // We create an instance of BricksManager and call
                // the spawnPlatform method which will take two seconds 

                // Wait 0.5f seconds and then drop the platform
                // The player has .5 seconds to stand on the platform
                // before it falls 
                Invoke("dropPlatform", 0.5f);

                // Destroy(gameObject, 2f); // Destroy the platform after 2 seconds


            }
        }
    }

    private void dropPlatform()
    {
        rigidBody.isKinematic = false; // Drops the platform 
    }

    IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(invulnerabilityDuration);

        // Invulnerability period has ended
        isInvulnerable = false;
    }
}
