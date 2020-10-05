using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePressure : MonoBehaviour
{
    // as time pressure decreases, filter on screen increases
    // press pump button many times to increase pressure
    float pressure = 400;
    float MAX_PRESSURE = 500;
    public Slider fill;
    public Image filter;
    CommandPrompt CP;

    private void Start()
    {
        CP = FindObjectOfType<CommandPrompt>();
        StartCoroutine(LeakPressure());
    }

    private void Update()
    {
        // filter on screen makes it hard to see
        Color temp = filter.color;
        filter.color = new Color(temp.r, temp.g, temp.b, (MAX_PRESSURE - pressure) / MAX_PRESSURE);
    }

    public void Pump()
    {
        if (pressure < MAX_PRESSURE)
        {
            pressure += 25;
            fill.value = pressure / MAX_PRESSURE;
        }
    }

    IEnumerator LeakPressure()
    {
        while (pressure > 0 && CP.running)
        {
            pressure--;
            fill.value = pressure / MAX_PRESSURE;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
