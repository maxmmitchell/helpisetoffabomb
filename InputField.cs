using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class InputField : MonoBehaviour
{
    public Panel myPanel;
    public TMP_InputField input;

    private void Awake()
    {
        // initialize random keywords for player to type
        for (int i = 0; i < myPanel.commands.Length; i++)
        {
            string rand = RandomStringGenerator(UnityEngine.Random.Range(3, 5));
            myPanel.commands[i] = "Type password: " + rand;
            myPanel.responses[i] = rand;
        }
    }
    public void InputText(string output)
    {
        myPanel.ModifyPanel(output.ToUpper());
        // clear text
        input.Select();
        input.text = "";
    }

    string RandomStringGenerator(int length)
    {
        string build = "";
        char letter;

        for (int i = 0; i < length; i++)
        {
            float flt = UnityEngine.Random.Range(0f, 1f);
            int shift = (int)(25 * flt);
            letter = Convert.ToChar(shift + 65);
            build += letter;
        }
        return build;
    }
}
