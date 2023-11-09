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

    private void Start()
    {
        GenerateFrase();
    }

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
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(canvas.transform);

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
            listOfLetters[i] = button;
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
        string textSymbol = FindObjectOfType<gameInfo>().getState();

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
        keyInput kI = FindObjectOfType<keyInput>();

        ColorsSelect cSelect = FindObjectOfType<ColorsSelect>();

        int i = 0;
        foreach (GameObject o in listOfLetters)
        {
            o.GetComponent<Image>().color = cSelect.get_not_selected();

            if (o == me)
            {
                kI.lookingPos = i;

            }

            if(o.GetComponentInChildren<Text>().text == me.GetComponentInChildren<Text>().text)
            {
                o.GetComponent<Image>().color = cSelect.get_similar_selected();
            }


            i++;
        }

        me.GetComponent<Image>().color = cSelect.get_selected();
    }

    public void SetPos(int pos)
    {
        if (pos < 0 || pos >= listOfLetters.Length)
        {
            Debug.Log("ERROR, selecting wrong position!");
            return;
        }
        SetPos(listOfLetters[pos]);
    }
}
