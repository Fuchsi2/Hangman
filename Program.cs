var renderer = new hangman.Render();
var wordlist = new hangman.WordlistDE();

List<char> guessed = new() {};

Random rd = new Random();
string word = wordlist.Words[rd.Next(0, wordlist.Words.Count - 1)];

bool completed = false;
int wrongAnswers = 0;
string message = "";

renderer.RenderScreen(0, word, guessed,"");
while (!completed)
{
    message = "";
    Console.Write("Buchstabe Eingeben: ");
    string val = Console.ReadLine();
    Console.WriteLine(val);
    if (val == word)
    {
        completed = true;
        message = "Du hast gewonnen!";
    }
    else if (val == null || val.Count() > 1)
    {
        message = "Bitte Genau 1 Buchtstabe oder das ganze Wort eingeben!";
        wrongAnswers++;
    }
    else
    {
        if (!word.Contains(val.ToCharArray().First()) || guessed.Contains(val.ToCharArray().First())) {
            wrongAnswers++;
        }
        guessed.Add(val.ToCharArray().First());
    }

    if (wrongAnswers == renderer.hangmanStages.Count() - 1)
    {
        completed = true;
        message = "Du hast verloren!";
    }
    else if (word.All(c => { return guessed.Contains(c); }))
    {
        completed = true;
        message = "Du hast gewonnen!";
    }

    renderer.RenderScreen(wrongAnswers, word, guessed, message);
}