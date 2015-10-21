using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class Size
    {
        public int Width { get; }
        public int Height { get; }
        public bool IsSquare
        {
            get { return Width == Height; }
        }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Size size = obj as Size;
           
            return Width == size.Width && Height == size.Height;
        }

        public override int GetHashCode()
        {
            return Width + Height;
        }

        public static bool operator ==(Size left, Size right)
        {
          
            return left.Equals(right);
        }

        public static bool operator !=(Size left, Size right)
        {
            return !left.Equals(right);
        }
    }
}
