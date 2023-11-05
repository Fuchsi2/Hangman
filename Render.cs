using System.Reflection;
using System.Text.RegularExpressions;

namespace hangman
{
    internal static class Render
    {
        //ascii art by https://ascii.co.uk/art/hangman

        public static string hangmanLogo = ""
            + "<fd> _\n"
            + "| |     <f9>https://github.com/Fuchsi2/Hangman<fd>\n"
            + "| |__   __ _ _ __   __ _ _ __ ___   __ _ _ __  \n"
            + "| '_ \\ / _` | '_ \\ / _` | '_ ` _ \\ / _` | '_ \\ \n"
            + "| | | | (_| | | | | (_| | | | | | | (_| | | | |   <f6>von Fuchsi2<fd>\n"
            + "|_| |_|\\__,_|_| |_|\\__, |_| |_| |_|\\__,_|_| |_|\n"
            + "                    __/ |\n"
            + "                   |___/   <f8>V" + Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion + "<f#>\n";

        public static List<string> hangmanStages = new()
            {
              "\n"
            + "\n"
            + "\n"
            + "\n"
            + "\n"
            + "\n"
            + "\n"
            + "    <f2>_____<f#>\n",
              "\n"
            + "     <f8>|\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "    <f2>_<f8>|<f2>___<f#>\n",
              "      <f8>_______\n"
            + "     |/\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "    <f2>_<f8>|<f2>___<f#>\n",
              "      <f8>_______\n"
            + "     |/      |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "    <f2>_<f8>|<f2>___<f#>\n",
              "      <f8>_______\n"
            + "     |/      |\n"
            + "     |      <fe>(_)<f8>\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "     |\n"
            + "    <f2>_<f8>|<f2>___<f#>\n",
              "      <f8>_______\n"
            + "     |/      |\n"
            + "     |      <fe>(_)<f8>\n"
            + "     |       <fe>|<f8>\n"
            + "     |       <fe>|<f8>\n"
            + "     |\n"
            + "     |\n"
            + "    <f2>_<f8>|<f2>___<f#>\n",
              "      <f8>_______\n"
            + "     |/      |\n"
            + "     |      <fe>(_)<f8>\n"
            + "     |      <fe>\\|/<f8>\n"
            + "     |       <fe>|<f8>\n"
            + "     |\n"
            + "     |\n"
            + "    <f2>_<f8>|<f2>___<f#>\n",
              "      <f8>_______\n"
            + "     |/      |\n"
            + "     |      <fe>(_)<f8>\n"
            + "     |      <fe>\\|/<f8>\n"
            + "     |       <fe>|<f8>\n"
            + "     |      <fe>/ \\<f8>\n"
            + "     |\n"
            + "    <f2>_<f8>|<f2>___<f#>\n"
            };

        public static string getLostText(string word)
        {
            return "<fc>Du hast verloren! Das Wort war <f5>" + word + "<fc>.<f#>";
        }

        public static string winText = "<fa>Du hast gewonnen!<f#>";

        public static string wrongInputMessageText = "<fc>Bitte Genau 1 Buchtstabe oder das ganze Wort eingeben!<f#>";

        public static string inputText = "Buchstabe Eingeben: ";

        public static string restartQuestionText = "Neustarten? (<fa>j<f#>/<fc>N<f#>): ";

        public static void RenderScreen(int stage, string word, List<string> guessedChars, string message)
        {
            Console.Clear();
            Console.CursorVisible = false;
            ConsoleWrite(hangmanLogo,true);
            ConsoleWrite("\n",true);
            ConsoleWrite("Wort: ");
            for (int i = 0; i < word.Count(); i++)
            {
                if (GuessedContains(guessedChars, word[i].ToString().ToLower().ToCharArray().First()) || GuessedContains(guessedChars, word[i].ToString().ToUpper().ToCharArray().First()))
                {
                    ConsoleWrite("<f2>" + word[i] + "<f#> ");
                } else
                {
                    ConsoleWrite("_ ");
                }
            }
            ConsoleWrite("\n\n",true);
            ConsoleWrite(hangmanStages[stage],true);
            ConsoleWrite("\n",true);
            ConsoleWrite(String.Join(", ",guessedChars),true);
            ConsoleWrite("\n",true);
            ConsoleWrite(message,true);
            Console.CursorVisible = true;
        }

        private static Match HasColors(string text)
        {
            Regex rx = new Regex(@"<(?<color>[fb][0-9a-f#])>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rx.Match(text);
        }

        public static bool GuessedContains(List<string> list, char c)
        {
            Regex rx = new Regex(@"<[fb][0-9a-f#]>" + c + "<[fb][0-9a-f#]>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (string s in list)
            {
                if (rx.IsMatch(s)) return true;
            }
            return false;
        }

        public static void ConsoleWrite(string text, bool ln = false) {
            if (HasColors(text).Success)
            {
                List<char> textList = text.ToList<char>();

                for (int i = 0; i < textList.Count; i++)
                {
                    if (textList[i] == '<' && i +1 < textList.Count && i+2 < textList.Count && i + 3 < textList.Count)
                    {
                        Match match = HasColors("" + textList[i] + textList[i + 1] + textList[i + 2] + textList[i + 3]);
                        if (match.Success)
                        {
                            switch (match.Groups["color"].Value.ToLower())
                            {
                                case "f0":
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    break;
                                case "f1":
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    break;
                                case "f2":
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    break;
                                case "f3":
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    break;
                                case "f4":
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    break;
                                case "f5":
                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                    break;
                                case "f6":
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    break;
                                case "f7":
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    break;
                                case "f8":
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    break;
                                case "f9":
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;
                                case "fa":
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case "fb":
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    break;
                                case "fc":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case "fd":
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;
                                case "fe":
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                                case "ff":
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                case "f#":
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    break;
                                // Unused:
                                //case "bd":
                                //    Console.BackgroundColor = ConsoleColor.Magenta;
                                //    break;
                                //case "b#":
                                //    Console.BackgroundColor = ConsoleColor.Black;
                                //    break;
                                default:
                                    break;
                            }
                            i += 3;
                        }
                        else
                        {
                            Console.Write(textList[i]);
                        }
                    }
                    else
                    {
                        Console.Write(textList[i]);
                    }
                }
            }
            else
            {
                Console.Write(text);
            }
            if (ln) Console.WriteLine();
        }
    }
}
