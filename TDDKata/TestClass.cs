using NUnit.Framework;

namespace TDDKata {
    [TestFixture]
    public class TestClass {
        [TestCase("1", ExpectedResult = 1)]
        [TestCase("10", ExpectedResult = 10)]
        public int ShouldReturnWith1Operand(string data) {
            // Arrange
            var calc = CalculatorFactory.Create();

            // Act
            var actual = calc.Calculate(data);

            // Assert
            return actual;
        }

        [TestCase("2 2 +", ExpectedResult = 4)]
        [TestCase("1 7 +", ExpectedResult = 8)]
        [TestCase("3 4 -", ExpectedResult = -1)]
        public int ShouldCalculateWith2Operands(string data) {
            // Arrange
            var calc = CalculatorFactory.Create();

            // Act
            var actual = calc.Calculate(data);

            // Assert
            return actual;
        }

        [TestCase("7 2 3 * -", ExpectedResult = 1)]
        [TestCase("1 4 5 * +", ExpectedResult = 21)]
        [TestCase("20 5 1 - /", ExpectedResult = 5)]
        public int ShouldCalculateWith3Operands(string data) {
            // Arrange
            var calc = CalculatorFactory.Create();

            // Act
            var actual = calc.Calculate(data);

            // Assert
            return actual;
        }

        [TestCase("1 2 + 3 + 4 + 5 10 * 7 1 - + +", ExpectedResult = 66)]
        [TestCase("30 1 2 * - 4 + 6 - 2 /", ExpectedResult = 13)]
        [TestCase("4 1 2 * - 4 + 6 - 2 /", ExpectedResult = 0)]
        public int ShouldCalculateWithMoreOperands(string data) {
            // Arrange
            var calc = CalculatorFactory.Create();

            // Act
            var actual = calc.Calculate(data);

            // Assert
            return actual;
        }

        [TestCase("1 2 3 * * *")]
        [TestCase("1 2 3 + 4 5")]
        [TestCase("+ -")]
        [TestCase("1 + 2")]
        [TestCase("+ 1 2")]
        [TestCase("2 2 ++")]
        [TestCase("2 2 2 ++")]
        [TestCase("1 + - ")]
        [TestCase("1 - ")]
        [TestCase("a b -")]
        [TestCase("a")]
        [TestCase("1 2 + a +")]
        [TestCase("1 2 + 3 a +")]
        [TestCase("10 20 + 0 /")]
        public void ShouldThrowException(string data) {
            // Arrange
            var calc = CalculatorFactory.Create();

            // Act & Assert
            Assert.Throws<StringCalcException>(() => calc.Calculate(data));
        }
    }
}
