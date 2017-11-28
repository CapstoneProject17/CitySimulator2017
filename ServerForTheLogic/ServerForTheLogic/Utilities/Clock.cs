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
using ServerForTheLogic.Econ;
using CitySimNetworkService;

namespace ServerForTheLogic.Utilities
{
    [JsonObject(MemberSerialization.OptIn)]
    /// <summary>
    /// Holds the current time.  Intended for use by the City.
    /// <para/> Last edited:  2017-10-02
    /// </summary>
    public class Clock
    {
        Updater<ClientPacket> FullUpdater;
        Updater<Dictionary<int, Dictionary<Guid, Point>>> PartialUpdater;

        [JsonProperty]
        // Ticks every second to update the current time values.
        public Timer timer;
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
        /// The number of milliseconds between Clock "ticks."  In this case, 1 second = 1000.
        /// </summary>
        public const int INTERVAL = 100;

        /// <summary>
        /// Constructs a Clock object.
        /// <para>Written by Andrew Busto </para>
        /// </summary>
        public Clock(City city, SimulationStateQueue full, SimulationStateQueue partial)
        {
            this.city = city;
            timer = new Timer();
            PartialUpdater = new Updater<Dictionary<int, Dictionary<Guid, Point>>>(full, partial);
            FullUpdater = new Updater<ClientPacket>(full, partial);

            timer.Interval = INTERVAL;
            timer.Elapsed += TickMinute;

            timer.AutoReset = true;
            timer.Enabled = true;

            timer.Start();

        }

        /// <summary>
        /// Increments netMins.  Set as an event handler for timer.
        /// </summary>
        /// <para>Written by Andrew Busto </para>
        /// <para>Last modified by Justin McLennan 2017-11-21</para>
        /// <param name="source"> Unused. </param>
        /// <param name="e"> Unused .</param>
        private void TickMinute(Object source, ElapsedEventArgs e)
        {
            netMinutes++;
            //Console.WriteLine("Mins:\t" + netMinutes);

            if (netMinutes / 60 > netHours)
            {
                TickHour();
            }
        }

        /// <summary>
        /// Updates netHours. Calling methods that are run every hour
        /// <para>Written by Andrew Busto </para>
        /// <para>Last modified by Justin McLennan 2017-11-21</para>
        /// </summary>
        /// <para/> Last edited:  2017-11-12
        private void TickHour()
        {

            netHours = netMinutes / 60;
            Console.WriteLine("Hours:\t" + netHours);
            //Updater<Dictionary<Guid, Point>> updater = new Updater<Dictionary<Guid, Point>>();

            Console.WriteLine("Population = " + city.AllPeople.Count);
            foreach (Person p in city.AllPeople)
            {
                p.ConsumeProd();
            }

            //Console.WriteLine("Market checker " + Market.BusinessesHiring.Count);

            if (netHours / 24 > netDays)
            {
                TickDay();
            }

            PartialUpdater.SendPartialUpdate(city.PartialUpdateList, Formatting.None);
        }


        /// <summary>
        /// Updates netDays.
        /// </summary>
        /// <para>Written by Andrew Busto </para>
        /// <para/> Last edited:  2017-11-07
        private void TickDay()
        {
            foreach (Person p in city.AllPeople)
            {
                if (p.AgeDeathTick())
                {
                    city.AllPeople.Remove(p);
                }
            }
            netDays = netHours / 24;//send nudes
            Console.WriteLine("Days:\t" + netDays);
            if (netDays / 365 > netYears)
            {
                TickYear();
            }

            FullUpdater.SendFullUpdate(new ClientPacket(city), Formatting.Indented);
        }

        /// <summary>
        /// Updates netYears.
        /// </summary>
        /// <para>Written by Andrew Busto </para>
        /// <para/> Last edited:  2017-11-07
        private void TickYear()
        {
            netYears++;
            Console.WriteLine("Years:\t" + netYears);
            foreach (Person p in city.AllPeople)
            {
                p.SetAge();
            }
        }
    }
}
