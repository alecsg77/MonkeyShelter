using MonkeyShelter.Data;
using Xunit;

namespace MonkeyShelter.Tests.Data
{
    public class MonkeyCollectionBehaviors
    {
        [Fact]
        public void MonkeyCollection_IsNotEmpty()
        {
            Assert.NotEmpty(MonkeyCollection.Monkeys);
        }
    }
}
