using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsSelect : MonoBehaviour
{
    //Background
    Color background = new Color(1, 1, 1);                          //Keyboard Background lightmode
    Color background_dark = new Color(0, 0, 0);                     //Keyboard Background darkmode

    Color background_game_light = new Color(0.78f, 0.78f, 0.78f);   //Game Background lightmode
    Color background_game_dark = new Color(0.27f, 0.27f, 0.27f);    //Game Background darkmode

    //Letters
    Color none = new Color(0.5f, 0.5f, 0.5f, 0.75f);                //Keyboard non-clicked letters

    Color text = new Color(0f, 0f, 0f, 1f);                         //Keyboard font color
    Color text_dark = new Color(1f, 1f, 1f, 1f);                    //Keyboard font color darkmode

    Color notappears = new Color(0f, 0f, 0f, 1f); //TODO: Other colors maybe?
    Color notappears_dark = new Color(1f, 1f, 1f, 1f);              //Keyboard clicked letter not in frase darkmode

    Color correct = new Color(0.2745f, 0.8235f, 0.2745f, 1);        //Keyboard letter correct
    Color correct_dalt = new Color(1f, 0.8549f, 0.149f, 1);         //Keyboard letter correct daltonic

    Color possible = new Color(1f, 0.8549f, 0.149f, 1);             //Keyboard clicked possible letter
    Color possible_dalt = new Color(0.50588f, 0.8317f, 0.98f, 1);   //Keyboard clicked possible letter daltonic

    Color selected = new Color(0,0,0);                              //Frase letter selected
    Color not_selected = new Color(1,1,1);                          //Frase letter non selected
    Color similar_selected = new Color(0.5f, 0.5f, 0.5f);           //Frase letter same symbol as selected

    Color selected_dark = new Color(1, 1, 1);                       //Frase letter selected darkmode
    Color not_selected_dark = new Color(0, 0, 0);                   //Frase letter non selected darkmode
    Color similar_selected_dark = new Color(0.5f, 0.5f, 0.5f);      //Frase letter same symbol as selected darkmode

    Color text_selected = new Color(0, 0, 0);                       //Frase letter non selected
    Color text_selected_dark = new Color(1f, 1f, 1f);               //Frase letter same symbol as selected

    

    /*[HideInInspector] public List<GameObject> clickedButtons;
    [HideInInspector] public List<GameObject> notclickedButtons;
    [HideInInspector] public List<GameObject> FraseButtons;*/

    //Options
    [SerializeField] Toggle darkmode;
    [SerializeField] Toggle daltonic;

    public Color get_text()
    {
        return darkmode.isOn ? text_dark : text;
    }

    public Color get_none()
    {
        return none;
    }

    public Color get_notappears() {
        return get_background();// darkmode.isOn ? notappears_dark : notappears;
    }
    public Color get_correct()
    {
        return daltonic.isOn ? correct_dalt : correct;
    }
    public Color get_possible()
    {
        return daltonic.isOn ? possible_dalt : possible;
    }
    public Color get_background()
    {
        return darkmode.isOn ? background_dark : background;
    }
    public Color get_game_background()
    {
        return darkmode.isOn ? background_game_dark : background_game_light;
    }
    public Color get_selected()
    {
        return darkmode.isOn ? selected_dark : selected;
    }
    public Color get_text_selected()
    {
        return darkmode.isOn ? text_selected : text_selected_dark;
    }
    public Color get_not_text_selected()
    {
        return darkmode.isOn ? text_selected_dark : text_selected;
    }
    public Color get_not_selected()
    {
        return darkmode.isOn ? not_selected_dark: not_selected;
    }

    public Color get_similar_selected()
    {
        return darkmode.isOn ? similar_selected_dark: similar_selected;
    }

    public void DaltonicsOn()
    {
        //Reset Keyboard Colors
        FindObjectOfType<TextUIGenerator>().ResetColorKeyBoard();
    }

    public void DarkOn(GameObject backgroundKB)
    {
        //Change background
        backgroundKB.GetComponent<Image>().color = get_background();

        //Change key color
        TextUIGenerator ui = FindObjectOfType<TextUIGenerator>();
        ui.ResetColorFrase();
        ui.ResetColorKeyBoard();
        ui.SetPos(FindObjectOfType<keyInput>().lookingPos);

        //Game background
        FindObjectOfType<Camera>().backgroundColor = get_game_background();

        //Reset menu buttons text
        foreach(Text t in GameObject.Find("Menus").GetComponentsInChildren<Text>())
        {
            t.color = get_text();
        }
    }
}
