using AutoFixture.Xunit2;
using Xunit;

namespace DojoDDD.UnitTests.Fixtures
{
    public class AutoInlineDataAttribute : CompositeDataAttribute
    {
        public AutoInlineDataAttribute(params object[] values) :
                base(new AutoNSubstituteDataAttribute(), new InlineDataAttribute(values)) { }
    }
}
