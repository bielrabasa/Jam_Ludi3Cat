using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyInput : MonoBehaviour
{
    gameInfo gameManager;
    TextUIGenerator textUIG;
    public int lookingPos;
    int maxPos;

    void Start()
    {
        gameManager = FindObjectOfType<gameInfo>();
        textUIG = FindObjectOfType<TextUIGenerator>();

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

        //TODO: fer les coses de quan algú guanya
        Debug.Log("Wiiiin!!!!");
    }
}
