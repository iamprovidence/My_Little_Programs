using System;
using System.Collections.Generic;

namespace SearchAlgorithms.Model.Maze
{
    class CellComparer : IComparer<Cell>
    {
        public enum CellComparerType { indices, pointDistance, chebyshevDistance, manhattanDistance }

        private CellComparerType comparerType;
        private Cell goal;

        public CellComparer(CellComparerType comparerType, Cell goal = null)
        {
            this.comparerType = comparerType;
            this.goal = goal;
        }
        public int Compare(Cell x, Cell y)
        {
            switch (comparerType)
            {
                case CellComparerType.indices:
                    {
                        return x.CompareTo(y);
                    };
                    // if two distances are the same we comparing their indices
                case CellComparerType.pointDistance:
                    {
                        int res = Cell.PointDistance(x, goal).CompareTo(Cell.PointDistance(y, goal));
                        return res != 0 ? res : x.CompareTo(y);
                    };
                case CellComparerType.chebyshevDistance:
                    {
                        int res = Cell.ChebyshevDistance(x, goal).CompareTo(Cell.ChebyshevDistance(y, goal));
                        return res != 0 ? res : x.CompareTo(y);
                    }
                case CellComparerType.manhattanDistance:
                    {
                        int res = Cell.ManhattanDistance(x, goal).CompareTo(Cell.ManhattanDistance(y, goal));
                        return res != 0 ? res : x.CompareTo(y);
                    }
                default : throw new Exception("Not valid value to compare");
            }
        }
    }
}
