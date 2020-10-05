using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    // these two must line up
    public string[] commands;
    public string[] responses;

    // for all panels, must define these
    public struct Scenario
    {
        // print to command prompt
        public string command;
        // expect as key/answer
        public string response;
    };

    public Scenario[] scenarios;

    CommandPrompt CP;

    private void Start()
    {
        CP = FindObjectOfType<CommandPrompt>();

        // initialize array
        scenarios = new Scenario[commands.Length];
        for (int i = 0; i < scenarios.Length; i++)
        {
            scenarios[i] = MakeScenario(commands[i], responses[i]);
        }
    }

    public void ModifyPanel(string output)
    {
        if (CP.running)
        {
            CP.ResolveCommand(output);
        }
    }

    Scenario MakeScenario(string c, string r)
    {
        Scenario temp = new Scenario();
        temp.command = c;
        temp.response = r;

        return temp;
    }

}
