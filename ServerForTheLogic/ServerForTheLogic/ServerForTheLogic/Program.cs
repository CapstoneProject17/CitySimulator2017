using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.Extensions.Canada;
using Bogus;
using ConsoleDump;
using ServerForTheLogic.Utilities;
using ServerForTheLogic.Infrastructure;

namespace ServerForTheLogic {

    class Program {
        public static List<Person> People = new List<Person>();
        private const int MEAN_DEATH_AGE = 80;
        private const int STANDARD_DEVIATION_DEATH = 14;

        static void Main(string[] args) {
            ////Person me = new Person();
            ////me.Dump();
            //Person me = new Person();
            //for (int i = 0; i < 24; i++) {
            //    me = new Person();
            //   // me.createPerson();
            //    People.Add(me);
            //    me = null;
            //}
            //People.Dump();
            //me = new Person();
            //// me.KeepOpen();
            //KeepOpen();

            Creator creator = new Creator();
            for (int i = 0; i < 24; i++)
            {
                Person p = creator.createPerson();
                Console.WriteLine(p);
            }


            KeepOpen();
        }

        public static void KeepOpen()
        {
            while (true) ;
           
        }

        
    }
}
        