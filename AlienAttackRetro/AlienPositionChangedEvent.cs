namespace AlienAttackRetro
{
    using System;

    public class AlienPositionChangedEvent : EventArgs
    {
        public AlienPositionChangedEvent(int oldX, int oldY, int newX, int newY, Direction direction, ConsoleColor color, char symbol)
        {
            this.OldX = oldX;
            this.OldY = oldY;
            this.NewX = newX;
            this.NewY = newY;
            this.Direction = direction;
            this.Color = color;
            this.Symbol = symbol;
        }

        public int OldX
        {
            get;
            private set;
        }

        public int OldY
        {
            get;
            private set;
        }

        public int NewX
        {
            get;
            private set;
        }

        public int NewY
        {
            get;
            private set;
        }

        public Direction Direction
        {
            get;
            private set;
        }

        public ConsoleColor Color
        {
            get;
            private set;
        }

        public char Symbol
        {
            get; private set;
        }
    }
}
