using System;
using AutoFixture;
using AutoFixture.Xunit2;

namespace DojoDDD.UnitTests.Fixtures
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : this(FixtureFactory.Create) { }
        public AutoNSubstituteDataAttribute(Func<IFixture> fixtureFactory) : base(fixtureFactory) { }
    }
}
