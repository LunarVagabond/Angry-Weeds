using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingAmmo : MonoBehaviour
{
    private float fallDelay = 1f;
    [SerializeField] private float destroyDelay =  30f; // again used for scene cleanup later

    [SerializeField] private Rigidbody2D ammo;

    void Awake() {
        StartCoroutine(Fall());
        StartCoroutine(TimedDelete());
    }

    public IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        ammo.bodyType = RigidbodyType2D.Dynamic;
    }

    
    public IEnumerator TimedDelete()
    {
        yield return new WaitForSeconds(destroyDelay);
        if (gameObject != null)
            Destroy(gameObject);
    }
}
