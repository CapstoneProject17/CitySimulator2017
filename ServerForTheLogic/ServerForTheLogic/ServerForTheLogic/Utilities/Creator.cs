using Bogus;
using ServerForTheLogic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bogus.DataSets.Name;

namespace ServerForTheLogic.Utilities
{
    class Creator

    {
        public Person createPerson()
        {
          
            var modelFaker = new Faker<Person>("en")
                .RuleFor(o => o.Gender, f => f.PickRandom<Gender>())
                .RuleFor(o => o.FName, (f,o) => f.Name.FirstName(o.Gender))
                .RuleFor(o => o.LName, (f,o) => f.Name.LastName(o.Gender));
            
            return modelFaker.Generate(); 
        }

        public Industrial createIndustrialBuilding()
        {
            var modelFaker = new Faker<Industrial>()
                .RuleFor(o => o.Name, f => f.Company.CompanyName());
            return modelFaker.Generate();
        }
        public Commercial createCommercialBuilding()
        {
            var modelFaker = new Faker<Commercial>()
                .RuleFor(o => o.Name, f => f.Company.CompanyName());
            return modelFaker.Generate();
        }

        public Residential createResidentialBuilding()
        {
            var modelFaker = new Faker<Residential>();
            return modelFaker.Generate();
        }

        
    }
}
