using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUIGenerator : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] GameObject buttonPrefab;
    public GameObject canvas;

    Text textButton;

    GameObject[] listOfLetters;

    public void GenerateFrase()
    {
        gameInfo gI;

        gI = FindObjectOfType<gameInfo>();

        string textSymbol = gI.getState();

        listOfLetters = new GameObject[gI.cleanSolution.Length];

        for (int i = 0; i < gI.cleanSolution.Length; i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = canvas.transform;
            button.transform.position = new Vector2(20 + i*30, 200);
            textButton = button.GetComponentInChildren<Text>();
            textButton.text = textSymbol[i].ToString();
            Button btn = button.GetComponent<Button>();
            btn.GetComponent<Button>().onClick.AddListener(() => { SetPos(); });
            listOfLetters[i] = button as GameObject;
        }


        foreach (GameObject o in listOfLetters)
        {
            textButton = o.GetComponentInChildren<Text>();

            if (textButton.text == " ")
            {
                o.SetActive(false);
            }
        }
    }

    public void UpdateFrase()
    {
        gameInfo gI;

        gI = FindObjectOfType<gameInfo>();

        string textSymbol = gI.getState();

        int i = 0;

        foreach(GameObject o in listOfLetters)
        {
            textButton = o.GetComponentInChildren<Text>();
            textButton.text = textSymbol[i].ToString();
            i++;
        }
    }
    void SetPos()
    {
        keyInput kI;

        kI = FindObjectOfType<keyInput>();

        int i = 0;
        foreach (GameObject o in listOfLetters)
        {
            i++;
            if(o)
            {
                kI.lookingPos = i;
            }
        }
    }
}
