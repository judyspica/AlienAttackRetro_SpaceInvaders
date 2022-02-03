namespace AlienAttackRetro
{
    using System;
    using System.Collections.Generic;

    public class PrintService
    {
        private List<CrossHairPixel> cachePixels = new List<CrossHairPixel>();
        private ConsoleColor crossHairColor;

        public bool Print
        {
            get; set;
        }

        public void PrintTimer(object sender, TimeSpan timeSpan)
        {
            this.PrintPixel(new List<PrintPixelRequest>()
            {
                new PrintPixelRequest()
                {
                    X = 0,
                    Y = Console.WindowHeight - 1,
                    Symbol = "Time is running " + timeSpan.ToString(@"mm\:ss") + " left",
                    Color = ConsoleColor.White,
                },
            });
        }

        public void UpdateCrossHairPixels(List<CrossHairPixel> pixels)
        {
            this.cachePixels = pixels;
        }

        public void UpdateCrossHairColor(ConsoleColor color)
        {
            this.crossHairColor = color;
            this.PrintCrosshair();
        }

        public void ClearOldCrossHairPixels()
        {
            lock (this)
            {
                foreach (CrossHairPixel pixel in this.cachePixels)
                {
                    if (pixel.X < 0 || pixel.X >= Console.WindowWidth || pixel.Y < 0 || pixel.Y >= Console.WindowHeight - 1)
                    {
                        continue;
                    }

                    Console.SetCursorPosition(pixel.X, pixel.Y);
                    Console.Write(" ");
                }
            }
        }

        public void PrintCrosshair()
        {
            lock (this)
            {
                for (int index = 0; index < this.cachePixels.Count; index++)
                {
                    CrossHairPixel pixel = this.cachePixels[index];
                    if (pixel.X < 0 || pixel.X >= Console.WindowWidth || pixel.Y < 0 || pixel.Y >= Console.WindowHeight - 1)
                    {
                        continue;
                    }

                    Console.ForegroundColor = this.crossHairColor;
                    Console.SetCursorPosition(pixel.X, pixel.Y);
                    Console.Write(pixel.Type);
                }
            }
        }

        public void HanldeAlienPositionChangedEvent(object sender, AlienPositionChangedEvent positionChangedEvent)
        {
            List<PrintPixelRequest> requests = new List<PrintPixelRequest>()
            {
                new PrintPixelRequest()
                {
                    X = positionChangedEvent.NewX,
                    Y = positionChangedEvent.NewY,
                    Symbol = string.Empty + positionChangedEvent.Symbol,
                    Color = positionChangedEvent.Color,
                },
                new PrintPixelRequest()
                {
                    X = positionChangedEvent.OldX,
                    Y = positionChangedEvent.OldY,
                    Symbol = " "
                }
            };

            this.PrintPixel(requests);
        }

        private void PrintPixel(List<PrintPixelRequest> requests)
        {
            if (!this.Print)
            {
                return;
            }

            try
            {
                lock (this)
                {
                    foreach (PrintPixelRequest request in requests)
                    {
                        Console.ForegroundColor = request.Color;
                        Console.SetCursorPosition(request.X, request.Y);
                        Console.Write(request.Symbol);
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // This is in case alien reaches boarder and needs to change direction (after number of steps) after direction has been changes to the opposite side because it has reached the boarder of console.
            }
        }
    }
}
