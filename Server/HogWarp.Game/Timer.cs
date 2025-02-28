namespace HogWarp.Game
{
    public class Timer
    {
        public delegate void TickDelegate(float delta);

        private class TickInfo
        {
            public TickDelegate Tick;
            public float Interval;
            public float ElapsedTime;

            public TickInfo(TickDelegate tick, float interval)
            {
                Tick = tick;
                Interval = interval;
                ElapsedTime = 0;
            }
        }

        private List<TickInfo> ticks;

        protected Timer()
        {
            ticks = new List<TickInfo>();
        }

        public void Add(TickDelegate t, float interval)
        {
            ticks.Add(new TickInfo(t, interval));
        }

        public void Remove(TickDelegate t)
        {
            ticks.RemoveAll(ti => ti.Tick == t);
        }

        protected void Update(float delta)
        {
            for (int i = 0; i < ticks.Count; i++)
            {
                var tickInfo = ticks[i];
                tickInfo.ElapsedTime += delta;

                if (tickInfo.ElapsedTime >= tickInfo.Interval)
                {
                    tickInfo.Tick(tickInfo.ElapsedTime);
                    tickInfo.ElapsedTime = 0;
                }
            }
        }
    }
}
