using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEndTestProject
{
    using NUnit.Framework;

    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void Add_WhenGivenTwoNumbers_ReturnsSum()
        {
            // Arrange
            int a = 3;
            int b = 5;
            int expectedSum = 8;

            // Act
            int actualSum = MathHelper.Add(a, b);

            // Assert
            Assert.AreEqual(expectedSum, actualSum);
        }
        public static class MathHelper
        {
            public static int Add(int a, int b)
            {
                return a + b;
            }
        }
    }
}
