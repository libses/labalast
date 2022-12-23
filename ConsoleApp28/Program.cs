namespace ConsoleApp28
{
    public class Matrix
    {
        private double[,] matrix = new double[3, 3];

        public double this[int column, int row]
        {
            get { return matrix[column, row]; }
            set { matrix[column, row] = value; }
        }

        public Matrix() { }

        public Matrix(string m)
        {
            matrix = new double[3, 3];
            var temp =  m.Split("\n").Select(x => x.Split(" ").Select(double.Parse).ToArray()).ToArray();
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    matrix[x, y] = temp[y][x];
                }
            }
        }
    }

    public class Vector
    {
        private double[] vector = new double[3];

        public Vector()
        {

        }

        public Vector(double x, double y, double z)
        {
            vector = new double[] { x, y, z };
        }

        public double this[int i]
        {
            set { vector[i] = value; }
            get { return vector[i]; }
        }
    }

    internal class Program
    {
        static Vector Multiply(Matrix matrix, Vector vector)
        {
            var result = new Vector();
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    result[x] += matrix[y, x] * vector[y];
                }
            }

            return result;
        }

        static Vector Subtract(Vector f, Vector s)
        {
            var res = new Vector();
            res[0] = f[0] - s[0];
            res[1] = f[1] - s[1];
            res[2] = f[2] - s[2];
            return res;
        }

        static Vector Add(Vector f, Vector s)
        {
            var res = new Vector();
            res[0] = f[0] + s[0];
            res[1] = f[1] + s[1];
            res[2] = f[2] + s[2];
            return res;
        }

        static double Norm(Vector vector)
        {
            return Math.Sqrt(vector[0] * vector[0] + vector[1] * vector[1] + vector[2] * vector[2]);
        }

        static Vector Jacobi(Matrix B, Vector g)
        {
            var xk = new Vector();
            xk[0] = 10;
            xk[1] = 1;
            xk[2] = 81;
            var xkp1 = new Vector();
            xkp1[0] = 27;
            xkp1[1] = 26;
            xkp1[2] = 23;
            while (Norm(Subtract(xkp1, xk)) > 0.000006)
            {
                var temp = new Vector(xkp1[0], xkp1[1], xkp1[2]);
                xkp1 = Add(Multiply(B, xk), g);
                xk = temp;
            }

            return xkp1;
        }

        static void Main(string[] args)
        {
            var B = new Matrix("0 -0,46 -0,48\n-0,51 0 0,12\n-0,08 0,43 0");
            var g = new Vector(4.42, 2.3, 1.81);
            var res = Jacobi(B, g);
            Console.WriteLine(res[0]);
            Console.WriteLine(res[1]);
            Console.WriteLine(res[2]);
        }
    }
}