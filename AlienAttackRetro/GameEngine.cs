namespace AlienAttackRetro
{
    using System;

    public class GameEngine
    {
        private MenuInterface menuInterface;
        private GameInterface gameInterface;

        public GameEngine()
        {
            this.menuInterface = new MenuInterface();
            this.gameInterface = new GameInterface();
        }

        public void StartGame()
        {
            while (true)
            {
                this.gameInterface = new GameInterface();
                this.menuInterface = new MenuInterface();
                this.menuInterface.EnterStartMenu();

                Console.Clear();
                Console.CursorVisible = false;
                Difficulty difficulty = this.menuInterface.GetSelectedDifficulty();
                this.gameInterface.Start(difficulty);
            }
        }
    }
}