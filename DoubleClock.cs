using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoubleClock : MonoBehaviour
{
    public TextMeshProUGUI adjustedTime;
    public TextMeshProUGUI timeOfDeath;

    private void Awake()
    {
        int hour;
        int minute;
        // check if this is a new game or a new level
        if (PlayerPrefs.GetInt("Stage", 0) != 0)
        {
            hour = PlayerPrefs.GetInt("TODhour", 0);
            minute = PlayerPrefs.GetInt("TODhour", 0);
        }
        else
        {
            // new game, so clocks must be set
            hour = System.DateTime.Now.Hour;
            minute = System.DateTime.Now.Minute - 4;
            PlayerPrefs.SetInt("TODhour", hour);
            PlayerPrefs.SetInt("TODminute", minute);
        }

        // first, time of death
        timeOfDeath.text = hour.ToString() + ":" + (minute < 10 ? "0" : "") + minute.ToString();
        // now set adjusted time for right now
        adjustedTime.text = hour.ToString() + ":" + (minute < 10 ? "0" : "") + (minute + 4 - PlayerPrefs.GetInt("Stage", 0)).ToString();
    }
}
