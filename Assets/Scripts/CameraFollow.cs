using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;

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
    void LateUpdate()
    {
        // If the player doesn't exist, then go to end of the function
        if (!player)
            return;
        

        tempPos = transform.position; // Transform position of the Camera

        tempPos.x = player.position.x; // Make the current position of the camera
        // equal to the current position of the players x position

        if (tempPos.x < minxX)
        {


            // If the player goes off to the left side of the screen
            // The player should traverse to the right side of the screen
           tempPos.x = minxX;

            //tempPos.x = maxX;
        }

        else if(tempPos.x > maxX)
        {
            tempPos.x = maxX;
        }
         
        transform.position = tempPos;

    }
}
