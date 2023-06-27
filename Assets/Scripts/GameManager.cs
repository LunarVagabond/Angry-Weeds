using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting.FullSerializer;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // A static instance of itself inside the Game Manager
    private string GAME_PLAY = "GamePlay";

    public delegate void WaveIncrementEventHandler();
    public static event WaveIncrementEventHandler WaveUpEvent;
    public int CurrentWave;

    public delegate void VictoryAnimationEventHandler();
    public static event VictoryAnimationEventHandler WinEvent;

    [SerializeField] public int maxWaves = 4;
    private int victoryWaitSeconds = 10;

    private bool paused = false;

    [SerializeField] private GameObject[] characters;

    public GameObject victoryText;

    // Can either be 0 which is Player 1 or a 1 which is Player 2 
    private int _characterIndex;
    
    private int checkScore;
    MonsterSpawner thisMS;

    // This is a public Getter/Setter Method 
    public int characterIndex
    {
        get { return _characterIndex; }
        set { _characterIndex = value; }
    }

    public bool isPaused {
        get { return paused; }
        set { paused = value; }
    }

    // If the static GameManger 'instance' is equal to nothing then it will
    // be equal to what ever instance is calling it. It is equal to an instance of THIS class.
    // Awake() is pretty much the constructor for GameManager
    private void Awake()
    {
        CurrentWave = 1;
        if (instance == null)
        {
            instance = this; // We are using the Singleton pattern and can only have one instance
                             // of GameManger to be created.

            DontDestroyOnLoad(gameObject);
            // Don't destroy the GameManger object when loading a new scene. 
        }
        else
        {
            Destroy(gameObject); // If we are trying to create another instance, we destory that instance
            // Destroy the duplicate instance 
        }
    }

    public void UpWave()
    {
        CurrentWave++;

        // VPC 6/27 - call a number of functions when the player has defeated the game 
        // Use a coroutine to do a 5 Second wait to display some test and a victory animation
        if (CurrentWave > maxWaves)
        {
            StartCoroutine(VictorySequence());
        }
        WaveUpEvent?.Invoke();
    }

    IEnumerator VictorySequence()
    {
        victoryText = GameObject.FindGameObjectWithTag("Victory");
        Time.timeScale = 0f; // Stops everything from moving on the screen
        
        // Call a player animation here within the player script use GameManager Event
        //GameManager.WinEvent?.Invoke();
        
        // Display Some text on the screen 
        victoryText.GetComponent<TextMeshProUGUI>().SetText("VICTORY!!!\nYou have defended the Garden!\n" +
            "Press Enter to Return to main menu.");
       
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); //WaitForSecondsRealtime(victoryWaitSeconds);
        Time.timeScale = 1f;
        victoryText.GetComponent<TextMeshProUGUI>().SetText("");
        GameOver(); // here need to return user to main screen
    }


    //GameManger is subscribing to listen to when a scene is changed 
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        //OnLevelFinishedLoading is the method we are calling when
        // the event is triggered 
    }

    //Unsubscribing to prevent memory leakage 
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // Overwriting the delegate method of SceneManager 
    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == GAME_PLAY)
        {

            GameObject player = Instantiate(characters[characterIndex]);
            // player.transform.position = new Vector2(-3, 21);
        }
    }

    public void GameOver()
    {
        // VPC 6/26 - checking the score against playerPrefs for new high score
        thisMS = GameObject.FindGameObjectWithTag("MonsterSpawner").GetComponent<MonsterSpawner>();
        checkScore = thisMS.playerScore;
        if (PlayerPrefs.HasKey("hiScore"))
        {
            if (checkScore > PlayerPrefs.GetInt("hiScore"))
            {
                PlayerPrefs.SetInt("hiScore", checkScore);
                PlayerPrefs.Save();
            }
        }
        //sets initial high score
        else
        {
            PlayerPrefs.SetInt("hiScore", checkScore);
            PlayerPrefs.Save();
        }

        GameManager.instance.isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

   
} // Class 
