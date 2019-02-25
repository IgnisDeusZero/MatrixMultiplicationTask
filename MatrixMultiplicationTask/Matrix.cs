using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiplicationTask
{
    public class Matrix
    {
        private readonly double[,] matrix;
        public int Rows => matrix.GetLength(0);
        public int Columns => matrix.GetLength(1);
        public double this[int row, int col]
        {
            get { return matrix[row, col]; }
            set { matrix[row, col] = value; }
        }

        public Matrix(double[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }
            if (matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                throw new ArgumentException("Dimensions must be greater than zero", nameof(matrix));
            }
            this.matrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            CopyMatrix(matrix);
        }

        public Matrix(Matrix other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            this.matrix = new double[other.Rows, other.Columns];
            CopyMatrix(other.matrix);
        }

        public Matrix(int rows, int columns)
        {
            if (rows <= 0)
            {
                throw new ArgumentException("Must be greater than zero", nameof(rows));
            }
            if (columns <= 0)
            {
                throw new ArgumentException("Must be greater than zero", nameof(columns));
            }
            this.matrix = new double[rows, columns];
        }

        private void CopyMatrix(double[,] other)
        {
            for (int i = 0; i < other.GetLength(0); i++)
            {
                for (int j = 0; j < other.GetLength(1); j++)
                {
                    this.matrix[i, j] = other[i, j];
                }
            }
        }

        public Matrix Multiply(Matrix other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            if (Columns != other.Rows)
            {
                throw new InvalidOperationException("Matrices are not consistent");
            }
            var result = new double[Rows, other.Columns];
            Parallel.For(0, Rows, row =>
            {
                Parallel.For(0, other.Columns, col =>
                {
                    for (int i = 0; i < Columns; i++)
                    {
                        result[row, col] += matrix[row, i] * other.matrix[i, col];
                    }
                });
            });
            return new Matrix(result);
        }
    }
}
