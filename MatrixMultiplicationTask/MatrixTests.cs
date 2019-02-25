using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MatrixMultiplicationTask
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void IfNotConsistent_Fails()
        {
            var a = new Matrix(2, 2);
            var b = new Matrix(3, 3);
            Assert.Throws<InvalidOperationException>(() => a.Multiply(b));
        }

        [TestCaseSource("MultiplyTestSource")]
        public void MultiplyTest(Matrix a, Matrix b, Matrix abExpect)
        {
            var ab = a.Multiply(b);
            Assert.AreEqual(abExpect.Columns, ab.Columns);
            Assert.AreEqual(abExpect.Rows, ab.Rows);
            for (int i = 0; i < ab.Rows; i++)
            {
                for (int j = 0; j < ab.Columns; j++)
                {
                    Assert.AreEqual(abExpect[i, j], ab[i, j], 1e-5);
                }
            }
        }

        [TestCaseSource("LoadTestSource")]
        public void LoadTest(int count, int size)
        {
            var a = new Matrix(size, size);
            var b = new Matrix(size, size);
            for (int i = 0; i < count; i++)
            {
                a.Multiply(b);
            }
        }

        public static IEnumerable<TestCaseData> MultiplyTestSource()
        {
            yield return new TestCaseData(
                new Matrix(new double[,] { { 2, 3 }, { 6, 5 } }),
                new Matrix(new double[,] { { 8, 9, 7 }, { 5, 3, 5 } }),
                new Matrix(new double[,] { { 31, 27, 29 }, { 73, 69, 67 } }));
            yield return new TestCaseData(
                new Matrix(new double[,] { { 1 }, { 4 }, { 3 } }),
                new Matrix(new double[,] { { 2, 4, 1 } }),
                new Matrix(new double[,] { { 2, 4, 1 }, { 8, 16, 4 }, { 6, 12, 3 } }));
            yield return new TestCaseData(
                new Matrix(new double[,] { { 2, 4, 1 } }),
                new Matrix(new double[,] { { 1 }, { 4 }, { 3 } }),
                new Matrix(new double[,] { { 21 } }));
        }

        public static IEnumerable<TestCaseData> LoadTestSource()
        {
            yield return new TestCaseData(1, 5);
            yield return new TestCaseData(20000, 2);
            yield return new TestCaseData(1000, 10);
            yield return new TestCaseData(50, 50);
            yield return new TestCaseData(10, 200);
        }
    }
}
