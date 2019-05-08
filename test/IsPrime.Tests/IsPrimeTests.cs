using IsPrime.Controllers;
using NUnit.Framework;

namespace IsPrimeTests
{
    public class IsPrimeTests
    {
        private MathsController _mathsController;
        
        [SetUp]
        public void Setup()
        {
            _mathsController = new MathsController();
        }
       
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(53)]
        [TestCase(89)]
        public void ShouldReturnTrueForPrimeNumbers(int number)
        {
            var actualResult = _mathsController.IsPrime(number);

            Assert.True(actualResult);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(52)]
        [TestCase(99)]
        public void ShouldReturnFalseForNonPrimeNumbers(int number)
        {
            var actualResult = _mathsController.IsPrime(number);

            Assert.False(actualResult);
        }
    }
}