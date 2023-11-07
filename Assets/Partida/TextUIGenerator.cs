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
    int aux;
    int j = 0;

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
            button.transform.position = new Vector2(200 + aux*30, 200 - j);
            textButton = button.GetComponentInChildren<Text>();
            textButton.text = textSymbol[i].ToString();
            if (button.transform.position.x >= 1500 && textButton.text == " ")
            {
                j += 60;
                aux = -1;
            }
            listOfLetters[i] = button as GameObject;
            aux++;
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
    public void SetPos(GameObject me)
    {
        keyInput kI;

        kI = FindObjectOfType<keyInput>();


        int i = 0;
        foreach (GameObject o in listOfLetters)
        {
            if(o == me)
            {
                Debug.Log("HOLA");

                kI.lookingPos = i;
            }
            i++;

        }
    }
}
