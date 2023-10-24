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

    [SerializeField] TMP_Text textButton;

    //public ArrayList[] letterArray;

    public void GenerateFrase()
    {
        gameInfo gI;

        gI = FindObjectOfType<gameInfo>();

        //letterArray = new ArrayList[23];

        for (int i = 0; i < gI.getLettersLeft(); i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = canvas.transform;
            button.transform.position = new Vector2(20 + i*30, 200);
            textButton = button.GetComponentInChildren<TMP_Text>();
            //sha de cambiar per els simbols
            textButton.text = i.ToString();
        }
    }

   
}
