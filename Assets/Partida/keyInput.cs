using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keyInput : MonoBehaviour
{
    gameInfo gameManager;
    TextUIGenerator textUIG;
    public int lookingPos;
    int maxPos;

    GameObject stats;

    GameObject howToPlay;

    void Start()
    {
        gameManager = FindObjectOfType<gameInfo>();
        textUIG = FindObjectOfType<TextUIGenerator>();

        howToPlay = GameObject.Find("HowToPlay");

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
        if(!howToPlay.activeInHierarchy)
        {
            for (int i = 97; i <= 122; i++)
            {
                if (Input.GetKeyDown((KeyCode)i))
                {
                    CheckLetter((char)i);
                }
            }
        }

    }

    void UpdateScreen()
    {
        textUIG.UpdateFrase();
    }

    public void CheckLetter(char letter)
    {
        if(gameManager.checkLetter(letter, lookingPos))
        {
            CheckVictory(); //POSSIBLE ERROR: vigilar si s'ha guanyat, que després es poden seguir actualitzant coses

            UpdatePos();
            UpdateScreen();
        }

        GameObject t = GameObject.Find(letter.ToString());

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

        //Debug.LogError("ERROR, no positions found, something went wrong.");
    }

    public void Hint()
    {
        gameManager.Hint();

        textUIG.UpdateFrase();
        textUIG.ResetColorKeyBoard();

        CheckVictory();
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
        float nleters = gameManager.numberLetters;

        TextMeshProUGUI triesText = GameObject.Find("TriesText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI errorText = GameObject.Find("ErrorText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI hintsText = GameObject.Find("HintsText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI accuracityText = GameObject.Find("AccuracityText").GetComponent<TextMeshProUGUI>();

        triesText.text = tries.ToString();
        errorText.text = errors.ToString();
        hintsText.text = hints.ToString();

        float accuracity = 0;
        if(tries != 0)
        {
            accuracity = (nleters / (tries+hints)) * 100; //TODO: With hints, not well calculated
        }

        accuracityText.text = accuracity.ToString("F0") + "%";
    }
}
