using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyBoard : MonoBehaviour
{
    Color notappears = new Color(0.145f, 0.145f, 0.145f, 0.6078f);
    Color correct = new Color(0.2745f, 0.8235f, 0.2745f, 1);
    Color possible = new Color(0.9215f, 0.6274f, 0, 1);

    public void InputKey()
    {
        FindObjectOfType<keyInput>().CheckLetter(char.Parse(this.name));
        ChangeColor();
    }

    public void ChangeColor()
    {
        gameInfo gI = FindObjectOfType<gameInfo>();

        letterState l = gI.letters[gI.getLetterNumber(char.Parse(this.name))];

        if(l == letterState.CORRECT)
        {
            GetComponent<Image>().color = correct;
        }
        else if(l == letterState.NOTAPPEARS)
        {
            GetComponent<Image>().color = notappears;
        }
        else if(l == letterState.POSSIBLE)
        {
            GetComponent<Image>().color = possible;
        }
    }
}
