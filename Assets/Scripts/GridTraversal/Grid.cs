// Author: Mathew Morales
// Date: January 2023

using System;

namespace DSA
{
    public class GridLocation
    {
        public int Col { get; set; }
        public int Row { get; set; }

        public GridLocation (int col, int row)
        {
            Col = col;
            Row = row;
        }

        public override string ToString()
        {
            return $"({Col},{Row})";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is GridLocation))
            {
                return false;
            }

            GridLocation other = (GridLocation) obj;

            return Col == other.Col && Row == other.Row;
        }

        public static bool operator ==(GridLocation self, GridLocation other)
        {
            return self.Equals(other);
        }

        public static bool operator !=(GridLocation self, GridLocation other)
        {
            return !self.Equals(other);
        }

        public override int GetHashCode()
        {
            return $"{Col}-{Row}".GetHashCode();
        }
    }

    public class Grid<T> where T : class
    {
        T[,] array;
        public int Size
        {
            get
            {
                return array.GetLength(0);
            }
        }

        public Grid(int size)
        {
            array = new T[size, size];
        }

        public Grid(T[,] initial)
        {
            array = initial;
        }

        public T Get(int x, int y)
        {
            return array[y, x];
        }

        public void Set(int x, int y, T value)
        {
            array[y, x] = value;
        }

        public bool CheckBounds(int x, int y)
        {
            if (0 <= x && x < Size && 0 <= y && y < Size)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Find(Predicate<T> predicate, out T result, out int col, out int row)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    T value = Get(x, y);
                    if (predicate(value))
                    {
                        result = value;
                        col = x;
                        row = y;
                        return true;
                    }
                }
            }

            result = null;
            col = -1;
            row = -1;

            return false;
        }

        public override string ToString()
        {
            string build_string = "";
            for (int y = 0; y < array.GetLength(0); y++)
            {
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    if (this.array[y, x] != null)
                    {
                        build_string += array[y, x].ToString();
                    }
                    else
                    {
                        build_string += ".";
                    }
                    build_string += "\t";
                }
                build_string += "\n";
            }
            return build_string;
        }
    }
}