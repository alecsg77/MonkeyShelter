using System;
using MonkeyShelter.Data;
using Xunit;

namespace MonkeyShelter.Tests
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
