using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class TextUIGenerator : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] GameObject buttonPrefab;
    public GameObject canvas;

    Text textButton;

    GameObject[] listOfLetters;

    float screenWidth;
    float screenHeight;

    public void Start()
    {
        GenerateFrase();
        SetLink();
        SetImage();
    }

    public void GenerateFrase()
    {
        //Get frase to print
        string state = FindObjectOfType<gameInfo>().getState();
        listOfLetters = new GameObject[state.Length];

        //Screen info
        screenWidth = Screen.width;
        screenHeight = Screen.height;
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

    public void DeleteFrase()
    {
        foreach(GameObject go in listOfLetters)
        {
            Destroy(go);
        }
    }

    public void ResetColorKeyBoard()
    {
        GameObject keyboard = GameObject.Find("Teclat Alfabetic");
        VirtualKeyBoard vKB = FindObjectOfType<VirtualKeyBoard>();
        
        for (int i = 0; i < keyboard.transform.childCount; i++)
        {
            vKB.ChangeColorKeyBoard(keyboard.transform.GetChild(i).gameObject);
        }
    }

    public void SetImage()
    {
        Image img = GameObject.Find("Portada").GetComponent<Image>();

        string pL = FindObjectOfType<gameInfo>().news.PhotoLink;

        screenWidth = Screen.width / 2;
        screenHeight = Screen.height / 2;

        img.rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);

        //TODO: get link from newAssets
        StartCoroutine(LoadImage("https://img.ccma.cat/multimedia/jpg/0/1/1697536537110_670.jpg", img));
    }

    IEnumerator LoadImage(string link, Image img)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(link);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log("ERROR");
        }
        else
        {
            Texture2D newTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite newSprite = Sprite.Create(newTexture, new Rect(0,0,newTexture.width, newTexture.height), new Vector2(0.5f,0.5f));

            img.sprite = newSprite;
        }
    }

    public void SetLink()
    {
        string lN = FindObjectOfType<gameInfo>().news.NewsLink;

        Text t = GameObject.Find("Link").GetComponentInChildren<Text>();

        t.text = lN;
    }

    public void OpenLink()
    {
        string lN = FindObjectOfType<gameInfo>().news.NewsLink;
        Application.OpenURL(lN);
    }
}
