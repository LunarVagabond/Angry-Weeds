using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPotatoGun : MonoBehaviour
{

    private float fallDelay = 1f;
    [SerializeField] private Rigidbody2D gun;
   
    void Awake()
    {
        StartCoroutine(FallGun());
    }

    public IEnumerator FallGun()
    {
        yield return new WaitForSeconds(fallDelay);
    }

}
