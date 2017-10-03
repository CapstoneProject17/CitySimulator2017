using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServerForTheLogic.Utilities
{
    class Clock
    {
        private Timer timer;

        private UInt32 netMinutes;
        public UInt32 NetMinutes
        {
            get
            {
                return netMinutes;
            }
        }

        public UInt32 MinuteComponent
        {
            get
            {
                return netMinutes % 60;
            }
        }

        private UInt32 netHours;
        public UInt32 NetHours
        {
            get
            {
                return netHours;
            }
        }

        public UInt32 HourComponent
        {
            get
            {
                return netHours % 24;
            }
        }

        private UInt32 netDays;
        public UInt32 NetDays
        {
            get
            {
                return netDays;
            }
        }

        public const int INTERVAL = 100;

        public Clock()
        {
            timer = new Timer();

            timer.Interval = INTERVAL;
            timer.Elapsed += tickMinute;

            timer.AutoReset = true;
            timer.Enabled = true;

            timer.Start();
        }

        private void tickMinute(Object source, ElapsedEventArgs e)
        {
            netMinutes++;
            Console.WriteLine("Mins:\t" + netMinutes);

            if (netMinutes / 60 > netHours)
            {
                tickHour();
            }
        }

        private void tickHour()
        {
            netHours = netMinutes / 60;
            Console.WriteLine("Hours:\t" + netHours);

            if (netHours / 24 > netDays)
            {
                tickDay();
            }
        }

        private void tickDay()
        {
            netDays = netHours / 24;
            Console.WriteLine("Days:\t" + netDays);
        }
    }
}
