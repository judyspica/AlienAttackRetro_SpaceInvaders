namespace AlienAttackRetro
{
    using System;
    using System.Threading;

    public class AlienMover
    {
        private static readonly Random Random = new Random();
        private readonly bool isMediumLevel;
        private readonly bool isHardLevel;
        private readonly int stepsToBehaviourChange;
        private Thread moverThread;
        private bool isRunning;
        private int delay;
        private int stepCounter;

        public AlienMover(int delay, Alien alien, bool delayMediumChanges, bool delayHardChanges)
        {
            this.delay = delay;
            Alien = alien;

            this.isMediumLevel = delayMediumChanges;
            this.isHardLevel = delayHardChanges;

            this.stepsToBehaviourChange = Random.Next(10, 20);
            this.stepCounter = 0;
        }

        public Alien Alien
        {
            get; set;
        }

        public bool IsAlive()
        {
            return this.moverThread.IsAlive;
        }

        public void Start()
        {
            this.isRunning = true;
            this.moverThread = new Thread(this.Move)
            {
                Name = "alienMover"
            };
            this.moverThread.Start();
        }

        public void Stop()
        {
            this.isRunning = false;
        }

        private void Move()
        {
            while (this.isRunning)
            {
                Thread.Sleep(this.delay);
                switch (Alien.Direction)
                {
                    case Direction.Up:
                        Alien.Y--;
                        break;
                    case Direction.Down:
                        Alien.Y++;
                        break;
                    case Direction.Left:
                        Alien.X--;
                        break;
                    case Direction.Right:
                        Alien.X++;
                        break;
                }

                Alien.AutoCorrectDirection();

                if (this.isMediumLevel || this.isHardLevel)
                {
                    if (++this.stepCounter == this.stepsToBehaviourChange)
                    {
                        this.stepCounter = 0;
                        Alien.Direction = DirectionUtils.RandomDirection();

                        if (this.isHardLevel)
                        {
                            this.delay = Random.Next(100, 800);
                        }
                    }
                }
            }
        }
    }
}
