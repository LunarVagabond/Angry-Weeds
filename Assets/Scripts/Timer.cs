using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    private float timeDuration = 3f * 60; // 3 Seconds but we make it display in seconds

    private float timer; // Keeps track of the time

    private float flashDuration = 1f;

    private float flashTimer;

    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI seperator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;


    // Start is called before the first frame update
    void Start()
    {
        ResetTimer(); // Set the inital timer 
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 30)
        {
            timer -= Time.deltaTime; // Every frame or update we substract the time that has lapsed
            UpdateTimerDisplay(timer); // Displays updated time
        }
        else
        {
            Flash();
        }
    }

    private void ResetTimer()
    {
        timer = timeDuration;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60); // Retrieve the minutes 
        float seconds = Mathf.FloorToInt(time % 60); // Get what is left over which will be the seconds

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds); // A function that displays our current time in the right display

        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString(); // Convert the char into a String
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();

    }

    private void Flash()
    {
        timer -= Time.deltaTime; // Every frame or update we substract the time that has lapsed

        if (timer <= 0) // This means the number has to be negative so we reset it back to 0 and update it. 
        {
            timer = 0;
        }

        if (flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }
        else if (flashTimer >= flashDuration / 2) 
        {
            flashTimer -= Time.deltaTime;
            setTextDisplay(false);
        }
        else
        {        
            flashTimer -= Time.deltaTime;  
            setTextDisplay(true);
        }

        UpdateTimerDisplay(timer);

    }

    private void setTextDisplay(bool enabled)
    {
        firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        seperator.enabled= enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;


    }
}
