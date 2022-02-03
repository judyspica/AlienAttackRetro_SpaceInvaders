namespace AlienAttackRetro
{
    using System;

    public class DifficultySettings
    {
        public DifficultySettings(bool startsAtBoarder, int minHealthPoints, int maxHealthPoints, int minSpeed, int maxSpeed, int aliensCount, TimeSpan timeSpan)
        {
            this.StartsAtBoarder = startsAtBoarder;
            this.MinHealthPoints = minHealthPoints;
            this.MaxHealthPoints = maxHealthPoints;
            this.MinSpeed = minSpeed;
            this.MaxSpeed = maxSpeed;
            this.AliensCount = aliensCount;
            this.TimeSpan = timeSpan;
        }

        public bool StartsAtBoarder
        {
            get; private set;
        }

        public int MinHealthPoints
        {
            get; private set;
        }

        public int MaxHealthPoints
        {
            get; private set;
        }

        public int MinSpeed
        {
            get; private set;
        }

        public int MaxSpeed
        {
            get; private set;
        }

        public int AliensCount
        {
            get; private set;
        }

        public TimeSpan TimeSpan
        {
            get; private set;
        }
    }
}
