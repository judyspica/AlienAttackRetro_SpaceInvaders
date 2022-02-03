namespace AlienAttackRetro
{
    using System;
    using System.Collections.Generic;

    public class CrossHair
    {
        private readonly PrintService printService;
        private readonly List<CrossHairPixel> pixels;
        private int x;
        private int y;

        public CrossHair(int x, int y, PrintService printService)
        {
            this.x = x;
            this.y = y;

            this.pixels = this.CreatePixels();
            this.printService = printService;
            this.printService.UpdateCrossHairPixels(this.pixels);
        }

        public void FireAt(List<AlienMover> alienMovers)
        {
            foreach (AlienMover mover in alienMovers)
            {
                if (mover.Alien.X == this.x && mover.Alien.Y == this.y)
                {
                    mover.Alien.HealthPoints--;
                    if (mover.Alien.HealthPoints == 0)
                    {
                        mover.Stop();
                    }
                }
            }
        }

        public void DecreaseX()
        {
            if (this.x != 0)
            {
                this.printService.ClearOldCrossHairPixels();
                this.x--;
                foreach (CrossHairPixel pixel in this.pixels)
                {
                    pixel.X--;
                }

                this.printService.UpdateCrossHairPixels(this.pixels);
            }
        }

        public void IncreaseX()
        {
            if (this.x != Console.WindowWidth - 1)
            {
                this.printService.ClearOldCrossHairPixels();
                this.x++;
                foreach (CrossHairPixel pixel in this.pixels)
                {
                    pixel.X++;
                }

                this.printService.UpdateCrossHairPixels(this.pixels);
            }
        }

        public void DecreaseY()
        {
            if (this.y != 0)
            {
                this.printService.ClearOldCrossHairPixels();
                this.y--;
                foreach (CrossHairPixel pixel in this.pixels)
                {
                    pixel.Y--;
                }

                this.printService.UpdateCrossHairPixels(this.pixels);
            }
        }

        public void IncreaseY()
        {
            if (this.y != Console.WindowHeight - 2)
            {
                this.printService.ClearOldCrossHairPixels();
                this.y++;
                foreach (CrossHairPixel pixel in this.pixels)
                {
                    pixel.Y++;
                }

                this.printService.UpdateCrossHairPixels(this.pixels);
            }
        }

        public void UpdateCrossHair(object sender, AlienPositionChangedEvent positionChangedEvent)
        {
            ConsoleColor color = positionChangedEvent.NewX == this.x && positionChangedEvent.NewY == this.y ? ConsoleColor.Red : ConsoleColor.White;
            this.printService.UpdateCrossHairColor(color);
        }

        private List<CrossHairPixel> CreatePixels()
        {
            List<string> crossHair = new List<string>()
            {
                "X         X",
                "           ",
                "    + +    ",
                "    + +    ",
                "  +++ +++  ",
                "           ",
                "  +++ +++  ",
                "    + +    ",
                "    + +    ",
                "           ",
                "X         X"
            };

            List<CrossHairPixel> pixels = new List<CrossHairPixel>();
            for (int lineIndex = 0; lineIndex < crossHair.Count; lineIndex++)
            {
                char[] lineChars = crossHair[lineIndex].ToCharArray();

                for (int charIndex = 0; charIndex < lineChars.Length; charIndex++)
                {
                    char c = lineChars[charIndex];
                    if (c != ' ')
                    {
                        pixels.Add(new CrossHairPixel()
                        {
                            X = this.x - 5 + lineIndex,
                            Y = this.y - 5 + charIndex,
                            Type = c
                        });
                    }
                }
            }

            return pixels;
        }
    }
}
