using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // A static instance of itself inside the Game Manager
    private string GAME_PLAY = "GamePlay";

    [SerializeField]
    private GameObject[] characters;

    // Can either be 0 which is Player 1 or a 1 which is Player 2 
    private int _characterIndex;

    // This is a public Getter/Setter Method 
    public int characterIndex
    {
        get { return _characterIndex; }
        set { _characterIndex = value; }
    }

    // If the static GameManger 'instance' is equal to nothing then it will
    // be equal to what ever instance is calling it. It is equal to an instance of THIS class.
    // Awake() is pretty much the constructor for GameManager
    private void Awake()
    {
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
        if(scene.name == GAME_PLAY)
        {
           
            Instantiate(characters[characterIndex]);
        }
    }
} // Class 
