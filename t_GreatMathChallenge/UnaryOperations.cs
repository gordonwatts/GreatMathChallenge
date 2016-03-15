using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreatMathChallenge;

namespace t_GreatMathChallenge
{
    [TestClass]
    public class UnaryOperations
    {
        [TestMethod]
        public void FactorialZero()
        {
            var n = new Program.Number(0);
            var f = new Program.Factorial(n);
            Assert.AreEqual(1.0, f.Calc());
        }

        class MyNaN : Program.ITerm
        {
            public double Calc()
            {
                return double.NaN;
            }

            public bool isNumber()
            {
                return true;
            }
        }

        [TestMethod]
        public void FactorialOfNaN()
        {
            var n = new MyNaN();
            var f = new Program.Factorial(n);
            Assert.IsTrue(double.IsNaN(f.Calc()));
        }

        [TestMethod]
        public void LargeNumber()
        {
            // ((-1 + |8|)*(9!*|6|))! = 0
            var nineFactorial = new Program.Factorial(new Program.Number(9));
            var sixAbs = new Program.AbsoluteValue(new Program.Number(6));
            var t2 = new Program.Multiplication(nineFactorial, sixAbs);

            Assert.AreEqual(2177280.0, t2.Calc());

            var minus1 = new Program.MinusSign(new Program.Number(1));
            var eightAbs = new Program.AbsoluteValue(new Program.Number(8));
            var t1 = new Program.Plus(minus1, eightAbs);

            Assert.AreEqual(7.0, t1.Calc());

            var term = new Program.Multiplication(t1, t2);
            Assert.AreEqual(7.0 * 2177280.0, term.Calc());

            var final = new Program.Factorial(term);

            Assert.AreEqual("((-1 + |8|)*(9!*|6|))!", final.ToString());
            Assert.IsTrue(double.IsNaN(final.Calc()));
        }

        [TestMethod]
        public void NotZero()
        {
            // 0 = ((189) + 6)!
            var plus = new Program.Plus(new Program.Number(189), new Program.Number(6));
            var fac = new Program.Factorial(plus);

            Assert.IsTrue(fac.isOK());

            Assert.AreNotEqual(0.0, fac.Calc());
        }
    }
}
