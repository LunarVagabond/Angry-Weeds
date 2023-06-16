using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingAmmo : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay; // again used for scene cleanup later

    [SerializeField] Player player;

    [SerializeField] private Rigidbody2D ammo;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(Fall());
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.tag == "Player") {
            Destroy(this.gameObject, destroyDelay);
            player.ammoCount += Random.Range(1, 5);
        }
    }

    public IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        ammo.bodyType = RigidbodyType2D.Dynamic;
        // Destroy(gameObject, destroyDelay);
    }
}
