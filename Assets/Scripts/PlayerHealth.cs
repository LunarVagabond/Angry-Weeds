using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private bool isInvulnerable = false;
    private string MONSTER_TAG = "Enemy";

    
    public int health;
    public int maxHealth = 3;

    public float invulnerabilityDuration = 1.5f;
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        livesText = GameObject.FindWithTag("LifeText").GetComponent<Text>();
        livesText.text = "Lives: " + maxHealth.ToString();
    }
    
    // Runs each frame there is a collision. So if an enemy keeps pushing us we can drain hp
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MONSTER_TAG)
            DealDamage(1);
    }

    public void DealDamage(int damage)
    {
        if (!isInvulnerable)
        {
            Debug.Log("deal damage called");
            health -= damage;
            isInvulnerable = true;
            
            if (health <= 0)
            {
                FindObjectOfType<GameManager>().GameOver();
            }
            else
            {
                StartCoroutine(InvulnerabilityTimer());
            }
            if (livesText != null)
                livesText.text = "Lives: " + health.ToString();

        }
    }

    IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(invulnerabilityDuration);

        // Invulnerability period has ended
        isInvulnerable = false;
    }
}