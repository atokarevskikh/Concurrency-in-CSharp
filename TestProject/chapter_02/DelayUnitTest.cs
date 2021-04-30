using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestProject
{
    public class DelayUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestDelayResult()
        {
            var delay = new ConsoleUI.chapter_02.Delay();
            int delayInSeconds = 1;
            int resultShouldBe = 42;
            var result = await delay.DelayResult(resultShouldBe, TimeSpan.FromSeconds(delayInSeconds));
            Assert.AreEqual(result, resultShouldBe);
        }
    }
}