namespace AlienAttackRetro
{
    using System;

    public class DirectionUtils
    {
        private static readonly Random RANDOM = new Random();

        public static Direction RandomDirection()
        {
            int indexToUse = RANDOM.Next(0, 4);
            switch (indexToUse)
            {
                case 0: return Direction.Left;
                case 1: return Direction.Right;
                case 2: return Direction.Up;
                case 3: return Direction.Down;
                default: return Direction.Left;
            }
        }
    }
}
