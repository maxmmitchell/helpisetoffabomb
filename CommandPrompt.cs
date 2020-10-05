using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CommandPrompt : MonoBehaviour
{
    // the wait time between producing commands
    public float waitTime = 2f;
    // how many commands
    public int commandsLeft = 7;
    // controls flow of commands
    public bool running = true;
    bool runningPrev = true;
    // default text to copy and instantiate
    public TextMeshProUGUI defaultCommand;
    // marks stage progress
    public TextMeshProUGUI travelDistance;
    // for restarting
    public GameObject deathScreen;
    // for winning
    public GameObject winScreen;

    struct Command
    {
        // printed to screen
        public TextMeshProUGUI text;
        // answer to resolve command
        public string key;
    };

    // max length allowed of queue. if queue is too long,
    // kill player.
    int MAX_LENGTH = 5;
    // as commands are generated, add to queue
    Queue<Command> commands = new Queue<Command>();
    // all panels on board
    Panel[] panels;

    private void Start()
    {
        // turn off default command
        defaultCommand.gameObject.SetActive(false);
        // find all panels on board
        panels = FindObjectsOfType<Panel>();
        // set commandprompt in motion
        StartCoroutine(WriteCommands());

        // account for stage we are on
        int stage = PlayerPrefs.GetInt("Stage", 0);
        commandsLeft += stage;
        travelDistance.text = "Travelled " + 60 * stage + " seconds \n into the past.";
    }

    private void Update()
    {
        if (!running && runningPrev)
        {
            EndTextSelector();
        }
        // what running was last frame
        runningPrev = running;
    }


    // periodically prints commands
    // generate command, add to queue
    // command is struct with text object and string key
    // if queue is too long, kill player
    // move all child text up (on screen) after each new command is added
    // when command is done change text color to green from red
    IEnumerator WriteCommands()
    {
        yield return new WaitForSeconds(0.5f);
        // stops when player fails commands, when bomb goes off or when player finishes commands
        while (running)
        {
            if (commandsLeft > 0)
            {
                PushCommands();

                commandsLeft--;
                GenerateCommand();
            }

            if (commands.Count > MAX_LENGTH)
            {
                running = false;
            }

            // wait to send next command
            yield return new WaitForSeconds(waitTime);
        }
    }

    void GenerateCommand()
    {
        // pick panel randomly
        Panel aux = panels[Random.Range(0, panels.Length)];

        // access panel's command list
        // pick random command
        Panel.Scenario temp = aux.scenarios[Random.Range(0, aux.scenarios.Length)];

        // create Command object based on found command and key
        Command nextCom = new Command();
        GameObject newComTextObject = Instantiate(defaultCommand.gameObject, transform);
        nextCom.text = newComTextObject.GetComponent<TextMeshProUGUI>();
        nextCom.text.text = temp.command;
        nextCom.key = temp.response;
        // add Command object to queue
        commands.Enqueue(nextCom);
        // print command
        nextCom.text.gameObject.SetActive(true);
    }

    void PushCommands()
    {
        // moves all commands up by set amount
        // if off command image, despawn
        foreach (TextMeshProUGUI t in GetComponentsInChildren<TextMeshProUGUI>())
        {
            Vector2 pos = t.GetComponent<RectTransform>().position;
            t.GetComponent<RectTransform>().position = new Vector2(pos.x, pos.y + 32);
            // height of command prompt
            if (pos.y > 320)
            {
                Destroy(t.gameObject);
            }
        }
    }

    public void ResolveCommand(string answer)
    {
        Command pop = new Command();
        // dequeue from queue
        if (commands.Count != 0)
        {
            pop = commands.Dequeue();
        }

        // if answer matches key, we have resolved this command
        if (pop.key == answer)
        {
            // turn text green
            pop.text.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            // else, kill player for failing command
            running = false;
        }

        // if queue is empty and all commands are printed, we win!
        if (commands.Count == 0 && commandsLeft == 0)
        {
            FindObjectOfType<Bomb>().defused = true;
            running = false;
        }
    }

    void EndTextSelector()
    {
        PushCommands();
        // print "Terminated commands. " + "Success! Time warping now." or "Failure. Input too slow" or "Death. The bomb blew up."
        GameObject endPrompt = Instantiate(defaultCommand.gameObject, transform);
        string endMessage = "";
        // check why we stopped
        if (commandsLeft == 0 && commands.Count == 0)
        {
            // we won!
            endMessage = "Time warping now. . .";
            StartCoroutine(TimeWarp());
        }
        else
        {
            // we lose
            if (FindObjectOfType<Bomb>().exploded)
            {
                // we ran out of time
                endMessage = "TIMES UP!";
            }
            else if (commands.Count > MAX_LENGTH)
            {
                // we didnt keep up with the commands
                endMessage = "COMMAND OVERFLOW!";
                FindObjectOfType<Bomb>().timeLeft = 0;
            }
            else
            {
                // we input the wrong command
                endMessage = "WRONG COMMAND!";
                FindObjectOfType<Bomb>().timeLeft = 0;
            }
            StartCoroutine(DeathScreenActive());
        }
        
        endPrompt.GetComponent<TextMeshProUGUI>().text = endMessage;
        endPrompt.GetComponent<TextMeshProUGUI>().color = new Color32(255, 0 ,255, 255); 
        endPrompt.SetActive(true);
    }

    IEnumerator DeathScreenActive()
    {
        yield return new WaitForSeconds(2f);
        // allow player to restart
        deathScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        deathScreen.GetComponent<Animator>().SetTrigger("death");
    }

    IEnumerator TimeWarp()
    {
        yield return new WaitForSeconds(4f);
        int currStage = PlayerPrefs.GetInt("Stage", 0);

        if (currStage == 3)
        {
            // game won!
            AudioManager.instance.Stop(AudioManager.instance.musicPlaying);
            AudioManager.instance.Play("MusicRevival");
            winScreen.SetActive(true);
            winScreen.GetComponent<Animator>().SetTrigger("win");
        }
        else
        {
            // onto next stage
            // increment the stage we are on
            PlayerPrefs.SetInt("Stage", currStage + 1);
            FindObjectOfType<SceneLoader>().LoadNextScene("Stage");
        }
        
    }

    public void Restart()
    {
        // new game
        PlayerPrefs.SetInt("Stage", 0);
        FindObjectOfType<SceneLoader>().LoadNextScene("Stage");
    }

    public void ToMenu()
    {
        // back to menu
        PlayerPrefs.SetInt("Stage", 0);
        FindObjectOfType<SceneLoader>().LoadNextScene("MainMenu");
    }

}
