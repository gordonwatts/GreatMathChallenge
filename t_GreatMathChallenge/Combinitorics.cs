using GreatMathChallenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t_GreatMathChallenge
{
    [TestClass]
    public class Combinitorics
    {
        [TestMethod]
        public void UnaryFiniteCombination()
        {
            var n = new Program.Number(5);
            var all = Program.AllUnaryOperations(n).ToArray();
            foreach (var item in all)
            {
                Console.WriteLine(item);
            }
            Assert.AreEqual(15, all.Count());
        }

        [TestMethod]
        public void UnaryFiniteCombinationForOne()
        {
            var n = new Program.Number(1);
            var all = Program.AllUnaryOperations(n).ToArray();
            foreach (var item in all)
            {
                Console.WriteLine(item);
            }
            Assert.AreEqual(16, all.Count());
        }
    }
}
