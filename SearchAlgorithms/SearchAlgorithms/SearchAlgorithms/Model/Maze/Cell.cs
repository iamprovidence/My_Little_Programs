using System;
using System.Drawing;
using static System.Math;
using SearchAlgorithms.Model.EventArgs;
using SearchAlgorithms.Model.Maze.Generator;

namespace SearchAlgorithms.Model.Maze
{
    class Cell : IComparable<Cell>
    {
        // FIELDS
        private CellIndices indices;
        private float x;
        private float y;
        private CellType type;
        private Color color;
        private Cell previous;
        // CONSTRUCTORS
        public Cell(int i, int j, float x, float y, CellType type = CellType.Regular)
        {
            indices = new CellIndices(i, j);
            this.x = x;
            this.y = y;
            this.Type = type;
            this.previous = null;
        }
        // EVENTS
        public event EventHandler<CellTypeChangedEventArgs> TypeChanged;

        // PROPERTIES
        public int I => indices.I;
        public int J => indices.J;
        public float X => x;
        public float Y => y;
        public CellType Type
        {
            get
            {
                return type;
            }
            set
            {
                Reset(value);

                OnTypeChanged(new CellTypeChangedEventArgs(this));
            }
        }
        public Color Color => color;
        public Cell Previous
        {
            get
            {
                return this.previous;
            }
            set
            {
                this.previous = value;
            }
        }

        // METHODS
        public void Reset(CellType value)
        {
            // could not change type of the Start and the End and the Border
            if (type == CellType.Start || type == CellType.End || type == CellType.Border)
            {
                return;
            }

            this.type = value;
            SwitchTypeColor();
        }
        /// <summary>
        /// Calculates distance between two point.
        /// </summary>
        /// <param name="A">First instance of <see cref="SearchAlgorithms.Model.Maze.Cell"/> class.</param>
        /// <param name="B">Second instance of <see cref="SearchAlgorithms.Model.Maze.Cell"/> class.</param>
        /// <returns>Distance between two point.</returns>
        public static double PointDistance(Cell A, Cell B)
        {
            return Sqrt(Pow(B.X - A.X, 2) + Pow(B.Y - A.Y, 2));
        }
        /// <summary>
        /// Calculate distance between two cells in 8 dimensions.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="A">First instance of <see cref="SearchAlgorithms.Model.Maze.Cell"/> class.</param>
        /// <param name="B">Second instance of <see cref="SearchAlgorithms.Model.Maze.Cell"/> class.</param>
        /// <returns>Distance between two cells in 8 dimensions.</returns>
        public static double ChebyshevDistance(Cell A, Cell B)
        {
            return Max(Abs(A.X - B.X), Abs(A.Y - B.Y));
        }
        /// <summary>
        /// Calculate distance between two cells in 4 dimensions.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="A">First instance of <see cref="SearchAlgorithms.Model.Maze.Cell"/> class.</param>
        /// <param name="B">Second instance of <see cref="SearchAlgorithms.Model.Maze.Cell"/> class.</param>
        /// <returns>Distance between two cells in 4 dimensions.</returns>
        public static double ManhattanDistance(Cell A, Cell B)
        {
            return Abs(A.X - B.X) + Abs(A.Y - B.Y);
        }
        protected void SwitchTypeColor()
        {
            switch (type)
            {
                default:
                    case CellType.Regular: color = Color.Gray; break;

                case CellType.Wall:
                    case CellType.Border: color = Color.Black; break;

                case CellType.Visited: color = Color.White; break;
                case CellType.Current: color = Color.Gold; break;
                case CellType.Path: color = Color.Blue; break;
                case CellType.Start: color = Color.Green; break;
                case CellType.End: color = Color.Red; break;
            }
        }
        public int CompareTo(Cell other)
        {
            return this.indices.CompareTo(other.indices);
        }
        protected void OnTypeChanged(CellTypeChangedEventArgs arg)
        {
            TypeChanged?.Invoke(this, arg);
        }        
    }
}
