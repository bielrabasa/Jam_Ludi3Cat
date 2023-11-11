using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyInput : MonoBehaviour
{
    gameInfo gameManager;
    TextUIGenerator textUIG;
    public int lookingPos;
    int maxPos;

    GameObject stats;

    void Start()
    {
        gameManager = FindObjectOfType<gameInfo>();
        textUIG = FindObjectOfType<TextUIGenerator>();

        stats = GameObject.Find("Stats");
        stats.SetActive(false);

        lookingPos = -1; //-1 for not focused
        maxPos = gameManager.getState().Length;

        UpdatePos();
        UpdateScreen();
    }

    void Update()
    {
        //Keyboard 
        for (int i = 97; i <= 122; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                CheckLetter((char)i);
            }
        }
    }

    void UpdateScreen()
    {
        //TODO: amb lookingPos, marcar el quadre seleccionat d'alguna manera
        textUIG.UpdateFrase();
        //Debug.Log("Pos: " + lookingPos + "\tLetters left: " + gameManager.getLettersLeft() + "\n" + gameManager.getState());
    }

    public void CheckLetter(char letter)
    {
        if(gameManager.checkLetter(letter, lookingPos))
        {
            CheckVictory(); //POSSIBLE ERROR: vigilar si s'ha guanyat, que després es poden seguir actualitzant coses

            UpdatePos();
            UpdateScreen();
        }
        else
        {
            //TODO: Send a message: "incorrect letter"
            Debug.Log("IncorrectLetter");
        }

        GameObject t = GameObject.Find(letter.ToString());

        t.GetComponent<VirtualKeyBoard>().AddLetterPress();
        t.GetComponent<VirtualKeyBoard>().ChangeColor();
    }

    //Select next possible position
    void UpdatePos()
    {
        int startingPos = lookingPos;

        for(int i = startingPos + 1; i != lookingPos; i++)
        {
            if (i >= maxPos) i = 0;

           
            if (gameManager.positionIsEmpty(i))
            {
                lookingPos = i;
                textUIG.SetPos(i);
                return;
            }
        }

        Debug.LogError("ERROR, no positions found, something went wrong.");
    }

    void CheckVictory()
    {
        if(gameManager.Victory()) Win();
    }

    void Win()
    {
        UpdateScreen();

        stats.SetActive(true);

        int tries = gameManager.numberOfTries;
        int errors = gameManager.numberOfErrors;
        int hints = gameManager.numberOfHints;
        int nleters = gameManager.cleanSolution.Length;

        Text triesText = GameObject.Find("TriesText").GetComponent<Text>();
        Text errorText = GameObject.Find("ErrorText").GetComponent<Text>();
        Text hintsText = GameObject.Find("HintsText").GetComponent<Text>();
        Text accuracityText = GameObject.Find("AccuracityText").GetComponent<Text>();

        triesText.text = tries.ToString();
        errorText.text = errors.ToString();
        hintsText.text = hints.ToString();

        //number of letters in the frase/ number of tries
        float accuracity = (float)5 / tries;

        accuracityText.text = accuracity.ToString("F2") + "%";

        //TODO: fer les coses de quan algú guanya
        Debug.Log("Wiiiin!!!!");
    }
}
