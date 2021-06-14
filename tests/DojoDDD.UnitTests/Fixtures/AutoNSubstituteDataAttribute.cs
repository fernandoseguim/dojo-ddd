using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using DojoDDD.Infra.DbContext;

namespace DojoDDD.UnitTests.Fixtures
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : this(FixtureFactory) { }
        public AutoNSubstituteDataAttribute(Func<IFixture> fixtureFactory) : base(fixtureFactory) { }

        private static IFixture FixtureFactory()
        {
            var fixture = new Fixture()
                    .Customize(new DataStoreCustomization())
                    .Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            fixture.RepeatCount = 1;

            return fixture;
        }

        private class DataStoreCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(() => new DataStore());
            }
        }
    }
}
