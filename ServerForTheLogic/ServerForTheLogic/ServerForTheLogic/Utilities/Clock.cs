using Newtonsoft.Json;
using ServerForTheLogic.Infrastructure;
using ServerForTheLogic.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ConsoleDump;

namespace ServerForTheLogic.Utilities
{
    [JsonObject(MemberSerialization.OptIn)]
    /// <summary>
    /// Holds the current time.  Intended for use by the City.
    /// <para/> Last edited:  2017-10-02
    /// </summary>
    class Clock
    {
        [JsonProperty]
        // Ticks every second to update the current time values.
        private Timer timer;
        private City city;

        /// <summary>
        /// The number of minutes that have passed in the current hour.
        /// minutes are 0.5 seconds
        /// </summary>
        [JsonProperty]
        public UInt32 netMinutes { get; set; }

        /// <summary>
        /// The total number of hours since this Clock started.
        /// hours are 30 seconds
        /// </summary>
        [JsonProperty]
        public UInt32 netHours { get; set; }

        /// <summary>
        /// The total number of days since this Clock started.
        /// days are 12 min
        /// </summary>
        [JsonProperty]
        private UInt32 netDays { get; set; }

        /// <summary>
        /// The total number of years since this Clock started.
        /// </summary>
        [JsonProperty]
        private UInt32 netYears { get; set; }

        [JsonProperty]
        /// <summary>
        /// The number of milliseconds between Clock "ticks."  In this case, 1 second.
        /// </summary>
        public const int INTERVAL = 500;

        /// <summary>
        /// Constructs a Clock object.
        /// <para/> Last edited:  2017-10-02
        /// </summary>
        public Clock(City city)
        {
            this.city = city;
            timer = new Timer();

            timer.Interval = INTERVAL;
            timer.Elapsed += tickMinute;

            timer.AutoReset = true;
            timer.Enabled = true;

            timer.Start();
        }

        /// <summary>
        /// Increments netMins.  Set as an event handler for timer.
        /// </summary>
        /// <para/> Last edited:  2017-10-02
        /// <param name="source"> Unused. </param>
        /// <param name="e"> Unused .</param>
        private void tickMinute(Object source, ElapsedEventArgs e)
        {
            netMinutes++;
            Console.WriteLine("Mins:\t" + netMinutes);

            if (netMinutes / 10 > netHours)
            {
                tickHour();
            }
        }

        /// <summary>
        /// Updates netHours.
        /// </summary>
        /// <para/> Last edited:  2017-10-02
        private void tickHour()
        {
            netHours = netMinutes / 10;
            Console.WriteLine("Hours:\t" + netHours);
            Updater<Dictionary<Guid, Point>> updater = new Updater<Dictionary<Guid, Point>>();
            //error

            for (int i = 0; i < 24; i++)
            {
                updater.sendPartialUpdate(
                city.PartialUpdateList[i], //gets all persons that have move
                Newtonsoft.Json.Formatting.None
                );
            }
            if (netHours / 24 > netDays)
            {
                tickDay();
            }
        }

        /// <summary>
        /// Updates netDays.
        /// </summary>
        /// <para/> Last edited:  2017-11-07
        private void tickDay()
        {
            foreach (Person p in city.AllPeople)
            {
                if (p.AgeDeathTick())
                {
                    city.AllPeople.Remove(p);
                }
            }

            netDays = netHours / 24;//send nudes
            //Console.WriteLine("Days:\t" + netDays);
            if (netDays / 3 > netYears)
            {
                tickYear();
            }
            //Updater updater = new Updater();
            //updater.SendDailyUpdate(DATA);
        }

        /// <summary>
        /// Updates netYears.
        /// </summary>
        /// <para/> Last edited:  2017-11-07
        private void tickYear()
        {
            netYears++;
            //Console.WriteLine("Years:\t" + netYears);
            foreach (Person p in city.AllPeople)
            {
                p.SetAge();
            }
        }
    }
}
