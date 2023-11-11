using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum letterState
{
    NONE,
    CORRECT,
    POSSIBLE,
    NOTAPPEARS
}

public class gameInfo : MonoBehaviour
{
    [HideInInspector] public const int NUM_LETTERS = 26;
    const string SYMBOLS = "★✤✿♠♣♥♦☁☀▶∎☻◈〓✚⦿⊕☗✈◐☂♜♞♚♛☎";

    [SerializeField] string solution;        //Hóla qüe, tAL?
    public string cleanSolution;   //hola que, tal?
    string shuffledSymbols;
    
    public letterState[] letters;
    public int numberOfTries;
    public int numberOfErrors;
    public int numberOfHints;
    public float numberLetters;
    public NewsAsset news;

    private void Awake()
    {
        CreateGame();
    }

    /*private void Start()
    { 
        Debug.Log("Symbols: " + shuffledSymbols);
        Debug.Log("Solution: " + solution);
        Debug.Log("Clean Solution: " + cleanSolution);
        Debug.Log("Current board: " + getState());
        Debug.Log("Letters left: " + getLettersLeft());
    }*/

    //-------------------------------------------------------------------------------------------------------------
    //
    // GAME CREATION
    //
    //-------------------------------------------------------------------------------------------------------------

    void CreateGame()
    {
        InitializeVariables();

        //Get Solution
        news = loadNews.getNews(-1); // -1 is get a random new from the document
        solution = news.Title;

        CleanSolution();

        ShuffleSymbols(3);

        numberLetters = (float)getLettersLeft();
    }
    void InitializeVariables()
    {
        //Initialize variables
        solution = "";
        cleanSolution = "";
        shuffledSymbols = "";
        numberOfTries = 0;
        numberOfHints = 0;
        news = new NewsAsset();

        //Initialize letter array
        letters = new letterState[26];
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i] = 0; //NONE
        }
    }

    void CleanSolution()
    {
        string lowerSolution = solution.ToLower();
        cleanSolution = "";
        foreach (char l in lowerSolution)
        {
            if (l == 'ñ') cleanSolution += 'n';
            else if (l == 'ç') cleanSolution += 'c';
            else if ("áàäâ".Contains(l)) cleanSolution += 'a';
            else if ("éèëê".Contains(l)) cleanSolution += 'e';
            else if ("íìïî".Contains(l)) cleanSolution += 'i';
            else if ("óòöô".Contains(l)) cleanSolution += 'o';
            else if ("úùüû".Contains(l)) cleanSolution += 'u';
            else cleanSolution += l;
        }
    }

    void ShuffleSymbols(uint times = 1)
    {
        shuffledSymbols = SYMBOLS;

        for (int n = 0; n < times; n++)
        {
            char[] symbols = shuffledSymbols.ToCharArray();
            System.Random rng = new System.Random();

            for (int i = symbols.Length - 1; i > 0; i--)
            {
                int k = rng.Next(i + 1);
                char aux = symbols[k];
                symbols[k] = symbols[i];
                symbols[i] = aux;
            }

            shuffledSymbols = new string(symbols);
        }
    }


    //-------------------------------------------------------------------------------------------------------------
    //
    // USEFUL FUNCTIONS
    //
    //-------------------------------------------------------------------------------------------------------------

    public bool isLetter(char c)
    {
        return (int)c >= 97 && (int)c <= 122;
    }

    public bool positionIsEmpty(int p)
    {
        //If is letter and position is not correct, return true
        return isLetter(cleanSolution[p]) && letters[getLetterNumber(cleanSolution[p])] != letterState.CORRECT;
    }

    public int getLetterNumber(char c)
    {     
        return ((int)c - 19) % NUM_LETTERS;
    }


    //-------------------------------------------------------------------------------------------------------------
    //
    // GAME FUNCTIONALITIES
    //
    //-------------------------------------------------------------------------------------------------------------

    public void Reset()
    {
        CreateGame();
    }

    public bool checkLetter(char c, int pos)
    {
        if (pos >= cleanSolution.Length || pos < 0)
        {
            Debug.LogError("Letter checking failed: position out of bounds.");
            return false;
        }

        //Add a try
        numberOfTries++;

        //Correct
        if (cleanSolution[pos] == c)
        {
            letters[getLetterNumber(c)] = letterState.CORRECT;
            return true;
        }

        //Not correct
        if (letters[getLetterNumber(c)] == letterState.NONE)
        {
            if (cleanSolution.Contains(c))
            {
                letters[getLetterNumber(c)] = letterState.POSSIBLE;
            }
            else
            {
                letters[getLetterNumber(c)] = letterState.NOTAPPEARS;
            }
        }

        //TODO: Afegir la lletra comprovada a la llista de lletres comprovades en aquesta posició.

        return false;
    }

    public uint getLettersLeft()
    {
        //if (solution == getState()) return 0;

        uint n = NUM_LETTERS;
        for (int i = 97; i < 97 + NUM_LETTERS; i++)
        {
            if (!cleanSolution.Contains((char)i)) n--;
            else if (letters[getLetterNumber((char)i)] == letterState.CORRECT) n--;
        }

        return n;
    }

    public string getState()
    {
        string result = "";
        for (int l = 0; l < cleanSolution.Length; l++)
        {
            if (isLetter(cleanSolution[l]))
            {
                if (letters[getLetterNumber(cleanSolution[l])] == letterState.CORRECT)
                {
                    result += solution[l];
                }
                else
                {
                    result += shuffledSymbols[getLetterNumber(cleanSolution[l])];
                }
            }
            else
            {
                result += solution[l];
            }
        }

        return result;
    }

    public bool Victory()
    {
        return getState() == solution;
    }

}
