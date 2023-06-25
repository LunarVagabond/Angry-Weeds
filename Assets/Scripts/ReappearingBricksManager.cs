using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReappearingBricksManager : MonoBehaviour
{
    // Use a prefab 
    [SerializeField]
    GameObject platform;


    public static ReappearingBricksManager instance = null;

    // The Constructor that creates the instance of this class
    // Through the use of the 'Singleton Pattern'
    void Awake()
    {
        // This only allows for once instance of this class 
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy the new instance 
        }
    }

    // Update is called once per frame
    void Start()
    {
        // Create four brick platforms, in the appropriate positions that we want


        CreateFallingPlatform(new Vector2(71.46f, 5.29f), platform.transform.rotation);
        CreateFallingPlatform(new Vector2(73.22f, 7.4f), platform.transform.rotation);
        CreateFallingPlatform(new Vector2(71.9f, 7.84f), platform.transform.rotation);
        CreateFallingPlatform(new Vector2(75.87f, 6.08f), platform.transform.rotation);
        CreateFallingPlatform(new Vector2(81.13f, 6.08f), platform.transform.rotation);

    }

    void CreateFallingPlatform(Vector2 loc, Quaternion rotaion)
    {
        /*
        Here we are instantiating the platform in the level and adding a private script to it 
        with this the platforms will destory when hitting ground
        */
        GameObject newPlatform = Instantiate(platform, loc, rotaion);
        newPlatform.AddComponent<PrivateCollisionHander>();
    }

    // Internal class acting as a script attached to each falling platform (it's messy but better astetics in game)
    public class PrivateCollisionHander : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the collision is with the specific object you want to trigger the destruction
            if (collision.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject); // Destroy the collided object
            }
        }
    }

    IEnumerator spawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2.5f);
        CreateFallingPlatform(spawnPosition, platform.transform.rotation); // Create the new passed in platform with the appropriate positioning

    }
}
