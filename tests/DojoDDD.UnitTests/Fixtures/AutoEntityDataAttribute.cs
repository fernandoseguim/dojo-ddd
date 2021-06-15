using System;
using System.Linq;
using AutoFixture;
using Bogus;
using DojoDDD.Domain.Entities;

namespace DojoDDD.UnitTests.Fixtures
{
    public class AutoEntityDataAttribute : AutoNSubstituteDataAttribute
    {
        public AutoEntityDataAttribute(Type type) : base(() => FixtureFactory(type)) { }

        private static IFixture FixtureFactory(Type type)
        {
            Faker faker = new("pt_BR");

            var fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            fixture.RepeatCount = 1;

            if(type == typeof(PurchaseOrder))
            {

            }

            return fixture;
        }
    }
}