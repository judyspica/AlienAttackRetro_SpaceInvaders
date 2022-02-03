namespace AlienAttackRetro
{
    using System;

    public class DifficultyStableSettings
    {
        public static readonly DifficultySettings QUICKDRAW = new DifficultySettings(true, 1, 1, 30, 50, 1, TimeSpan.FromSeconds(15));
        public static readonly DifficultySettings EASY = new DifficultySettings(true, 5, 15, 100, 800, 10, TimeSpan.FromMinutes(5));
        public static readonly DifficultySettings MEDIUM = new DifficultySettings(false, 5, 15, 100, 800, 30, TimeSpan.FromMinutes(10));
        public static readonly DifficultySettings HARD = new DifficultySettings(false, 5, 15, 100, 800, 50, TimeSpan.FromMinutes(15));

        private DifficultyStableSettings()
        {
            // Not allowed
        }

        public static DifficultySettings ConvertToSettings(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.QUICK_DRAW: return QUICKDRAW;
                case Difficulty.EASY: return EASY;
                case Difficulty.MEDIUM: return MEDIUM;
                case Difficulty.HARD: return HARD;
                default: return null;
            }
        }
    }
}
