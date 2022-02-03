namespace AlienAttackRetro
{
    public class GameConfiguration
    {
        public GameConfiguration()
        {
            Difficulty = Difficulty.QUICK_DRAW;
        }

        public Difficulty Difficulty
        {
            get; private set;
        }

        public void SelectNext()
        {
            if (Difficulty == Difficulty.QUICK_DRAW)
            {
                Difficulty = Difficulty.EASY;
            }
            else if (Difficulty == Difficulty.EASY)
            {
                Difficulty = Difficulty.MEDIUM;
            }
            else if (Difficulty == Difficulty.MEDIUM)
            {
                Difficulty = Difficulty.HARD;
            }
            else
            {
                Difficulty = Difficulty.QUICK_DRAW;
            }
        }
    }
}
