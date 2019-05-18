using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;

namespace IsPrimeTests
{
    public class IsPrimeTests
    {        
        private HttpClient _serviceUnderTest;
        
        [SetUp]
        public void Setup()
        {
            var builder = new WebHostBuilder()
                .UseStartup<IsPrime.Startup>();
            
            var server = new TestServer(builder);

            _serviceUnderTest = server.CreateClient();
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(53)]
        [TestCase(89)]
        public async Task ShouldReturnTrueForPrimeNumbers(int number)
        {
            var actualResult = await _serviceUnderTest.GetAsync($"/IsPrime/{number}");

            var result = await actualResult.Content.ReadAsStringAsync();
            
            Assert.True(bool.Parse(result));
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(52)]
        [TestCase(99)]
        public async Task ShouldReturnFalseForNonPrimeNumbers(int number)
        {
            var actualResult = await _serviceUnderTest.GetAsync($"/IsPrime/{number}");

            var result = await actualResult.Content.ReadAsStringAsync();

            Assert.False(bool.Parse(result));
        }
//
//        [Test]
//        public void ShouldFail()
//        {
//            Assert.Fail();
//        }
    }
}