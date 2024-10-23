using System.ComponentModel.DataAnnotations;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.White;
        var words = LoadWords();
        string? input;
        bool cont;
        do
        {
            Console.Write("Let's play wordle! Do you want to play? Press 'y' for yes and 'n' for no: ");
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                cont = false;
                Console.WriteLine("Invalid input!");
            }
            else
            {
                input = input!.ToLower();
                if (input == "y")
                {
                    cont = false;
                    Wordle(words);
                }
                else if (input == "n")
                {
                    cont = true;
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    cont = false;
                    Console.WriteLine("Invalid input!");
                }
            }
        } while (!cont);
    }

    private static Dictionary<char, int> LetterCount(string word)
    {
        var toReturn = new Dictionary<char, int>();
        foreach (char c in word)
        {
            if (toReturn.ContainsKey(c))
            {
                toReturn[c]++;
            }
            else
            {
                toReturn[c] = 1;
            }
        }
        return toReturn;
    }

    private static List<string> LoadWords()
    {
        var words = new List<string>();
        string path = @"D:/Training/C#/Wordle/words.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                words.Add(line);
            }
        }
        return words;
    }

    private static void Wordle(List<string> words)
    {
        Random random = new Random();
        int idxToChoose = random.Next(0, words.Count);
        string wordToGuess = words[idxToChoose];
        string?[] guesses = new string?[6];
        int guessNumber = 0;
        bool win = false;
        Console.Clear();
        while (guesses[5] == null && !win)
        {
            string? input;
            bool cont;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                foreach (string? guess in guesses)
                {
                    if (guess != null)
                    {
                        var gDict = LetterCount(guess);
                        var wtgDict = LetterCount(wordToGuess);
                        for (int i = 0; i < guess.Length; i++)
                        {
                            if (guess[i] == wordToGuess[i] && wtgDict[guess[i]] > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                gDict[guess[i]]--;
                                wtgDict[guess[i]]--;
                            } 
                            else if (wordToGuess.Contains(guess[i]) && wtgDict[guess[i]] > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                gDict[guess[i]]--;
                                wtgDict[guess[i]]--;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.Write(guess[i]);
                        }
                        Console.Write("\n");
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                input = Console.ReadLine();
                input = input!.Trim();
                if (input!.Length == 5 && words.Contains(input))
                {
                    cont = true;
                    guesses[guessNumber] = input;
                }
                else
                {
                    cont = false;
                }
            } while (!cont);
            win = guesses[guessNumber] == wordToGuess;
            guessNumber++;
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        foreach (string? guess in guesses)
        {
            if (guess != null)
            {
                var gDict = LetterCount(guess);
                var wtgDict = LetterCount(wordToGuess);
                for (int i = 0; i < guess.Length; i++)
                {
                    if (guess[i] == wordToGuess[i] && wtgDict[guess[i]] > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        gDict[guess[i]]--;
                        wtgDict[guess[i]]--;
                    }
                    else if (wordToGuess.Contains(guess[i]) && wtgDict[guess[i]] > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        gDict[guess[i]]--;
                        wtgDict[guess[i]]--;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(guess[i]);
                }
                Console.Write("\n");
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
        if (win)
        {
            Console.WriteLine("You won! Nice job!");
        }
        else
        {
            Console.WriteLine($"Out of guesses! The word was {wordToGuess}");
        }
    }
}