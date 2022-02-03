namespace AlienAttackRetro
{
    using System;
    using System.Threading;

    public class Timer
    {
        private TimeSpan timeSpan;
        private Thread timerThread;
        public event EventHandler<TimeSpan> Tick;

        public bool IsRunning
        {
            get; set;
        }

        public void Init(TimeSpan timeSpan)
        {
            this.IsRunning = true;
            this.timeSpan = timeSpan;
            this.timerThread = new Thread(this.CountDown);
            this.timerThread.Start();
        }

        private void CountDown()
        {
            while (this.timeSpan >= TimeSpan.Zero && this.IsRunning)
            {
                this.Tick(this, this.timeSpan);
                this.timeSpan = this.timeSpan.Subtract(TimeSpan.FromSeconds(1));
                Thread.Sleep(1000);
            }
        }
    }
}
