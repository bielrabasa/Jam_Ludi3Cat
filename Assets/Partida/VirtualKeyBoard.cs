using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyBoard : MonoBehaviour
{

    public void InputKey()
    {
        FindObjectOfType<keyInput>().CheckLetter(char.Parse(this.name));
    }

    public void AddLetterPress()
    {
        //Sumar 1 a intents
        ColorsSelect cSelect = FindObjectOfType<ColorsSelect>();
        cSelect.clickedButtons.Add(this.gameObject);

        gameInfo gI = FindObjectOfType<gameInfo>();

        letterState l = gI.letters[gI.getLetterNumber(char.Parse(this.name))];

        if (l == letterState.CORRECT || l == letterState.POSSIBLE)
        {
            gI.Statistics(false);

        }
        else if (l == letterState.NOTAPPEARS)
        {
            gI.Statistics(true);
        }


    }

    public void ChangeColor()
    {
        ColorsSelect cSelect = FindObjectOfType<ColorsSelect>();

        gameInfo gI = FindObjectOfType<gameInfo>();

        letterState l = gI.letters[gI.getLetterNumber(char.Parse(this.name))];

        if (l == letterState.CORRECT)
        {
            GetComponent<Image>().color = cSelect.correct;

        }
        else if(l == letterState.NOTAPPEARS)
        {
            GetComponent<Image>().color = cSelect.notappears;
        }
        else if(l == letterState.POSSIBLE)
        {
            GetComponent<Image>().color = cSelect.possible;
        }
    }

    public void ChangeColorKeyBoard(GameObject g)
    {
        VirtualKeyBoard vKB = g.GetComponent<VirtualKeyBoard>();
        vKB.ChangeColor();
    }


}
