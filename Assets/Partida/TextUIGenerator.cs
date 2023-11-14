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

    TextMeshProUGUI textButton;
    [SerializeField]TextMeshProUGUI link;

    public GameObject[] listOfLetters;

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

        //Button info
        Vector2 letterSize = new Vector2(buttonPrefab.GetComponent<RectTransform>().rect.width, buttonPrefab.GetComponent<RectTransform>().rect.height); //Width = Height

        //Screen info
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        Vector2 spacing = new Vector2(letterSize.x/5f, letterSize.y/3f);

        //
        int lettersPerRow = (int)((screenWidth - (screenWidth * 0.1f * 2)) / (letterSize.x + spacing.x));

        //Cursor
        Vector2 cursor = new Vector2(screenWidth * 0.1f, screenHeight * 0.9f);

        string[] frase = state.Split(' ');
        int realSolutionPosition = 0;

        for (int i = 0; i < frase.Length; i++)
        {
            //Check all printable words in the row
            string currentPrinting = frase[i];
            while (i < frase.Length - 1 && //Check last word
                currentPrinting.Length + 1 + frase[i+1].Length <= lettersPerRow) //Check that the next word fits
            {
                currentPrinting += ' ' + frase[i + 1];
                i++;
            }
            
            //Check size to center TODO: Not Perfect
            float letterOccupation = (currentPrinting.Length * letterSize.x) + ((currentPrinting.Length - 1f) * spacing.x);
            cursor.x = (screenWidth - letterOccupation) / 2f;

            //Add a space if it's not the las word
            if (i < frase.Length - 1) currentPrinting += ' ';

            //Print frase
            for (int l = 0; l < currentPrinting.Length; l++)
            {
                //Instantiate
                GameObject button = Instantiate(buttonPrefab);
                button.transform.SetParent(canvas.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text =  currentPrinting[l].ToString();

                //Position
                button.transform.position = cursor;
                cursor.x += letterSize.x + spacing.x;

                //Add to list
                listOfLetters[realSolutionPosition + l] = button;
            }

            //Line jump
            cursor.y -= letterSize.y + spacing.y;
            realSolutionPosition += currentPrinting.Length;
        }

        //Deactivate spaces
        foreach (GameObject o in listOfLetters)
        {
            textButton = o.GetComponentInChildren<TextMeshProUGUI>();

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
            textButton = o.GetComponentInChildren<TextMeshProUGUI>();
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
            o.GetComponentInChildren<TextMeshProUGUI>().color = cSelect.get_not_text_selected();

            if (o == me)
            {
                kI.lookingPos = i;

            }

            if(o.GetComponentInChildren<TextMeshProUGUI>().text == me.GetComponentInChildren<TextMeshProUGUI>().text)
            {
                o.GetComponent<Image>().color = cSelect.get_similar_selected();
            }


            i++;
        }

        me.GetComponent<Image>().color = cSelect.get_selected();
        me.GetComponentInChildren<TextMeshProUGUI>().color = cSelect.get_text_selected();
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

    public void ResetColorFrase()
    {
        ColorsSelect cSelect = FindObjectOfType<ColorsSelect>();

        //Change Frase color
        foreach (GameObject obj in listOfLetters)
        {
            obj.GetComponent<Image>().color = cSelect.get_background();
            obj.GetComponentInChildren<TextMeshProUGUI>().color = cSelect.get_text();
        }
    }

    public void SetImage()
    {
        Image img = GameObject.Find("Portada").GetComponent<Image>();
        TextMeshProUGUI date = GameObject.Find("DateNews").GetComponent<TextMeshProUGUI>();

        NewsAsset n = FindObjectOfType<gameInfo>().news;
        Sprite photo = Resources.Load<Sprite>("fotos/" + n.NumNews);

        //Set photo scale & position
        float ratio = photo.textureRect.width / photo.textureRect.height;
        float photoSize = 275;
        img.rectTransform.sizeDelta = new Vector2(photoSize * ratio, photoSize);
        img.rectTransform.localPosition = new Vector3(0, -160, 0);
        img.sprite = photo;

        //Set date position
        date.text = n.Date;
        date.transform.localPosition = img.transform.localPosition + new Vector3(0, photoSize/2 + 20, 0);
        
        //TODO: get link from newAssets
        //StartCoroutine(LoadImage("https://img.ccma.cat/multimedia/jpg/0/1/1697536537110_670.jpg", img));
    }

    /*IEnumerator LoadImage(string link, Image img)
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
    }*/
    /*
    public void ToggleDarkMode()
    {
        ColorsSelect cSelect = FindObjectOfType<ColorsSelect>();
        
        foreach (GameObject i in listOfLetters)
        {
            cSelect.FraseButtons.Add(i.gameObject);
        }


    }
    */    
    public void SetLink()
    {
        string l = "Enllaç a la notícia:\n";
        l += FindObjectOfType<gameInfo>().news.NewsLink;
        link.text = l;
    }

    public void OpenLink()
    {
        string lN = FindObjectOfType<gameInfo>().news.NewsLink;
        Application.OpenURL(lN);
    }
}
