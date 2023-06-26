using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HighScore : MonoBehaviour
{
    public int displayScore;
    [SerializeField] public Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        displayScore  = PlayerPrefs.GetInt("hiScore");
        updateHighScoreText(displayScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void updateHighScoreText(int score) => highScoreText.text = "Current High Score: " + score.ToString();
}
