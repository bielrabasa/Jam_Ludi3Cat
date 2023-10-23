using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIGenerator : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] GameObject buttonPrefab;
    public GameObject canvas;
    gameInfo le = FindObjectOfType<gameInfo>();

    public ArrayList[] letterArray;

    public void GenerateFrase()
    {
        letterArray = new ArrayList[23];

        for (int i = 0; i < letterArray.Length; i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.parent = canvas.transform;
            button.transform.position = new Vector2(20 + i*30, 200);
        }
    }

   
}
