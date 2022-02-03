namespace AlienAttackRetro
{
    using System;

    public class MenuInterface
    {
        private readonly GameConfiguration gameConfiguration;
        private bool gameStarted;

        public MenuInterface()
        {
            this.gameConfiguration = new GameConfiguration();
            this.gameStarted = false;
        }

        public void EnterStartMenu()
        {
            int consoleHeight = Console.WindowHeight;
            int consoleWidth = Console.WindowWidth;
            this.FlashWelcomeMessage();

            while (!this.gameStarted)
            {
                Console.Clear();
                Console.WriteLine(" Please pick an option 1-3\n");
                Console.WriteLine(" 1 Start Game");
                Console.WriteLine($" 2 Change Difficulty now {gameConfiguration.Difficulty}");
                Console.WriteLine(" 3 Exit Application");

                int selectedOption = this.GetIntInputFromUser(1, 4);
                switch (selectedOption)
                {
                    case 1:
                        this.CheckOnConsoleSize();
                        this.gameStarted = true;
                        break;
                    case 2:
                        this.gameConfiguration.SelectNext();
                        break;
                    case 3:
                        Console.SetWindowSize(consoleWidth, consoleHeight);
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public Difficulty GetSelectedDifficulty()
        {
            return this.gameConfiguration.Difficulty;
        }

        private void FlashWelcomeMessage()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                System.Threading.Thread.Sleep(350);
                Console.SetCursorPosition((Console.WindowWidth / 2) - 12, (Console.WindowHeight / 2) - 1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Welcome To Alien attacK\r");
                Console.ResetColor();
                System.Threading.Thread.Sleep(250);
            }
        }

        private int GetIntInputFromUser(int minInclusive, int maxExclusive)
        {
            int userSelectedNumber;
            do
            {
                this.CheckOnConsoleSize();
                bool parseSuccessful = int.TryParse(Console.ReadLine(), out userSelectedNumber);
                if (!parseSuccessful || userSelectedNumber < minInclusive || userSelectedNumber >= maxExclusive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This is not an option, please try again");
                    Console.ResetColor();
                    userSelectedNumber = -1;
                }
            }
            while (userSelectedNumber == -1);

            return userSelectedNumber;
        }

        private void CheckOnConsoleSize()
        {
            // I realized i can not do the same for when the actual game is running because it wil slow down any kind of movement on the console.
            Console.BufferWidth = 120;
            Console.BufferHeight = 30;
            Console.SetWindowSize(Console.BufferWidth, Console.BufferHeight);
        }
    }
}
