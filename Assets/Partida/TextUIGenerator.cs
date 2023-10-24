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

    [SerializeField] Text textButton;

    //public ArrayList[] letterArray;

    public void GenerateFrase()
    {
        gameInfo gI;

        gI = FindObjectOfType<gameInfo>();

        string textSymbol = gI.getState();

        for (int i = 0; i < gI.cleanSolution.Length; i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = canvas.transform;
            button.transform.position = new Vector2(20 + i*30, 200);
            textButton = button.GetComponentInChildren<Text>();
            //sha de cambiar per els simbols
            textButton.text = textSymbol[i].ToString();
        }
    }

   
}
