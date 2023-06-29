using System.Collections;
using UnityEngine;

using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    
    private bool isInvulnerable = false;
    private string MONSTER_TAG = "Enemy";
    [SerializeField] private AudioSource damageTakenSFX;
    [SerializeField] private AudioSource playerDeathSFX;

    public static int health;
    public static int maxHealth = 5;

    public float invulnerabilityDuration = 1.5f;
    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        livesText = GameObject.FindWithTag("LifeText").GetComponent<Text>();
        livesText.text = "Lives: " + maxHealth.ToString();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == MONSTER_TAG || other.gameObject.CompareTag("Bat"))
            DealDamage(1);
    }

    void OnTriggerEnter2D(Collider2D otherObj) {
        if (otherObj.gameObject.tag == MONSTER_TAG || otherObj.gameObject.CompareTag("Bat"))
            DealDamage(1);
    }

    public void DealDamage(int damage)
    {
        if (!isInvulnerable)
        {
            Debug.Log("deal damage called");
            health -= damage;

            // Or else lower we play the death sound 
            if (health >= 1)
            {
                damageTakenSFX.Play();
            }
            isInvulnerable = true;

          

            if (health <= 0)
            {
                playerDeathSFX.Play();
                GameManager.instance.isPaused = true;
                StartCoroutine(gameOver());


                // TODO: Add a death sound here once we figure out how to destroy character
                // Currently the character can't be deleted due to special prefab clone properties (I think)
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

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(playerDeathSFX.clip.length);
        FindObjectOfType<GameManager>().GameOver();
      

    }

   
}
