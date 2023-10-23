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
    public static int NUM_LETTERS = 26;
    const string SYMBOLS = "★✤✿♠♣♥♦☁☀▶∎☻◈〓✚⦿⊕☗✈◐☂♜♞♚♛☎";

    string solution;        //Hóla qüe, tAL?
    string cleanSolution;   //hola que, tal?
    string shuffledSymbols;
    
    public letterState[] letters;

    private void Awake()
    {
        solution = "";
        cleanSolution = "";
        shuffledSymbols = "";

        createGame();
    }

    private void Start()
    {
        //TODO: Erase this
        Debug.Log("Symbols: " + SYMBOLS);
        Debug.Log("Symbols: " + shuffledSymbols);
        Debug.Log("Solution: " + solution);
        Debug.Log("Clean Solution: " + cleanSolution);
        Debug.Log("Current board: " + getState());
        Debug.Log("Letters left: " + getLettersLeft());
    }

    void createGame()
    {
        //Initialize letter array
        letters = new letterState[26];
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i] = 0;
        }

        //Get Solution
        NewsAsset news = loadNews.getNews(8);
        solution = news.Title;

        CleanSolution();

        ShuffleSymbols(3);

        //TODO: Marcar com a correctes, les lletres que no surten a la frase
    }

    public uint getLettersLeft()
    {
        if (solution == getState()) return 0;
        
        uint n = 26;
        foreach (letterState l in letters)
        {
            if (l == letterState.CORRECT || l == letterState.NOTAPPEARS) n--;
        }
        return n;
    }

    public string getState()
    {
        string result = "";
        for (int l = 0; l < cleanSolution.Length; l++) {
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

    public bool isLetter(char c)
    {
        return (int)c >= 97 && (int)c <= 122;
    }

    public bool positionIsEmpty(int p)
    {
        //If is letter and position is not correct, return true
        return isLetter(cleanSolution[p]) && letters[getLetterNumber(cleanSolution[p])] != letterState.CORRECT;
    }

    public bool checkLetter(char c, int pos)
    {
        if(pos >= cleanSolution.Length || pos < 0)
        {
            Debug.LogError("Letter checking failed: position out of bounds.");
            return false;
        }

        //Correct
        if(cleanSolution[pos] == c)
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

    public int getLetterNumber(char c)
    {     
        return ((int)c - 19) % NUM_LETTERS;
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
}
