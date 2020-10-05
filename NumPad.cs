using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumPad : MonoBehaviour
{
    public Panel myPanel;
    public TextMeshProUGUI display;

    private void Awake()
    {
        // initialize random keywords for player to type
        for (int i = 0; i < myPanel.commands.Length; i++)
        {
            string rand = RandomPinGenerator();
            myPanel.commands[i] = "Enter pin: " + rand;
            myPanel.responses[i] = rand;
        }
    }

    public void EnterNum(int num)
    {
        display.text = display.text + num.ToString();
    }

    public void Clear()
    {
        display.text = "";
    }

    public void EnterPin()
    {
        myPanel.ModifyPanel(display.text);
        Clear();
    }

    string RandomPinGenerator()
    {
        string num = "";

        for (int i = 0; i < 4; i++)
        {
            num += Random.Range(0, 10); 
        }

        return num;
    }
}
