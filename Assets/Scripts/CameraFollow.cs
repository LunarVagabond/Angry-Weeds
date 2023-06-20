using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
   

    [SerializeField]
    private float minxX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        // Gets access to the transform attributes on the Player
        player = GameObject.FindWithTag("Player").transform;

        Debug.Log("The selected index: " + GameManager.instance.characterIndex);
    }

    // Update is called once per frame
    // Is being called after all calculations in Update are finished.
    void Update()
    {
        // If the player doesn't exist, then go to end of the function
        if (!player)
            return;

   

    }



}
