using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsSelect : MonoBehaviour
{
    //Background
    Color background = new Color(1, 1, 1);
    Color background_dark = new Color(0, 0, 0);

    //Letters
    Color none = new Color(0.5f, 0.5f, 0.5f, 0.75f);

    Color text = new Color(0f, 0f, 0f, 1f);
    Color text_dark = new Color(1f, 1f, 1f, 1f);

    Color notappears = new Color(0f, 0f, 0f, 1f); //TODO: Other colors maybe?
    Color notappears_dark = new Color(1f, 1f, 1f, 1f);

    Color correct = new Color(0.2745f, 0.8235f, 0.2745f, 1);
    Color correct_dalt = new Color(1f, 0.8549f, 0.149f, 1);

    Color possible = new Color(1f, 0.8549f, 0.149f, 1);
    Color possible_dalt = new Color(0.50588f, 0.8317f, 0.98f, 1);

    Color selected = new Color(0,0,0);
    Color not_selected = new Color(1,1,1);
    Color similar_selected = new Color(0.5f, 0.5f, 0.5f);

    [HideInInspector] public List<GameObject> clickedButtons;
    [HideInInspector] public List<GameObject> notclickedButtons;

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
    public Color get_selected()
    {
        return selected;
    }

    public Color get_not_selected()
    {
        return not_selected;
    }

    public Color get_similar_selected()
    {
        return similar_selected;
    }

    public void DaltonicsOn()
    {
        //Change key color
        VirtualKeyBoard vKB = FindObjectOfType<VirtualKeyBoard>();
        foreach(GameObject obj in clickedButtons)
        {
            vKB.ChangeColorKeyBoard(obj);
        }
    }

    public void DarkOn(GameObject backgrounKB)
    {
        //Change background
        backgrounKB.GetComponent<Image>().color = get_background();

        //Change key color
        VirtualKeyBoard vKB = FindObjectOfType<VirtualKeyBoard>();
        foreach (GameObject obj in clickedButtons)
        {
            vKB.ChangeColorKeyBoard(obj);
        }

        //Change font color
        foreach (GameObject obj in notclickedButtons)
        {
            obj.GetComponentInChildren<Text>().color = get_text();
        }
    }
}
