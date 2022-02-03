namespace AlienAttackRetro
{
    using System;

    public class Program
    {
        public static void Main()
        {
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            Console.SetWindowSize(Console.BufferWidth, Console.BufferHeight);
            Console.CursorVisible = false;
            new GameEngine().StartGame();
        }
    }
}
