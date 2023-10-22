using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsAsset
{
    public string Title = "";
    public string PhotoLink = "";
    public string Date = "";
    public string NewsLink = "";
}

public class loadNews
{
    static string[] loadCSV()
    {
        TextAsset data = Resources.Load<TextAsset>("noticies");
        return data.text.Split(new char[] { '\n' });
    }

    public static NewsAsset getNews(int number)
    {
        string[] file = loadCSV();

        NewsAsset n = new NewsAsset();

        if (number < 0 || number >= file.Length)
        {
            Debug.Log("Incorrect new picked, number out of bounds.");
            n.Title = "Notícia de prova, si veus aquest missatge, hi ha hagut un error!";
            return n;
        }

        string[] fileRow = file[number].Split(new char[] { ',' });

        //Comprovar si hi ha comes a la frase
        string title = fileRow[0];
        for (int i = 1; i < fileRow.Length - 3; i++)
        {
            title += ',';
            title += fileRow[i];
        }

        n.Title = title;
        
        //Mirar des del final de la ROW
        n.PhotoLink = fileRow[fileRow.Length - 3];
        n.Date = fileRow[fileRow.Length - 2];
        n.NewsLink = fileRow[fileRow.Length - 1];
        return n;
    }
}
