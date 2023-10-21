using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum letterState
{
    NONE,
    CORRECT,
    POSSIBLE
}

public class gameInfo : MonoBehaviour //Make non monobehabiour
{
    const int NUM_LETTERS = 26;

    string solution; //Solution frase
    //string solutionLetterTypes; //Info about solution letters (U uppercase, L lowercase, S special character)
    string cleanSolution;
    letterState[] letters;

    private void Start()
    {
        solution = "";
        cleanSolution = "";
        
        createGame();

        //letters[4] = letterState.CORRECT; //TODO: ERASE THIS

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
        solution = "Un de cada quatre habitatges de Gaza, destruït o malmès pels bombardejos d'Israel"; //TODO: agafar solution de base de dades/doc

        string lowerSolution = solution.ToLower();
        cleanSolution = "";
        foreach (char l in lowerSolution)
        {
            if (l == 'ñ') cleanSolution += 'n';
            else if ("áàäâ".Contains(l)) cleanSolution += 'a';
            else if ("éèëê".Contains(l)) cleanSolution += 'e';
            else if ("íìïî".Contains(l)) cleanSolution += 'i';
            else if ("óòöô".Contains(l)) cleanSolution += 'o';
            else if ("úùüû".Contains(l)) cleanSolution += 'u';
            else cleanSolution += l;
        }
    }

    string getState()
    {
        string result = "";
        for (int l = 0; l < cleanSolution.Length; l++) {
            int ascii = (int)cleanSolution[l];
            if (ascii >= 97 && ascii <= 122)
            {
                if (letters[getLetterNumber(cleanSolution[l])] == letterState.CORRECT)
                {
                    result += solution[l];
                }
                else
                {
                    result += "_";
                }
            }
            else
            {
                result += solution[l];
            }
        }

        return result;
    }

    uint getLettersLeft()
    {
        uint n = 26;
        foreach(letterState l in letters)
        {
            if (l == letterState.CORRECT) n--;
        }
        return n;
    }

    int getLetterNumber(char c)
    {     
        return ((int)c - 19) % NUM_LETTERS;
    }
}
