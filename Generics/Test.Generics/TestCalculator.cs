using Generics;

namespace Test.Generics
{
    public class TestCalculator
    {
        private readonly Calculator<int> _intCalc = new Calculator<int>();
        private readonly Calculator<double> _doubleCalc = new Calculator<double>();

        [Fact]
        public void Add_Int_ReturnsSum()
        {
            Assert.Equal(5, _intCalc.Add(2, 3));
        }

        [Fact]
        public void Add_Double_ReturnsSum()
        {
            Assert.Equal(5.5, _doubleCalc.Add(2.2, 3.3), 5);
        }

        [Fact]
        public void Substract_Int_ReturnsDifference()
        {
            Assert.Equal(-1, _intCalc.Substract(2, 3));
        }

        [Fact]
        public void Substract_Double_ReturnsDifference()
        {
            Assert.Equal(-1.1, _doubleCalc.Substract(2.2, 3.3), 5);
        }

        [Fact]
        public void Multiply_Int_ReturnsProduct()
        {
            Assert.Equal(6, _intCalc.Multiply(2, 3));
        }

        [Fact]
        public void Multiply_Double_ReturnsProduct()
        {
            Assert.Equal(7.26, _doubleCalc.Multiply(2.2, 3.3), 5);
        }

        [Fact]
        public void Divide_Int_ReturnsQuotient()
        {
            Assert.Equal(2, _intCalc.Divide(6, 3));
        }

        [Fact]
        public void Divide_Double_ReturnsQuotient()
        {
            Assert.Equal(2.0, _doubleCalc.Divide(6.6, 3.3), 5);
        }

        [Fact]
        public void Divide_Int_ByZero_Throws()
        {
            Assert.Throws<DivideByZeroException>(() => _intCalc.Divide(1, 0));
        }

        [Fact]
        public void Divide_Double_ByZero_Throws()
        {
            Assert.Throws<DivideByZeroException>(() => _doubleCalc.Divide(1.0, 0.0));
        }
    }
}
