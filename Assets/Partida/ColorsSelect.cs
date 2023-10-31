using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsSelect : MonoBehaviour
{
    public Color notappears = new Color(0.145f, 0.145f, 0.145f, 0.6078f);
    public Color correct = new Color(0.2745f, 0.8235f, 0.2745f, 1);
    public Color possible = new Color(0.9215f, 0.6274f, 0, 1);

    public List<GameObject> clickedButtons;

    public void DaltonicsOn()
    {
        Toggle toggle = this.GetComponentInParent<Toggle>();

        if (toggle.isOn)
        {
            notappears = new Color(0.145f, 0.145f, 0.145f, 0.6078f);
            correct = new Color(1f, 0.8549f, 0.149f, 1);
            possible = new Color(0.50588f, 0.8317f, 0.98f, 1);
        }
        else
        {
            notappears = new Color(0.145f, 0.145f, 0.145f, 0.6078f);
            correct = new Color(0.2745f, 0.8235f, 0.2745f, 1);
            possible = new Color(0.9215f, 0.6274f, 0, 1);
        }

        VirtualKeyBoard vKB = FindObjectOfType<VirtualKeyBoard>();

        foreach(GameObject obj in clickedButtons)
        {
            vKB.ChangeColorKeyBoard(obj);

        }
    }
}
