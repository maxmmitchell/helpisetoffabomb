using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredButtons : MonoBehaviour
{
    public Panel myPanel;
    public void PressButton(string output)
    {
        myPanel.ModifyPanel(output);
    }

}
