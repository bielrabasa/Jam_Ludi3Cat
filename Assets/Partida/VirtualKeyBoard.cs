using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualKeyBoard : MonoBehaviour
{
    Color notappears = new Color(37,37,37,155);
    Color correct = new Color(70,210,70,255);
    Color possible = new Color(235,160,0,255);

    public void InputKey()
    {
        FindObjectOfType<keyInput>().CheckLetter(char.Parse(this.name));
    }

    public void ChangeColor()
    {
        gameInfo gI = FindObjectOfType<gameInfo>();

        letterState l = gI.letters[gI.getLetterNumber(char.Parse(this.name))];

        if(l == letterState.CORRECT)
        {

        }
        else if(l == letterState.NOTAPPEARS)
        {

        }
        else if(l == letterState.POSSIBLE)
        {

        }
    }
}
