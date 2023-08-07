using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using hangman;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Text;

static void Run()
{
    var renderer = new hangman.Render();

    List<char> guessed = new() { };

    string utfString = Encoding.UTF8.GetString(hangman.Resources.WordsDE, 0, hangman.Resources.WordsDE.Length);

    JArray o1 = JArray.Parse(utfString);

    Random rd = new Random();
    string word = o1[rd.Next(0, o1.Count - 1)].ToString();

    bool completed = false;
    int wrongAnswers = 0;
    string message = "";

    renderer.RenderScreen(0, word, guessed, "");
    while (!completed)
    {
        message = "";
        Console.Write("Buchstabe Eingeben: ");
        string val = Console.ReadLine();
        if (val == null || val.Count() > 1 || val.Count() == 0)
        {
            message = "Bitte Genau 1 Buchtstabe oder das ganze Wort eingeben!";
            wrongAnswers++;
        }
        else if (val == word)
        {
            completed = true;
            message = "Du hast gewonnen!";
        }
        else
        {
            if ((!word.Contains(val.ToUpper().ToCharArray().First()) && !word.Contains(val.ToLower().ToCharArray().First())) || guessed.Contains(val.ToCharArray().First()))
            {
                wrongAnswers++;
            }
            if (!guessed.Contains(val.ToCharArray().First())) guessed.Add(val.ToCharArray().First());
        }

        if (wrongAnswers == renderer.hangmanStages.Count() - 1)
        {
            completed = true;
            message = "Du hast verloren! Das Wort war " + word + ".";
        }
        else if (word.All(c => { return guessed.Contains(c.ToString().ToLower().ToCharArray().First()) || guessed.Contains(c.ToString().ToUpper().ToCharArray().First()); }))
        {
            completed = true;
            message = "Du hast gewonnen!";
        }

        renderer.RenderScreen(wrongAnswers, word, guessed, message);
    }
}

Run();
while (true)
{
    Console.Write("Neustarten? (j/N): ");
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