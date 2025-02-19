using HogWarp.Replicated;
using HogWarpSdk.Game;
using HogWarpSdk.Systems;

namespace HogWarp.Weather
{
    public class Plugin : HogWarpSdk.IPlugin
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
        private Logger log = new HogWarpSdk.Systems.Logger("HogWarpWeather");
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
            weatherActor = HogWarpSdk.Server.World.Spawn<BP_HogWarpWeather>()!;
            weatherActor.Plugin = this;
            
            HogWarpSdk.Server.PlayerSystem.PlayerJoinEvent += PlayerSystem_PlayerJoinEvent;
            HogWarpSdk.Server.Timer.Add(Timer_Elapsed, 60.0f);

            log.Info($"The time is {currentTime.TimeOfDay.ToString()}, the weather is: {currentWeather}, and the season: {_currentESeason.ToString()}");
        }

        private void Timer_Elapsed(float delta)
        {
            currentTime.AddMinutes(1);
            SendTimeUpdate();
            if (weatherActor != null)
            {
                if(HogWarpSdk.Server.PlayerSystem.Players.First() != null)
                {
                    weatherActor.RequestWeather(HogWarpSdk.Server.PlayerSystem.Players.First());
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
                    foreach (var p in HogWarpSdk.Server.PlayerSystem.Players)
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
                    foreach (var p in HogWarpSdk.Server.PlayerSystem.Players)
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
                    foreach (var p in HogWarpSdk.Server.PlayerSystem.Players)
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
                foreach (var p in HogWarpSdk.Server.PlayerSystem.Players)
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
        public partial void SendWeather(HogWarpSdk.Game.Player player, string Weather)
        {
            Plugin!.SetWeather(Weather);
        }

        public partial void SendSeason(HogWarpSdk.Game.Player player, int Season)
        {
            Plugin!.SetSeason(Season);
        }

        public partial void SendTime(HogWarpSdk.Game.Player player, int Hour, int Minute, int Second)
        {
            Plugin!.SetTime(Hour, Minute, Second);
        }
    }
}
