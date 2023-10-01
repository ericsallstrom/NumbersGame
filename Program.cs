// Eric Sällström .NET23

namespace NumbersGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isPlaying = true;

            // infinite loop until the user decides not to play anymore
            while (isPlaying)
            {
                // the game menu from which the user decides the level 
                // of difficulty and the choice to exit the game
                Console.Clear();
                Console.WriteLine("Välkommen till nummerspelet!" +
                                  "\nGissa rätt nummer och vinn fina priser!\n" +
                                  "\nVälj svårighetsgrad nedan:" +
                                  "\n--------------------------" +
                                  "\n[1] Lätt" +
                                  "\n[2] Medel" +
                                  "\n[3] Svår" +
                                  "\n[4] Galenskap" +
                                  "\n--------------------------" +
                                  "\n[5] Avsluta spelet" +
                                  "\n--------------------------");

                Console.Write("Ditt val: ");
                string menuChoice = Console.ReadLine();

                // using a switch statement in which cases 1-4 is calling a method to 
                // play the numbers game, depending on the user's choice of difficulty.
                // in case 5 the game is terminated.
                // in default a message is prompted to the user to enter the right value
                // if the user has entered something else than numbers 1-5.
                // the method Thread.Sleep() is used throughout the game to make sure the 
                // user reads all the instructions and makes for a smoother gaming experience.
                switch (menuChoice)
                {
                    case "1":
                        GameEasy();
                        isPlaying = PlayAgain();
                        break;
                    case "2":
                        GameIntermediate();
                        isPlaying = PlayAgain();
                        break;
                    case "3":
                        GameHard();
                        isPlaying = PlayAgain();
                        break;
                    case "4":
                        GameInsane();
                        isPlaying = PlayAgain();
                        break;
                    case "5":
                        Console.Write("\nSpelet avslutas...");
                        Thread.Sleep(1500);
                        Console.Clear();
                        Console.WriteLine("Tack för att du spelade!");
                        isPlaying = false;
                        break;
                    default:
                        Console.Write("\nFel input! Var god välj ett val från menyn." +
                            "\nTryck \"ENTER\" och försök igen.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        // if the user guesses the right number in three rounds or less they win a prize
        static void Prizes(int winningRound)
        {
            switch (winningRound)
            {
                // first place
                case 1:
                    Console.WriteLine("\nDu har vunnit vårt finaste pris...");
                    Thread.Sleep(2000);
                    Console.WriteLine("En båt!");
                    break;
                    // second place
                case 2:
                    Console.WriteLine("\nDu har vunnit vårt näst finaste pris...");
                    Thread.Sleep(2000);
                    Console.WriteLine("En cykel!");
                    break;
                    // third place
                case 3:
                    Console.WriteLine("\nDu har vunnit vårt tredjepris...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Kattmat för 500 kr!");
                    break;
            }
        }

        // after every played game, the user is asked if they want to keep playing
        // if not, the game is terminated, otherwise the game is restarted
        // the method is returning a bool, that's either true or false
        static bool PlayAgain()
        {
            Thread.Sleep(2500);
            Console.WriteLine("\nVill du spela igen? J/N");
            string userAnswer = Console.ReadLine();

            if (userAnswer.ToLower() == "j" || userAnswer.ToLower() == "ja")
            {
                return true;
            }
            else
            {
                Console.Write("\nSpelet avslutas...");
                Thread.Sleep(1000);
                Console.WriteLine("\nTack för att du spelade!");
                return false;
            }
        }

        // the method returns a random number between 0-3 and the reason for this 
        // is that the returned int is used as an index in the chosen string array
        // to display a message to the user based on their guess
        static int RandomIndexForMessage()
        {
            Random random = new();
            int randomIndex = random.Next(0, 4);

            return randomIndex;
        }

        // if the user's guess is to high, a message (one out of four) is displayed from an 
        // string array based on the random index from the RandomIndexForMessage()-method
        static string TooHighGuess()
        {
            int randomIndex = RandomIndexForMessage();

            string[] tooHighGuessGenerator = new[]
            { "Du måste gissa lägre. You can do it!",
              "Tyvärr, du gissade för högt. Försök igen!",
              "Fel! Försök gissa på ett lägre tal...",
              "Nej, nej, nej... Gissa lägre nästa gång."
            };

            string tooHighGuessMessage = tooHighGuessGenerator[randomIndex];
            return tooHighGuessMessage;
        }

        // if the user's guess is to low, a message (one out of four) is returned from an
        // string array based on the random index from the RandomIndexForMessage()-method
        static string TooLowGuess()
        {
            int randomIndex = RandomIndexForMessage();

            string[] tooLowGuessGenerator = new[]
            { "Du måste gissa högre. You can do it!",
              "Tyvärr, du gissade för lågt. Försök igen!",
              "Fel! Försök gissa på ett högre tal...",
              "Nej, nej, nej... Gissa högre nästa gång."
            };

            string tooLowMessage = tooLowGuessGenerator[randomIndex];
            return tooLowMessage;
        }

        // if the user's guess is way off, in that sense the guessed number is off by at least 
        // 10 digits, a message (one out of four) is returned from an string array based
        // on the random index from the RandomIndexForMessage()-method
        static string WayOffGuess()
        {
            int randomIndex = RandomIndexForMessage();

            string[] wayOffGuessGenerator = new[]
            { "Haha! Inte ens i närheten.",
              "Du kommer aldrig att gissa rätt om du fortsätter på det här sättet!",
              "Oj, där tog du i. Du kanske inte vill vinna det här?",
              "Dagens sämsta gissning?"
            };

            string wayOffMessage = wayOffGuessGenerator[randomIndex];
            return wayOffMessage;
        }

        // checks on what way the user's guess is wrong, i.e. either too high or low, 
        // and if the guessed number is close or way off the correct number
        static void CheckWrongGuess(int userGuess, int correctNr, int min, int max)
        {
            // variable difference is assigned an absolute value and with
            // Math.Abs, a negative int is converted to an positive int
            // which is then used to compute if the userGuess was close 
            // or way off the correctNr
            int difference = Math.Abs(correctNr - userGuess);

            while (true)
            {
                // if the user enters a value that's less than or larger than the numbers displayed
                if (userGuess < min || userGuess > max)
                {
                    Console.WriteLine($"Fel input! Du kan endast gissa på ett tal mellan {min}-{max}");
                    break;
                }
                else if (difference == 1)
                {
                    Console.WriteLine("Nu börjar det brännas du!" +
                        "\nBara ett nummer ifrån...");
                    break;
                }
                else if (difference >= 10)
                {
                    Console.WriteLine(WayOffGuess());
                }

                // using a ternary operator to check if userGuess is either too low or too high and
                // the variable wrongGuess is assigned the corresponding message from either method
                string wrongGuess = userGuess < correctNr ? TooLowGuess() : TooHighGuess();
                Console.WriteLine(wrongGuess);
                break;
            }
        }

        // this method receives an int based on which round the user guessed 
        // the right nr, and then displays the coherent message to that round
        static void CheckWinningRound(int winningRound, int correctNr)
        {
            Console.Clear();
            switch (winningRound)
            {
                case 1:
                    Console.WriteLine("Du är ett geni! Du klarade det på första försöket!");
                    goto default;
                case 2:
                    Console.WriteLine($"Snyggt! Du gissade rätt på bara {winningRound} omgångar!");
                    goto default;
                case 3:
                case 4:
                    Console.WriteLine($"Bra jobbat! Du tog det på {winningRound} omgångar.\n" +
                        $"Försök att slå det nästa gång!");
                    goto default;
                case 5:
                    Console.WriteLine($"Pfjuu! Det var på håret!\nDu klarade det på sista försöket...");
                    goto default;
                default:
                    Console.WriteLine($"Talet jag tänkte på var ju såklart {correctNr}.");
                    Thread.Sleep(1000);
                    break;
            }

            // calling the Prizes-method to check if the user have won a prize
            Prizes(winningRound);
        }

        // calling the rules for the insane numbers game
        static void GameInsane()
        {
            int min = 1;
            int max = 1000;

            int allowedGuesses = 3;

            Random randomNr = new Random();

            // correctNr is assigned a random number between 1-1000
            int correctNr = randomNr.Next(min, max + 1);

            PlayGame(min, max, allowedGuesses, correctNr);
        }

        // calling the rules for the hard numbers game
        static void GameHard()
        {
            int min = 1;
            int max = 100;

            int allowedGuesses = 3;

            Random randomNr = new Random();

            // correctNr is assigned a random number between 1-100
            int correctNr = randomNr.Next(min, max + 1);

            PlayGame(min, max, allowedGuesses, correctNr);
        }

        // calling the rules for the intermediate numbers game
        static void GameIntermediate()
        {
            int min = 1;
            int max = 40;

            int allowedGuesses = 5;

            Random randomNr = new Random();

            // correctNr is assigned a random number between 1-40
            int correctNr = randomNr.Next(min, max + 1);

            PlayGame(min, max, allowedGuesses, correctNr);
        }

        // calling the rules for the easy numbers game
        static void GameEasy()
        {
            int min = 1;
            int max = 20;

            int allowedGuesses = 5;

            Random randomNr = new Random();

            // correctNr is assigned a random number between 1-20
            int correctNr = randomNr.Next(min, max + 1);

            PlayGame(min, max, allowedGuesses, correctNr);
        }

        // starts a game based on the level of difficulty the user has chosen
        static void PlayGame(int min, int max, int allowedGuesses, int correctNr)
        {
            Console.Clear();
            Console.WriteLine($"Jag tänker på ett nummer mellan {min}-{max}... Kan du gissa vilket?" +
                $"\nDu får {allowedGuesses} försök.");

            GameSetup(min, max, correctNr, allowedGuesses);
        }

        // this method calls the standard game setup for every numbers game, no matter the difficulty level
        static void GameSetup(int min, int max, int correctNr, int allowedGuesses)
        {
            int userGuess = 0;
            int guessCount = 0;

            while (guessCount < allowedGuesses)
            {
                // continues to run as long as the user don't enter a valid value
                if (!int.TryParse(Console.ReadLine(), out userGuess))
                {
                    Console.WriteLine($"Fel input! Var god ange ett tal mellan {min}-{max}." +
                        "\nFörsök igen.");
                    continue;
                }

                guessCount++;

                if (userGuess == correctNr)
                {
                    CheckWinningRound(guessCount, userGuess);
                    break;
                }

                CheckWrongGuess(userGuess, correctNr, min, max);

                Console.WriteLine($"Du har {allowedGuesses - guessCount} försök kvar.");
            }
            // if the user fails to guess the right number in a specific number of attempts 
            if (guessCount == allowedGuesses && userGuess != correctNr)
            {
                Console.Clear();
                Console.WriteLine($"Tyvärr, du lyckades inte gissa talet på {allowedGuesses} försök." +
                    $"\nTalet jag tänkte på var {correctNr}.");
                Thread.Sleep(2000);
                Console.WriteLine("\nGissa det rätta talet på någon av de tre första omgångarna" +
                                  "\nför kunna att vinna några av våra fina priser!");
            }
        }
    }
}