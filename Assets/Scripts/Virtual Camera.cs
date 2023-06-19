using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;



public class VirtualCamera : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    private Vector3 tempPos;

    private const int MAX = 90;

    [SerializeField]
    private float minxX, maxX;

    private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                target = player.transform;
                vcam.Follow = target;


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        rotateScreen();

    }




    private void rotateScreen()
    {

        if (tempPos.x < minxX)
        {


            tempPos.x = minxX;


        }

        else if (tempPos.x > maxX)
        {
            tempPos.x = maxX;
        }

        transform.position = tempPos;
    }
}
