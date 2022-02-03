namespace AlienAttackRetro
{
    using System;
    using System.Collections.Generic;

    public class AlienManager
    {
        private static readonly Random Random = new Random();
        private readonly List<AlienMover> movers;
        private readonly PrintService printService;
        private DifficultySettings difficultySettings;

        public AlienManager(PrintService printService)
        {
            this.printService = printService;
            this.movers = new List<AlienMover>();
        }

        public void HanldeShutDowbCode(object sender, int shutDownCode)
        {
            foreach (AlienMover mover in this.movers)
            {
                mover.Stop();
            }
        }

        public List<AlienMover> GetAlienMovers()
        {
            return this.movers;
        }

        public void ApplySettings(Difficulty difficulty)
        {
            this.difficultySettings = DifficultyStableSettings.ConvertToSettings(difficulty);

            for (int index = 0; index < this.difficultySettings.AliensCount; index++)
            {
                AlienMover mover = this.CreateAlienMover(difficulty, index);
                this.movers.Add(mover);
                mover.Start();
            }
        }

        public void SubscribeForAlienMovment(CrossHair crossHair)
        {
            foreach (AlienMover alienMover in this.movers)
            {
                alienMover.Alien.PositionChanged += crossHair.UpdateCrossHair;
            }
        }

        private AlienMover CreateAlienMover(Difficulty difficulty, int index)
        {
            int delay = Random.Next(this.difficultySettings.MinSpeed, this.difficultySettings.MaxSpeed);

            Alien alien = Alien.CREATE( // X, Y, HITS, DIRECTION.
                this.difficultySettings.StartsAtBoarder ? 0 : Random.Next(0, Console.WindowWidth - 1),
                Random.Next(0, Console.WindowHeight - 2),
                Random.Next(this.difficultySettings.MinHealthPoints, this.difficultySettings.MaxHealthPoints + 1),
                this.DeterminAlienDirection(difficulty, index));

            AlienMover mover = new AlienMover(delay, alien, difficulty == Difficulty.MEDIUM && index > 15, difficulty == Difficulty.HARD);

            mover.Alien.PositionChanged += this.printService.HanldeAlienPositionChangedEvent;
            return mover;
        }

        private Direction DeterminAlienDirection(Difficulty difficulty, int index)
        {
            if (difficulty == Difficulty.QUICK_DRAW || difficulty == Difficulty.EASY)
            {
                return Direction.Left;
            }
            else if (difficulty == Difficulty.MEDIUM)
            {
                if (index < this.difficultySettings.AliensCount / 2)
                {
                    return Random.Next() % 2 == 0 ? Direction.Left : Direction.Down;
                }
                else
                {
                    return DirectionUtils.RandomDirection();
                }
            }
            else
            {
                return DirectionUtils.RandomDirection();
            }
        }
    }
}
