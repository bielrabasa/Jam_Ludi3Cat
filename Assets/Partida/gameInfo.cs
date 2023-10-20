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
    string solutionLetterTypes; //Info about solution letters (U uppercase, L lowercase, S special character)
    letterState[] letters;

    private void Start()
    {
        createGame();

        //letters[6] = letterState.CORRECT; //TODO: ERASE THIS
        //letters[0] = letterState.CORRECT; //TODO: ERASE THIS

        Debug.Log("Solution: " + solution);
        Debug.Log("Character types: " + solutionLetterTypes);
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
        
        //Create solutionLetterTypes
        for (int l = 0; l < solution.Length; l++)
        {
            int lnum = (int)solution[l];
            if (lnum >= 65 && lnum <= 90) solutionLetterTypes += "U";
            else if (lnum >= 97 && lnum <= 122) solutionLetterTypes += "L";
            else solutionLetterTypes += "S";
        }
    }

    string getState()
    {
        string result = "";
        string sol = solution.ToLower();
        for (int l = 0; l < sol.Length; l++) {
            if(solutionLetterTypes[l] == 'S')
            {
                result += sol[l];
            }
            else if(letters[getLetterNumber(sol[l])] == letterState.CORRECT)
            {
                if(solutionLetterTypes[l] == 'U')
                {
                    //Make letter Uppercase
                    result += (char)((int)sol[l] -32);
                }
                else
                {
                    result += sol[l];
                }
            }
            else
            {
                result += "_";
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
