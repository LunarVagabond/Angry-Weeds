using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Global references 
    public Transform platformNew;
    public Transform startPoint;
    public Transform endPoint;

    public int direction = 1;


    // How fast the platform will move
    public float speed = 1.5f;

    private Vector2 currentMovementTarget()
    {
        if (direction == 1) // 1 Means we are on the right side 
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position; // -1 = Means we are on the left side
        }
    }

    public void Update()
    {
        Vector2 target = currentMovementTarget(); // Get the current position of the target
        // If we are going left the x coordinate will the starting position
        // If we are going right the x coordinate will be the ending position 

        // Gives us the distance from the current position of the platform and the targeted ending or start point
        // We then factor in the speed of the moving platform 
        platformNew.position = Vector2.Lerp(platformNew.position, target, speed * Time.deltaTime);


        // Targetted ending or startpoint minus the current position 
        float distance = (target - (Vector2)platformNew.position).magnitude;

        // If the distance is less than .1 we want to make the direction nevagtive (the left side/starting point)
        
        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

   

    public void OnDrawGizmos()
    {
        // Draws line between points/just for debugging purposes
        if (platformNew != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platformNew.transform.position, startPoint.position);
            Gizmos.DrawLine(platformNew.transform.position, endPoint.position);

        }
    }

    // Allow the Player object colliding or the enemy colliding to become a child of the parent class
    // Therefor that object will follow the transform coordinates 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
         
            
        
    }

    // Allow the player or enemy to get off the platform, and release that object as a child of the parent class 'Moving Platform'
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);

    }
}
