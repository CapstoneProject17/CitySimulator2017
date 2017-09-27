using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.Extensions.Canada;
using Bogus;
using ConsoleDump;

namespace ServerForTheLogic {
    class Program {
        public static List<Person> People = new List<Person>();

        static void Main(string[] args) {
            //Person me = new Person();
            //me.Dump();
            Person me = new Person();
            for (int i = 0; i < 24; i++) {
                me = new Person();
                me.createPerson();
                People.Add(me);
                me = null;
            }
            People.Dump();
            me = new Person();
            me.KeepOpen();
        }

        public class Person {
            public string FName { get; set; }
            public string LName { get; set; }

            public void createPerson() {
                var modelFaker = new Faker<Person>()
                    .RuleFor(o => o.FName, f => f.Name.FirstName())
                    .RuleFor(o => o.LName, f => f.Name.LastName());

                var Human = modelFaker.Generate();
                FName = Human.FName;
                LName = Human.LName;
                //Human.Dump();
            }
            public void KeepOpen() {
                while (true) {
                }
            }


        }
    }
}
        