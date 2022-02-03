namespace AlienAttackRetro
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class GameInterface
    {
        private readonly AlienManager alienManager;
        private readonly KeyboardWatcher keyboardWatcher;
        private readonly CrossHair crossHair;
        private readonly PrintService printService;
        private readonly Timer timer;
        private bool isRunning = false;

        public GameInterface()
        {
            this.keyboardWatcher = new KeyboardWatcher();
            this.printService = new PrintService();
            this.alienManager = new AlienManager(this.printService);
            this.crossHair = new CrossHair((Console.WindowWidth / 2) - 1, (Console.WindowHeight / 2) - 1, this.printService);
            this.timer = new Timer();
        }

        public event EventHandler<int> ShutDownCode;

        public void Start(Difficulty difficulty)
        {
            this.isRunning = true;
            this.printService.Print = true;
            this.RouteControlsToGame();
            this.timer.Init(DifficultyStableSettings.ConvertToSettings(difficulty).TimeSpan);
            this.alienManager.ApplySettings(difficulty);
            this.alienManager.SubscribeForAlienMovment(this.crossHair);

            while (this.isRunning)
            {
                Thread.Sleep(1000);
                if (this.AllMoversStopped(this.alienManager.GetAlienMovers()))
                {
                    this.printService.Print = false;
                    this.timer.IsRunning = false;
                    this.isRunning = false;
                }
            }

            if (this.AllMoversStopped(this.alienManager.GetAlienMovers()))
            {
                Console.SetCursorPosition(30, Console.WindowHeight - 1);
                Console.WriteLine("Congratulations you have shot down all aliens! Press any key to go back to main menu");
                Console.ReadKey();
            }

            this.ShutDownCode(this, 0);
        }

        private bool AllMoversStopped(List<AlienMover> alienMovers)
        {
            foreach (AlienMover mover in alienMovers)
            {
                if (mover.IsAlive())
                {
                    return false;
                }
            }

            return true;
        }

        private void RouteControlsToGame()
        {
            this.keyboardWatcher.KeyPressed += this.KeyPressed;
            this.keyboardWatcher.Start();

            this.ShutDownCode += this.alienManager.HanldeShutDowbCode;
            this.ShutDownCode += this.keyboardWatcher.HandleShutdowcode;

            this.timer.Tick += this.printService.PrintTimer;
            this.timer.Tick += this.TimerIsFinished;
        }

        private void TimerIsFinished(object sender, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero)
            {
                Console.SetCursorPosition(30, Console.WindowHeight - 1);
                Console.WriteLine("Time Ran out!:( better luck next time! Press any key to go back to main menu");
                Console.ReadKey(true);
                this.isRunning = false;
            }
        }

        private void KeyPressed(object sender, KeyboardPressedEvent e)
        {
            switch (e.Key)
            {
                case ConsoleKey.UpArrow:
                    this.crossHair.DecreaseY();
                    break;
                case ConsoleKey.DownArrow:
                    this.crossHair.IncreaseY();
                    break;
                case ConsoleKey.RightArrow:
                    this.crossHair.IncreaseX();
                    break;
                case ConsoleKey.LeftArrow:
                    this.crossHair.DecreaseX();
                    break;
                case ConsoleKey.Spacebar:
                    this.crossHair.FireAt(this.alienManager.GetAlienMovers());
                    break;
            }
        }
    }
}
