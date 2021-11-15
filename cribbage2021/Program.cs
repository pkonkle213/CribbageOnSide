using cribbage2021.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace cribbage2021
{
    public class Program
    {
        // Get the connection string from the appsettings.json file
        /*       IConfigurationBuilder builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

               IConfigurationRoot configuration = builder.Build()

               string connectionString = configuration.GetConnectionString("Project");
        */
        public static void Main(string[] args)
        {
            bool quit = false;
            while (!quit)
            {

                Console.WriteLine();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine();
                Console.WriteLine("1) Play Solitaire Cribbage");
                Console.WriteLine("2) Test all hands");
                Console.WriteLine("3) Hand Predictor");
                Console.WriteLine("0) Exit");
                Console.WriteLine();
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        Console.Clear();
                        Solitaire();
                        break;

                    case "2":
                        Console.Clear();
                        MassiveMath();
                        break;

                    case "3":
                        Console.Clear();
                        StatsForHands();
                        break;

                    case "0":
                        Console.Clear();
                        Console.WriteLine("It's been a pleasure! Hope you had fun!");
                        quit = true;
                        break;

                    default:
                        Console.WriteLine("Did you read the instructions? Pick a legit number.");
                        break;
                }
            }
        }

        public static void Solitaire()
        {
            UserInterface ui = new UserInterface();
            ui.Run();
        }

        public static void MassiveMath()
        {
            Console.WriteLine();
            Console.WriteLine("This feature is not yet implemented.");
            Console.WriteLine("Press [Enter] to continue.");
            string answer = Console.ReadLine();
            if (answer == "Phillip".ToUpper())
            {
                BuildingAI ai = new BuildingAI();
                ai.mathTime();
            }
        }

        public static void StatsForHands()
        {
            Console.WriteLine();
            Console.WriteLine("This feature is not yet implemented.");
            Console.WriteLine("Press [Enter] to continue.");
            string answer = Console.ReadLine();
            if (answer == "Phillip".ToUpper())
            {
                BuildingAI ai = new BuildingAI();
                ai.OddsOfHand();
            }
        }
    }
}
