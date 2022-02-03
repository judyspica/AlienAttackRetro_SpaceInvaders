namespace AlienAttackRetro
{
    using System;

    public class KeyboardPressedEvent : EventArgs
    {
        public KeyboardPressedEvent(ConsoleKeyInfo cki)
        {
            this.KeyInfo = cki;
        }

        public ConsoleKeyInfo KeyInfo { get; private set; }

        public ConsoleKey Key
        {
            get
            {
                return this.KeyInfo.Key;
            }
        }

        public char KeyChar
        {
            get
            {
                return this.KeyInfo.KeyChar;
            }
        }
    }
}
