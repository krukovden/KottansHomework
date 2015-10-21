using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class CoolMatrix
    {
        int[,] Arr { get; }
        public bool IsSquare
        {
            get { return Size.IsSquare; }
        }

        public Size Size { get; }

        public CoolMatrix(int[,] arr)
        {
            if (arr == null)
                throw new ArgumentNullException();
            Arr = arr;
            Size = new Size(Arr.GetLength(0), Arr.GetLength(1));
        }

        //public CoolMatrix(Size size)
        //{
        //    Size = size;
        //    Arr=new int[size.Width,size.Height];
        //}

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < Arr.GetLength(0); i++)
            {
                if (i != 0)
                    str.Append("\r\n");
                for (int j = 0; j < Arr.GetLength(1); j++)
                {
                    str.AppendFormat("{0}{1}", (j == 0 ? "[" : ", "), Arr[i, j]);
                }
                str.Append("]");
            }

            return str.ToString();
        }

        public static implicit operator CoolMatrix(int[,] arr)
        {
            return new CoolMatrix(arr);
        }

        public int this[int a, int b]
        {
            get
            {
                if (a > Size.Width && b > Size.Height)
                    throw new IndexOutOfRangeException();
                return Arr[a, b];
            }
            set {
                if (a > Size.Width && b > Size.Height)
                    throw new IndexOutOfRangeException();
                Arr[a, b] = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var tmp = obj as CoolMatrix;
            if (!this.Size.Equals(tmp.Size))
                return false;

            for (int i = 0; i < Arr.GetLength(0); i++)
                for (int j = 0; j < Arr.GetLength(1); j++)
                    if (Arr[i, j] != tmp[i, j])
                        return false;
            return true;
        }

        public override int GetHashCode()
        {
            int rez=0;

            for (int i = 0; i < Arr.GetLength(0); i++)
                for (int j = 0; j < Arr.GetLength(1); j++)
                    rez += Arr[i, j];
            return rez;
        }

        public static bool operator ==(CoolMatrix left, CoolMatrix right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(CoolMatrix left, CoolMatrix right)
        {
            return !left.Equals(right);
        }
        

        public static CoolMatrix operator *(CoolMatrix left, int b)
        {
            for (int i = 0; i < left.Arr.GetLength(0); i++)
                for (int j = 0; j < left.Arr.GetLength(1); j++)
                    left[i, j] = left[i, j]*2;
            return left;
        }

        public static CoolMatrix operator +(CoolMatrix left, CoolMatrix right)
        {
            if(left== null || right==null || !left.Size.Equals(right.Size))
                throw new ArgumentException();

            for (int i = 0; i < left.Arr.GetLength(0); i++)
                for (int j = 0; j < left.Arr.GetLength(1); j++)
                    left[i, j] = left[i, j] + right[i, j];
            return left;
        }

        public CoolMatrix Transpose()
        {
            int[,] arr=new int[Size.Height,Size.Width];

            for (int i = 0; i < Arr.GetLength(0); i++)
                for (int j = 0; j < Arr.GetLength(1); j++)
                    arr[j, i] = Arr[i, j];
            return  new CoolMatrix(arr);
        }
    }
}
