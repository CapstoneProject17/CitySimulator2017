using ServerForTheLogic.Infrastructure;
using ServerForTheLogic.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServerForTheLogic.Utilities
{
    /// <summary>
    /// Holds the current time.  Intended for use by the City.
    /// <para/> Last edited:  2017-10-02
    /// </summary>
    class Clock
    {
        // Ticks every second to update the current time values.
        private Timer timer;
        private City city;

        private UInt32 netMinutes;
        /// <summary>
        /// The total number of minutes since this Clock started.
        /// </summary>
        public UInt32 NetMinutes
        {
            get
            {
                return netMinutes;
            }
        }

        /// <summary>
        /// The number of minutes that have passed in the current hour.
        /// </summary>
        public UInt32 MinuteComponent
        {
            get
            {
                return netMinutes % 60;
            }
        }

        private UInt32 netHours;
        /// <summary>
        /// The total number of hours since this Clock started.
        /// </summary>
        public UInt32 NetHours
        {
            get
            {
                return netHours;
            }
        }

        /// <summary>
        /// The number of hours that have passed in the current day.
        /// </summary>
        public UInt32 HourComponent
        {
            get
            {
                return netHours % 24;
            }
        }

        private UInt32 netDays;
        /// <summary>
        /// The total number of days since this Clock started.
        /// </summary>
        public UInt32 NetDays
        {
            get
            {
                return netDays;
            }
        }

        /// <summary>
        /// The number of milliseconds between Clock "ticks."  In this case, 1 second.
        /// </summary>
        public const int INTERVAL = 1000;

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

            if (netMinutes / 60 > netHours)
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
            netHours = netMinutes / 60;
            Console.WriteLine("Hours:\t" + netHours);
            Updater<Dictionary<Guid, Point>> updater = new Updater<Dictionary<Guid, Point>>();
            updater.sendPartialUpdate(
                city.PartialUpdateList[(int)HourComponent], //gets all persons that have move
                Newtonsoft.Json.Formatting.None
                );
            if (netHours / 24 > netDays)
            {
                tickDay();
            }
        }

        /// <summary>
        /// Updates netDays.
        /// </summary>
        /// <para/> Last edited:  2017-10-02
        private void tickDay()
        {
            foreach (Person p in city.AllPeople)
            {
                if (p.Age())
                {
                    city.AllPeople.Remove(p);
                }
            }
            netDays = netHours / 24;
            Console.WriteLine("Days:\t" + netDays);
            //Updater updater = new Updater();
            //updater.SendDailyUpdate(DATA);
        }
    }
}
