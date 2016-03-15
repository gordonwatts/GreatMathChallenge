﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace GreatMathChallenge
{
    public class Program
    {
        public interface ITerm
        {
            double Calc();

            /// <summary>
            /// Returns true if this is a normal number below - just a Number.
            /// </summary>
            /// <returns></returns>
            bool isNumber();
        }

        /// <summary>
        /// A simple number
        /// </summary>
        public class Number : ITerm
        {
            private int _n;

            public Number(int n)
            {
                _n = n;
            }

            /// <summary>
            /// Nice printout for the world
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return _n.ToString();
            }

            public double Calc()
            {
                return _n;
            }

            public bool isNumber()
            {
                return true;
            }
        }

        /// <summary>
        /// Single operand operations (unary)
        /// </summary>
        public interface IUnaryOperation : ITerm
        {
            // True if this is going to compute something sensible.
            // May cause evaluation of the arguments.
            bool isOK();
        }

        class PlusSign : IUnaryOperation
        {
            private ITerm _t1;

            public PlusSign(ITerm t1)
            {
                _t1 = t1;
            }

            public double Calc()
            {
                return _t1.Calc();
            }

            public override string ToString()
            {
                return _t1.ToString();
            }

            public bool isNumber()
            {
                return _t1.isNumber();
            }

            public bool isOK()
            {
                return true;
            }
        }

        public class MinusSign : IUnaryOperation
        {
            private ITerm _t1;

            public MinusSign(ITerm t1)
            {
                _t1 = t1;
            }

            public double Calc()
            {
                return -_t1.Calc();
            }

            public override string ToString()
            {
                return $"-{_t1}";
            }

            public bool isNumber()
            {
                return false;
            }

            public bool isOK()
            {
                return true;
            }
        }

        class SquareRoot : IUnaryOperation
        {
            private ITerm _t1;
            private double _t1Value;

            public SquareRoot(ITerm t1)
            {
                _t1 = t1;
                _t1Value = _t1.Calc();
            }

            public double Calc()
            {
                return Math.Sqrt(_t1Value);
            }

            public override string ToString()
            {
                return $"sq({_t1})";
            }

            public bool isNumber()
            {
                return false;
            }

            public bool isOK()
            {
                return !double.IsNaN(_t1Value) && !double.IsInfinity(_t1Value) && _t1Value > 0;
            }
        }

        public class AbsoluteValue : IUnaryOperation
        {
            private ITerm _t1;

            public AbsoluteValue(ITerm t1)
            {
                _t1 = t1;
            }

            public double Calc()
            {
                return Math.Abs(_t1.Calc());
            }

            public override string ToString()
            {
                return $"|{_t1}|";
            }

            public bool isNumber()
            {
                return false;
            }

            public bool isOK()
            {
                return true;
            }
        }

        public class Factorial : IUnaryOperation
        {
            private ITerm _t1;
            private double _value;

            public Factorial(ITerm t1)
            {
                _t1 = t1;
                _value = _t1.Calc();
            }

            public double Calc()
            {
                if (_value == 0)
                {
                    return 1;
                }

                var result = 1;
                for (int i = (int)_value; i > 0; i--)
                {
                    result *= i;
                }

                if (double.IsInfinity(result))
                {
                    return double.NaN;
                }
                return result;
            }

            public override string ToString()
            {
                return $"{_t1}!";
            }

            public bool isNumber()
            {
                return false;
            }

            public bool isOK()
            {
                if (_value != (int)_value || _value < 0)
                {
                    return false;
                }
                if (_value > 500)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 2 operand operations (binary)
        /// </summary>
        interface IBinaryOperation : ITerm
        {

        }

        public class Plus : IBinaryOperation
        {
            private ITerm _t1;
            private ITerm _t2;

            public Plus(ITerm t1, ITerm t2)
            {
                _t1 = t1;
                _t2 = t2;
            }

            public override string ToString()
            {
                return $"({_t1} + {_t2})";
            }

            public double Calc()
            {
                return _t1.Calc() + _t2.Calc();
            }

            public bool isNumber()
            {
                return false;
            }
        }

        class Minus : IBinaryOperation
        {
            private ITerm _t1;
            private ITerm _t2;

            public Minus(ITerm t1, ITerm t2)
            {
                _t1 = t1;
                _t2 = t2;
            }

            public override string ToString()
            {
                return $"({_t1} - {_t2})";
            }

            public double Calc()
            {
                return _t1.Calc() - _t2.Calc();
            }

            public bool isNumber()
            {
                return false;
            }
        }

        class Division : IBinaryOperation
        {
            private ITerm _t1;
            private ITerm _t2;

            public Division(ITerm t1, ITerm t2)
            {
                _t1 = t1;
                _t2 = t2;
            }

            public override string ToString()
            {
                return $"({_t1}/{_t2})";
            }

            public double Calc()
            {
                // We have protect things a little here.
                var o1 = _t1.Calc();
                var o2 = _t2.Calc();

                if (double.IsInfinity(o1) || double.IsInfinity(o2))
                {
                    return double.NaN;
                }

                var r = _t1.Calc() / _t2.Calc();
                if (r == 0 && o1 != 0)
                {
                    return double.NaN;
                }

                return r;
            }

            public bool isNumber()
            {
                return false;
            }
        }

        public class Multiplication : IBinaryOperation
        {
            private ITerm _t1;
            private ITerm _t2;

            public Multiplication(ITerm t1, ITerm t2)
            {
                _t1 = t1;
                _t2 = t2;
            }

            public override string ToString()
            {
                return $"({_t1}*{_t2})";
            }

            public double Calc()
            {
                return _t1.Calc() * _t2.Calc();
            }

            public bool isNumber()
            {
                return false;
            }
        }

        class Exponent : IBinaryOperation
        {
            private ITerm _t1;
            private ITerm _t2;

            public Exponent(ITerm t1, ITerm t2)
            {
                _t1 = t1;
                _t2 = t2;
            }

            public override string ToString()
            {
                return $"({_t1}^{_t2})";
            }

            public double Calc()
            {
                var o1 = _t1.Calc();
                var o2 = _t2.Calc();

                if (o2 < 0)
                {
                    return double.NaN;
                }

                var r = Math.Pow(o1, o2);
                if (double.IsInfinity(r))
                {
                    return double.NaN;
                }

                if (r == 0 && o1 != 0)
                {
                    return double.NaN;
                }

                return r;
            }

            public bool isNumber()
            {
                return false;
            }
        }

        class JoinNumbers : IBinaryOperation
        {
            private ITerm _t1;
            private ITerm _t2;

            /// <summary>
            /// Can only join original numbers!
            /// </summary>
            /// <param name="t1"></param>
            /// <param name="t2"></param>
            public JoinNumbers(ITerm t1, ITerm t2)
            {
                if (t1.isNumber() && t2.isNumber()) {
                    _t1 = t1;
                    _t2 = t2;
                } else
                {
                    throw new ArgumentException("Have to be numbers!");
                }
            }

            public override string ToString()
            {
                // We do this because we know we are just numbers
                // and sometimes extra "(" and ")" creep in...
                return $"({_t1.Calc()}{_t2.Calc()})";
            }

            public double Calc()
            {
                return int.Parse($"{_t1.Calc()}{_t2.Calc()}");
            }

            public bool isNumber()
            {
                return true;
            }
        }

        static void Main(string[] args)
        {
            WriteLine("Great Math Challenge!");

            // Numbers we can use
            int[] numbers = { 1, 8, 9, 6 };
            Write($"Numbers we are using: ");
            foreach (var n in numbers)
            {
                Write($"{n} ");
            }
            WriteLine();

            ///
            /// Setup
            ///

            // Convert all the numbers to terms
            var allNumbers = numbers
                .Select(n => new Number(n))
                .ToArray();

            ///
            /// Start the combinatoric loop. We do three of them.
            /// 

            // do a op (b op (c op d)
            var combo2and3 = from t1 in AllUnaryOperations(allNumbers[2])
                             from t2 in AllUnaryOperations(allNumbers[3])
                             from aCombo in AllBinaryOperations(t1, t2)
                             select aCombo;

            var combo1and23 = from t1 in AllUnaryOperations(allNumbers[1])
                              from t2 in combo2and3
                              from aCombo in AllBinaryOperations(t1, t2)
                              select aCombo;

            var final1 = from t1 in AllUnaryOperations(allNumbers[0])
                         from t2 in combo1and23
                         from aCombo in AllBinaryOperations(t1, t2)
                         select aCombo;

            // Do (a op b) op (c op d)
            var combo0and1 = from t1 in AllUnaryOperations(allNumbers[0])
                             from t2 in AllUnaryOperations(allNumbers[1])
                             from aCombo in AllBinaryOperations(t1, t2)
                             select aCombo;

            var final2 = from t1 in combo0and1
                         from t2 in combo2and3
                         from r in AllBinaryOperations(t1, t2)
                         select r;

            // do ((a ob b) op c) op d
            var combo01and2 = from t1 in combo0and1
                              from t2 in AllUnaryOperations(allNumbers[2])
                              from aCombo in AllBinaryOperations(t1, t2)
                              select aCombo;

            var final3 = from t1 in combo01and2
                         from t2 in AllUnaryOperations(allNumbers[3])
                         from aCombo in AllBinaryOperations(t1, t2)
                         select aCombo;

            // Combine all sources, and allow the application of everything once more.
            var final = from f in final1.AsParallel().Concat(final2.AsParallel()).Concat(final3.AsParallel())
                        from rCombo in AllUnaryOperations(f)
                        select rCombo;

            // Ready now to get all the answers.
            var answers = new Dictionary<int, string>();

            bool dumpEveryTime = false;
            int numberFound = 0;

            foreach (var result in final)
            {
                var rNumber = result.Calc();
                if (!double.IsNaN(rNumber)
                    && !double.IsInfinity(rNumber)
                    && rNumber >= 0
                    && rNumber <= 100 
                    && ((int)rNumber == rNumber))
                {
                    var s = result.ToString();

                    numberFound++;
                    if (dumpEveryTime)
                    {
                        WriteLine($"{s} = {rNumber}");
                    } else
                    {
                        if (numberFound % 10000 == 0)
                        {
                            WriteLine($"{numberFound}: {s} = {rNumber}  [We have {answers.Count} of 0-100 filled in]");
                        }
                    }

                    if (!answers.ContainsKey((int)rNumber))
                    {
                        answers[(int)rNumber] = s;
                    } else
                    {
                        if (answers[(int)rNumber].Length > s.Length)
                        {
                            answers[(int)rNumber] = s;
                        }
                    }
                }
            }

            // Now dump out everything in order
            WriteLine();
            WriteLine("Answers in order, shortest picked when duplicates exist:");
            WriteLine($"Found {answers.Count} solutions.");
            WriteLine($"Tried {numberFound} combinations.");
            foreach (var ans in answers.Keys.OrderBy(k => k))
            {
                WriteLine($"{ans} = {answers[ans]}");
            }
        }

        /// <summary>
        /// A list of all the binary operations, as generators
        /// </summary>
        static Func<ITerm, ITerm, IBinaryOperation>[] allBinaryOperations = new Func<ITerm, ITerm, IBinaryOperation>[]
        {
                (t1, t2) => new JoinNumbers(t1, t2),
                (t1, t2) => new Plus(t1, t2),
                (t1, t2) => new Minus(t1, t2),
                (t1, t2) => new Division(t1, t2),
                (t1, t2) => new Multiplication(t1, t2),
                (t1, t2) => new Exponent(t1, t2),
        };

        /// <summary>
        /// Return all binary operations given two input terms
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        static IEnumerable<ITerm> AllBinaryOperations(ITerm t1, ITerm t2)
        {
            foreach (var g in allBinaryOperations)
            {
                ITerm r;
                try {
                    r = g(t1, t2);
                } catch
                {
                    r = null;
                }
                if (r != null)
                {
                    yield return r;
                }

            }
        }

        /// <summary>
        /// List of all the unary operations we can run.
        /// </summary>
        static Func<ITerm, IUnaryOperation>[] allUnaryOperations = new Func<ITerm, IUnaryOperation>[]
        {
                t1 => new PlusSign(t1),
                t1 => new MinusSign(t1),
                t1 => new SquareRoot(t1),
                //t1 => new AbsoluteValue(t1), // The plus and negative sign should take care of this.
                t1 => new Factorial(t1),
        };

        /// <summary>
        /// Generate all possible unary operations. These can be applied on top of each other, so this is a bit of a recursive thing.
        /// </summary>
        /// <param name="t1"></param>
        /// <returns></returns>
        static public IEnumerable<ITerm> AllUnaryOperations(ITerm t1)
        {
            return ApplyUniaryOperationsRecursively(t1, allUnaryOperations.Length);
        }

        /// <summary>
        /// Apply them one at a go.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="allUnaryOperations"></param>
        /// <returns></returns>
        private static IEnumerable<ITerm> ApplyUniaryOperationsRecursively(ITerm t1, int count)
        {
            if (count == 0)
            {
                yield return t1;
            }
            else {
                var didOne = false;
                foreach (var g in allUnaryOperations)
                {
                    var allOtherApplications = ApplyUniaryOperationsRecursively(t1, count - 1);
                    foreach (var item in allOtherApplications)
                    {
                        var r = g(item);
                        if (r.isOK())
                        {
                            yield return r;
                            didOne = true;
                        }
                    }
                }

                if (!didOne)
                {
                    yield return t1;
                }
            }
        }
    }
}
