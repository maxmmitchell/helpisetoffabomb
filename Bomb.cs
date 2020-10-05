using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bomb : MonoBehaviour
{
    // bomb counts down and updates gui
    // if ever reaches 0, explodes

    public float timeLeft = 30f; // in seconds
    public TextMeshProUGUI timer;

    public bool exploded = false;
    public bool defused = false;

    private void Start()
    {
        timer.text = TimeToString(timeLeft);
    }

    void Update()
    {
        if (!exploded && !defused)
        {
            timeLeft -= Time.deltaTime;
            timer.text = TimeToString(timeLeft);
            if (timeLeft <= 0)
            {
                timer.text = "0:00.00";
                exploded = true;
                // tell commandprompt we blew up
                FindObjectOfType<CommandPrompt>().running = false;
            }
        }
    }

    string TimeToString(float time)
    {
        string timeString = "";

        int minutes = (int)timeLeft / 60;
        // the floor rounds it to the hundredths place
        float seconds = Mathf.Floor((timeLeft % 60) * 100) / 100;
        // if statement added to account for extra 0
        timeString = minutes.ToString() + ":" + (seconds < 10 ? "0" : "") + seconds.ToString();

        return timeString;
    }
}
