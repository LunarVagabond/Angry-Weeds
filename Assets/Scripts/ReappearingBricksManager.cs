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
        else if(instance != this)
        {
            Destroy(gameObject); // Destroy the new instance 
        }
    }

    // Update is called once per frame
    void Start()
    {
        // Create four brick platforms, in the appropriate positions that we want

        
        Instantiate(platform, new Vector2(71.46f, 5.29f), platform.transform.rotation);

        Instantiate(platform, new Vector2(73.22f, 7.4f), platform.transform.rotation);
        Instantiate(platform, new Vector2(75.673f, 9.66f), platform.transform.rotation);
        Instantiate(platform, new Vector2(80.31f, 9.71f), platform.transform.rotation);

    }

    IEnumerator  spawnPlatform(Vector2 spawnPosition)
    {
       
            yield return new WaitForSeconds(2f);
            Instantiate(platform, spawnPosition, platform.transform.rotation); // Create the new passed in platform with the appropriate positioning

     
    }
}
