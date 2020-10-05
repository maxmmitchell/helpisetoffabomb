using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    // on button click, go to stage
    public void GoToStage()
    {
        FindObjectOfType<SceneLoader>().LoadNextScene("Stage");
    }
}
