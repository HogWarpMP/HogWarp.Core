using HogWarp.Game;
using HogWarp.Game.System;
using HogWarp.Game.World;
using HogWarp.Replicated;
using Serilog;

namespace HogWarp.Weather
{
    public class Plugin : IPlugin
    {
        public string Author => "HogWarp Team";
        public string Name => "HogWarpWeather";
        public Version Version => new(1, 0);
        enum eSeasons
        {
            Invalid = 0,
            Fall = 1,
            Winter = 2,
            Spring = 3,
            Summer = 4,
        }

        private eSeasons _currentESeason = eSeasons.Fall;
        private string currentWeather = "StormyLarge_01";
        private DateTime currentTime = new DateTime(1980, 1, 1, 6, 0, 0);
        private ILogger Logger = Log.Logger.ForContext<Plugin>();
        private BP_HogWarpWeather? weatherActor;

        public Plugin()
        {
        }

        private void PlayerSystem_PlayerJoinEvent(Player Id)
        {
            if (weatherActor != null)
            {
                weatherActor.UpdateTime(Id, currentTime.Hour, currentTime.Minute, currentTime.Second);
                weatherActor.UpdateSeason(Id, (int)_currentESeason);
                weatherActor.UpdateWeather(Id, currentWeather);
            }
        }

        public void PostLoad() 
        {
            weatherActor = Server.Level.Spawn<BP_HogWarpWeather>()!;
            weatherActor.Plugin = this;

            Server.Level.Players.PlayerJoinEvent += PlayerSystem_PlayerJoinEvent;
            Server.Timer.Add(Timer_Elapsed, 60.0f);

            Logger.Information($"The time is {currentTime.TimeOfDay.ToString()}, the weather is: {currentWeather}, and the season: {_currentESeason.ToString()}");
        }

        private void Timer_Elapsed(float delta)
        {
            currentTime = currentTime.AddMinutes(1);
            SendTimeUpdate();
            if (weatherActor != null)
            {
                var syncPlayer = Server.Level.Players.FirstOrDefault();
                if(syncPlayer != null)
                {
                    weatherActor.RequestWeather(syncPlayer);
                }
            }
        }

        public void Shutdown() 
        {

        }

        public void SetWeather(string weather)
        {
            if (weather != currentWeather)
            {
                currentWeather = weather;

                if (weatherActor != null)
                {
                    foreach (var p in Server.Level.Players)
                    {
                        weatherActor.UpdateWeather(p, currentWeather);
                    }
                }
            }
        }

        public void SetSeason(int season)
        {
            if (season != (int)_currentESeason)
            {
                _currentESeason = (eSeasons)season;

                if (weatherActor != null)
                {
                    foreach (var p in Server.Level.Players)
                    {
                        weatherActor.UpdateSeason(p, (int)_currentESeason);
                    }
                }
            }
        }

        public void SetTime(int hour, int min, int sec)
        {
            var newTime = new DateTime(1980, 1, 1, hour, min, sec);

            if (newTime != currentTime)
            {
                currentTime = newTime;

                if (weatherActor != null)
                {
                    foreach (var p in Server.Level.Players)
                    {
                        weatherActor.UpdateTime(p, currentTime.Hour, currentTime.Minute, currentTime.Second);
                    }
                }
            }
        }

        private void SendTimeUpdate()
        {
            if (weatherActor != null)
            {
                foreach (var p in Server.Level.Players)
                {
                    weatherActor.UpdateTime(p, currentTime.Hour, currentTime.Minute, currentTime.Second);
                }
            }
        }
    }
}

namespace HogWarp.Replicated
{
    public partial class BP_HogWarpWeather
    {
        internal Weather.Plugin? Plugin { get; set; }
        public partial void SendWeather(Player player, string Weather)
        {
            Plugin!.SetWeather(Weather);
        }

        public partial void SendSeason(Player player, int Season)
        {
            Plugin!.SetSeason(Season);
        }

        public partial void SendTime(Player player, int Hour, int Minute, int Second)
        {
            Plugin!.SetTime(Hour, Minute, Second);
        }
    }
}
