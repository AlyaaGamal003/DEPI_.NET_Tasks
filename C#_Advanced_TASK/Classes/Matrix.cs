using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Bonus: Implement a Matrix class with a two-dimensional indexer matrix[row, column] for mathematical operations.
    internal class Matrix
    {
        private double[,] data;

        public int Rows { get; }
        public int Columns { get; }

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            data = new double[rows, cols];
        }

        //The Indexer 
        public double this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                    throw new IndexOutOfRangeException("Invalid index!");
                return data[row, col];
            }
            set
            {
                if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                    throw new IndexOutOfRangeException("Invalid index!");
                data[row, col] = value;
            }
        }


        // Matrix Addition
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new InvalidOperationException("Matrix dimensions must match for addition.");

            Matrix result = new Matrix(a.Rows, a.Columns);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++)
                    result[i, j] = a[i, j] + b[i, j];

            return result;
        }
        // Matrix Subtraction
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new InvalidOperationException("Matrix dimensions must match for subtraction.");
            Matrix result = new Matrix(a.Rows, a.Columns);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++)
                    result[i, j] = a[i, j] - b[i, j];
            return result;
        }
        // Matrix Manpulation
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new InvalidOperationException("Matrix dimensions must match for subtraction.");
            Matrix result = new Matrix(a.Rows, a.Columns);
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++)
                    result[i, j] = a[i, j] * b[i, j];
            return result;
        }

        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    Console.Write($"{data[i, j],6} ");
                Console.WriteLine();
            }
        }

    }
}
