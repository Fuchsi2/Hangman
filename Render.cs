namespace hangman
{
    internal class Render
    {
        public Render(){ } //ascii art by https://ascii.co.uk/art/hangman

        public string hangmanLogo = ""
            + " _\n"
            + "| |     https://github.com/Fuchsi2/Hangman\n"
            + "| |__   __ _ _ __   __ _ _ __ ___   __ _ _ __  \n"
            + "| '_ \\ / _` | '_ \\ / _` | '_ ` _ \\ / _` | '_ \\ \n"
            + "| | | | (_| | | | | (_| | | | | | | (_| | | | |   Von Fuchsi2\n"
            + "|_| |_|\\__,_|_| |_|\\__, |_| |_| |_|\\__,_|_| |_|\n"
            + "                    __/ |\n"
            + "                   |___/   V1.0.10\n";

        public List<string> hangmanStages = new()
            {
              "\n"
            + "\n"
            + "\n"
            + "\n"
            + "\n"
            + "\n"
            + "\n"
            + "    _____\n",
              "\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "    _|___\n",
              "      _______\n"
            + "     |/\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "    _|___\n",
              "      _______\n"
            + "     |/      |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "    _|___\n",
              "      _______\n"
            + "     |/      |\n"
            + "     |      (_)\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "    _|___\n",
              "      _______\n"
            + "     |/      |\n"
            + "     |      (_)\n"
            + "     |       |\n"
            + "     |       |\n"
            + "     |\n"
            + "     |\n"
            + "    _|___\n",
              "      _______\n"
            + "     |/      |\n"
            + "     |      (_)\n"
            + "     |      \\|/\n"
            + "     |       |\n"
            + "     |\n"
            + "     |\n"
            + "    _|___\n",
              "      _______\n"
            + "     |/      |\n"
            + "     |      (_)\n"
            + "     |      \\|/\n"
            + "     |       |\n"
            + "     |      / \\\n"
            + "     |\n"
            + "    _|___\n"
            };

        public void RenderScreen(int stage, string word, List<char> guessedChars, string message)
        {
            Console.Clear();
            Console.WriteLine(hangmanLogo);
            Console.WriteLine("\n");
            Console.Write("Wort: ");
            for (int i = 0; i < word.Count(); i++)
            {
                if (guessedChars.Contains(word[i].ToString().ToLower().ToCharArray().First()) || guessedChars.Contains(word[i].ToString().ToUpper().ToCharArray().First()))
                {
                    Console.Write(word[i] + " ");
                } else
                {
                    Console.Write("_ ");
                }
            }
            Console.WriteLine("\n\n");
            Console.WriteLine(hangmanStages[stage]);
            Console.WriteLine("\n");
            Console.WriteLine(String.Join(", ",guessedChars));
            Console.WriteLine("\n");
            Console.WriteLine(message);
        }
    }
}
