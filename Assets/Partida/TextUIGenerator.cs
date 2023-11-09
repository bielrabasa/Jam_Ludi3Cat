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
    float j = 0;

    public void GenerateFrase()
    {
        //Get frase to print
        string state = FindObjectOfType<gameInfo>().getState();
        listOfLetters = new GameObject[state.Length];

        //Screen info
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        Vector2 padding = new Vector2 (screenWidth * 0.1f, screenHeight * 0.9f);
        Vector2 spacing = new Vector2(10f, 20f);

        //Button info
        float letterSize = buttonPrefab.GetComponent<RectTransform>().rect.width; //Width = Height

        //Cursor
        Vector2 cursor = padding;

        for (int i = 0; i < state.Length; i++)
        {
            //Instanciate
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = canvas.transform;

            //Position
            if(cursor.x + letterSize + spacing.x > screenWidth - padding.x) //TODO: Do it with the word, not the next letter
            {
                cursor.y -= letterSize + spacing.y;
                cursor.x = padding.x;
            }
            button.transform.position = cursor;
            cursor.x += letterSize + spacing.x;

            //Set letter
            textButton = button.GetComponentInChildren<Text>();
            textButton.text = state[i].ToString();

            //Add to list
            listOfLetters[i] = button as GameObject;
        }

        //Deactivate spaces
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
