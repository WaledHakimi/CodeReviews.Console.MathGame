using System;
using System.Diagnostics;

enum Difficulty {  Easy = 10, Medium = 50, Hard = 100 };
enum GameType { Addition, Subtraction, Multiplication, Division, RandomOperation, History, Quite}

namespace FinalMathGame
{
    class FinalMathGame
    {
        static List<string> gameHistory = new List<string>();
        static Random random = new Random();
        internal static void Main()
        {
            
            var date = DateTime.UtcNow;
            Console.WriteLine("Enter your name");
            var name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Hello {name.Trim().ToUpper()}. It's {date.DayOfWeek}. Welcome to Math Game");

            bool isGameOn = true;
            do
            {
                GameType choice = GameSelection();
                if (choice == GameType.Quite) break;
                if(choice == GameType.History) { ViewHistory(); continue; }

                Difficulty lvldiff = DifficultyLevel();
                RunGame(choice, lvldiff);
            }while (isGameOn);
            
        }

        private static void RunGame(GameType gameType, Difficulty lvldiff)
        {
            int score = 0;

            //start timer, initialize and start stopWatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            Console.Clear();
            //for loop to play game
            for(int i = 0; i < 5; i++)
            {
                //to select random operation
                GameType currentgame = (gameType == GameType.RandomOperation) ? (GameType)random.Next(0, 4) : gameType;
                int firstnumber = random.Next(1, (int)lvldiff);
                int secondnumber = random.Next(1, (int)lvldiff);

                //condition for division to ensure  whole number
                if(currentgame == GameType.Division)
                {
                    firstnumber = firstnumber * secondnumber; //Divident = products of two number
                }
                
                //game operations
                int result = currentgame switch
                {
                    GameType.Addition => firstnumber + secondnumber,
                    GameType.Subtraction => firstnumber - secondnumber,
                    GameType.Multiplication => firstnumber * secondnumber,
                    GameType.Division => firstnumber / secondnumber,
                    _ => 0
                };

                //determine operation symbol
                char gamesymbol = currentgame switch
                {
                    GameType.Addition => '+',
                    GameType.Subtraction => '-',
                    GameType.Multiplication => '*',
                    GameType.Division => '/',
                    _ => '?'
                };

                //display the question with operation symbol
                
                Console.WriteLine($"{firstnumber}{gamesymbol}{secondnumber}");
                
                //checke the input to be integer and check the answer is correct
                if(int.TryParse(Console.ReadLine(), out int userinput) && userinput == result)
                {
                    Console.WriteLine("CORRECT. Congratulations!!!");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Oop WRONG. The correct answer {result}");
                }
            }

            //stop the timer and calculate elapsed time
            stopwatch.Stop();
            TimeSpan timetaken = stopwatch.Elapsed;

            //time format
            string timedisplay = timetaken.TotalSeconds < 60 ? $"{timetaken.Seconds} seconds." :
                $"{timetaken.Minutes} m {timetaken.Seconds} sec";

            string answer = $"{DateTime.UtcNow}: {gameType} ({lvldiff}) - Score: {score} pts";
            gameHistory.Add(answer);
            Console.WriteLine($"GAME OVER!!!\n Time taken {timedisplay}. {answer}. Press any key to return to main menu");
            Console.ReadLine();
        }

        //method for difficulty levle selection
        private static Difficulty DifficultyLevel()
        {
            Console.Clear();
            Console.WriteLine("Which level of game you would like to play?\n E - Easy\tM - Medium\tH - Hard");
            string userchoice = Console.ReadLine().Trim().ToLower();
            return userchoice switch
            {
                "e" => Difficulty.Easy,
                "m" => Difficulty.Medium,
                "h" => Difficulty.Hard,
                _ => Difficulty.Easy
            };
        }

        private static void ViewHistory()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("=======GAME HISTORY=======");
            Console.WriteLine("-------------------------------");

            foreach(var user in gameHistory) Console.WriteLine(user);
            Console.WriteLine("Press any key to return....");
            Console.ReadLine();

        }

        private static GameType GameSelection()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(@"What game would you like to play today. Choose from the below options:
                    v - View History
                    A - Addition
                    S - Subtraction
                    M - Multiplication
                    D - Division
                    R - Random Operation
                    Q - Quite the game.");

                string userselection = Console.ReadLine().Trim().ToLower();

                return userselection switch
                {
                    "a" => GameType.Addition,
                    "s" => GameType.Subtraction,
                    "m" => GameType.Multiplication,
                    "d" => GameType.Division,
                    "r" => GameType.RandomOperation,
                    "v" => GameType.History,
                    "q" => GameType.Quite,
                    _ => throw new Exception("Invalid selection.")
                };
            }
        }
    }
}