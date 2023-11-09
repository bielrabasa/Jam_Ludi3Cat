using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsSelect : MonoBehaviour
{
    //Background
    [SerializeField] Color background = new Color(0, 0, 0);
    [SerializeField] Color background_dark = new Color(1, 1, 1);

    //Letters
    [SerializeField] Color notappears = new Color(0.145f, 0.145f, 0.145f, 0.6078f);
    [SerializeField] Color notappears_dark = new Color(1f, 1f, 1f, 1f);

    [SerializeField] Color correct = new Color(0.2745f, 0.8235f, 0.2745f, 1);
    [SerializeField] Color correct_dalt = new Color(0.2745f, 0.8235f, 0.2745f, 1);

    [SerializeField] Color possible = new Color(1f, 0.8549f, 0.149f, 1);
    [SerializeField] Color possible_dalt = new Color(0.50588f, 0.8317f, 0.98f, 1);

    [SerializeField] Color selected = new Color(1, 1, 1);
    [SerializeField] Color not_selected = new Color(0, 0, 0);
    [SerializeField] Color similar_selected = new Color(0.5f, 0.5f, 0.5f);

    public List<GameObject> clickedButtons;
    public List<GameObject> notclickedButtons;

    //Options
    [SerializeField] Toggle darkmode;
    [SerializeField] Toggle daltonic;

    public Color get_notappears() {
        return darkmode.isOn ? notappears_dark : notappears;
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
        VirtualKeyBoard vKB = FindObjectOfType<VirtualKeyBoard>();

        foreach(GameObject obj in clickedButtons)
        {
            vKB.ChangeColorKeyBoard(obj);
        }
    }

    public void DarkOn(GameObject backgrounKB)
    {
        backgrounKB.GetComponent<Image>().color = get_background();

        foreach (GameObject obj in notclickedButtons)
        {
            obj.GetComponentInChildren<Text>().color = get_notappears();
        }
    }
}
