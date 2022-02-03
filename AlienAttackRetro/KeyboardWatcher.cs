namespace AlienAttackRetro
{
    using System;
    using System.Threading;

    public class KeyboardWatcher
    {
        private Thread watcherThread;
        private bool isRunning;
        public event EventHandler<KeyboardPressedEvent> KeyPressed;

        public void Start()
        {
            this.isRunning = true;
            this.watcherThread = new Thread(this.Watch)
            {
                Name = "Keyboardwatcher"
            };

            this.watcherThread.Start();
        }

        public void HandleShutdowcode(object sender, int shutDownCode)
        {
            this.isRunning = false;
        }

        private void Watch()
        {
            while (this.isRunning)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (this.KeyPressed == null)
                {
                    continue;
                }

                this.KeyPressed(this, new KeyboardPressedEvent(cki));
            }
        }
    }
}
