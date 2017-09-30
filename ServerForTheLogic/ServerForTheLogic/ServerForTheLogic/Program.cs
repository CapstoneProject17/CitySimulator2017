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
        private const int MEAN_DEATH_AGE = 80;
        private const int STANDARD_DEVIATION_DEATH = 14;

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
        public void setDeathAge() {
            Random random = new Random();
            int months = random.Next(1, 13);
            int days = random.Next(1, 30);
            /*int a=0;
            int b=0;
            int c=0;
            int d=0;
            int e=0;
            int f=0;
            int g=0;*/
            double[] randAr = new double[2000];
            Random rand = new Random(); //reuse this if you are generating many
            for (int i = 0; i < 2000; i++) {
                double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
                double u2 = 1.0 - rand.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                double randNormal = MEAN_DEATH_AGE + STANDARD_DEVIATION_DEATH * randStdNormal; //random normal(mean,stdDev^2)

                /* if (randNormal < 50)
                      a++;
                  else if (randNormal > 50 && randNormal < 60)
                      b++;
                  else if (randNormal > 60 && randNormal < 70)
                      c++;
                  else if (randNormal > 70 && randNormal < 80)
                      d++;
                  else if (randNormal > 80 && randNormal < 90)
                      e++;
                  else if (randNormal > 90 && randNormal < 100)
                      f++;
                  else
                      g++;*/
                //Console.WriteLine(randNormal);
                randAr[i] = randNormal;
            }
            /*Console.WriteLine("less than 50: " + a);
            Console.WriteLine("50-60: " + b);
            Console.WriteLine("60-70: " + c);
            Console.WriteLine("70-80: " + d);
            Console.WriteLine("80-90: " + e);
            Console.WriteLine("90-100: " + f);
            Console.WriteLine("100+: " + g);*/
            /*double total = 0;
            for (int i = 0; i < 1000; i++)
            {
                total += randAr[i];
            }*/
            //Console.Write(total / 1000);
        }
    }
}
        