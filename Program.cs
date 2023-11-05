﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using hangman;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Text;

static void Run()
{

    List<char> guessed = new() { };

    string utfString = Encoding.UTF8.GetString(Resources.WordsDE, 0, Resources.WordsDE.Length);

    JArray o1 = JArray.Parse(utfString);

    Random rd = new Random();
    string word = o1[rd.Next(0, o1.Count - 1)].ToString();

    bool completed = false;
    int wrongAnswers = 0;
    string message = "";

    Render.RenderScreen(0, word, guessed, "");
    while (!completed)
    {
        message = "";
        Console.Write(Render.inputText);
        string val = Console.ReadLine();
        if (val == null || val.Count() > 1 || val.Count() == 0)
        {
            message = Render.wrongInputMessageText;
            wrongAnswers++;
        }
        else if (val == word)
        {
            completed = true;
            message = Render.winText;
        }
        else
        {
            if ((!word.Contains(val.ToUpper().ToCharArray().First()) && !word.Contains(val.ToLower().ToCharArray().First())) || guessed.Contains(val.ToCharArray().First()))
            {
                wrongAnswers++;
            }
            if (!guessed.Contains(val.ToCharArray().First())) guessed.Add(val.ToCharArray().First());
        }

        if (wrongAnswers == Render.hangmanStages.Count - 1)
        {
            completed = true;
            message = Render.getLostText(word);
        }
        else if (word.All(c => { return guessed.Contains(c.ToString().ToLower().ToCharArray().First()) || guessed.Contains(c.ToString().ToUpper().ToCharArray().First()); }))
        {
            completed = true;
            message = Render.winText;
        }

        Render.RenderScreen(wrongAnswers, word, guessed, message);
    }
}

Run();
while (true)
{
    Console.Write(Render.restartQuestionText);
    List<string> validYesAnswers = new() { "y", "Y", "j", "J" };
    if (validYesAnswers.Contains(Console.ReadLine()))
    {
        Run();
    }
    else
    {
        Environment.Exit(0);
    }
}