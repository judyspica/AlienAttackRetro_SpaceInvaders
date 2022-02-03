namespace AlienAttackRetro
{
    using System;

    public class Alien
    {
        private int x;
        private int y;

        private Alien(int x, int y, int healthPoints, Direction direction)
        {
            this.x = x;
            this.y = y;
            this.HealthPoints = healthPoints;
            Direction = direction;
        }

        public event EventHandler<AlienPositionChangedEvent> PositionChanged;

        public Direction Direction
        {
            get; set;
        }

        public int HealthPoints
        {
            get; set;
        }

        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                if (value == this.X || value < 0)
                {
                    return;
                }

                this.OnPositionChanged(this.X, this.Y, value, this.Y);
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                if (value == this.Y || value < 0)
                {
                    return;
                }

                this.OnPositionChanged(this.X, this.Y, this.X, value);
                this.y = value;
            }
        }

        public static Alien CREATE(int x, int y, int healthPoints, Direction direction)
        {
            return new Alien(x, y, healthPoints, direction);
        }

        public char ConvertHealthToChar(int healthPoints)
        {
            switch (healthPoints)
            {
                case 1: return '1';
                case 2: return '2';
                case 3: return '3';
                case 4: return '4';
                case 5: return '5';
                case 6: return '6';
                case 7: return '7';
                case 8: return '8';
                case 9: return '9';
                case 10: return 'A';
                case 11: return 'B';
                case 12: return 'C';
                case 13: return 'D';
                case 14: return 'E';
                case 15: return 'F';
                default: return ' ';
            }
        }

        public void AutoCorrectDirection()
        {
            switch (Direction)
            {
                case Direction.Left:
                    if (this.X <= 0)
                    {
                        Direction = Direction.Right;
                    }

                    break;
                case Direction.Right:
                    if (this.X >= Console.WindowWidth - 1)
                    {
                        Direction = Direction.Left;
                    }

                    break;
                case Direction.Up:
                    if (this.Y <= 0)
                    {
                        Direction = Direction.Down;
                    }

                    break;
                case Direction.Down:
                    if (this.Y >= Console.WindowHeight - 2)
                    {
                        Direction = Direction.Up;
                    }

                    break;
            }
        }

        private ConsoleColor ConvertToColor(int healthPoints)
        {
            switch (healthPoints)
            {
                case 1: return ConsoleColor.DarkRed;
                case 2: return ConsoleColor.DarkRed;
                case 3: return ConsoleColor.Red;
                case 4: return ConsoleColor.Red;
                case 5: return ConsoleColor.DarkYellow;
                case 6: return ConsoleColor.DarkYellow;
                case 7: return ConsoleColor.Yellow;
                case 8: return ConsoleColor.Yellow;
                case 9: return ConsoleColor.DarkCyan;
                case 10: return ConsoleColor.DarkCyan;
                case 11: return ConsoleColor.Cyan;
                case 12: return ConsoleColor.Cyan;
                case 13: return ConsoleColor.DarkGreen;
                case 14: return ConsoleColor.DarkGreen;
                case 15: return ConsoleColor.Green;
                default: return ConsoleColor.White;
            }
        }

        private void OnPositionChanged(int oldX, int oldY, int newX, int newY)
        {
            if (this.PositionChanged != null)
            {
                this.PositionChanged(this, new AlienPositionChangedEvent(oldX, oldY, newX, newY, Direction, this.ConvertToColor(this.HealthPoints), this.ConvertHealthToChar(this.HealthPoints)));
            }
        }
    }
}
