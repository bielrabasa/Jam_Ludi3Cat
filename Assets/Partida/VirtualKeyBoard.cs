using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualKeyBoard : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().color = FindObjectOfType<ColorsSelect>().get_none();
    }

    public void InputKey()
    {
        FindObjectOfType<keyInput>().CheckLetter(char.Parse(this.name));
    }

    public void ChangeColor()
    {
        ColorsSelect cSelect = FindObjectOfType<ColorsSelect>();

        gameInfo gI = FindObjectOfType<gameInfo>();

        letterState l = gI.letters[gI.getLetterNumber(char.Parse(this.name))];

        //Change key color
        if (l == letterState.CORRECT)
        {
            GetComponent<Image>().color = cSelect.get_correct();
        }
        else if(l == letterState.NOTAPPEARS)
        {
            GetComponent<Image>().color = cSelect.get_notappears();
        }
        else if(l == letterState.POSSIBLE)
        {
            GetComponent<Image>().color = cSelect.get_possible();
        }
        else
        {
            GetComponent<Image>().color = cSelect.get_none();
        }

        //Change key letter color
        GetComponentInChildren<TextMeshProUGUI>().color = cSelect.get_text();
    }

    public void ChangeColorKeyBoard(GameObject g)
    {
        VirtualKeyBoard vKB = g.GetComponent<VirtualKeyBoard>();
        vKB.ChangeColor();
    }


}
