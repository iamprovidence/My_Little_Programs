using System;
using System.Linq;
using System.Collections.Generic;
using SearchAlgorithms.Model.Maze;

namespace SearchAlgorithms.Model.Algorithms
{
    [Reflection.AttributeClasses.SearchAlgorithm(
       Name: "Dijkstra's Algorithm",
       Description: "An algorithm for finding the shortest paths regardless of time.")]
    class DijkstrasAlgorithm : SearchAbstract
    {
        class DCell : IComparable<DCell>
        {
            public Cell Cell { get; private set; }
            public double ShortestWay { get; set; }

            public DCell(Cell cell)
            {
                this.Cell = cell;
                this.ShortestWay = 0;
            }

            public int CompareTo(DCell other)
            {
                return this.ShortestWay.CompareTo(other.ShortestWay);
            }
        }

        // FIELDS
        private HashSet<DCell> openSet;
        private HashSet<Cell> closedSet;
        // CONSTRUCTORS
        public DijkstrasAlgorithm(Maze.Maze maze, byte delay = 25) 
            : base(maze, delay)
        {
            this.openSet = new HashSet<DCell>();
            this.closedSet = new HashSet<Cell>();
        }

        // METHODS
        public override bool Search()
        {
            this.Clear();
            this.openSet.Add(new DCell(start));
            // while there are known cells ...
            while (openSet.Count != 0)
            {
                // ... get cell with shortest way
                DCell current = openSet.Min();

                // if it is goal...
                if (current.Cell == goal)
                {
                    // ... the path has been found
                    lastPos = current.Cell;
                    return true;
                }
                // handle neighbours
                foreach (Cell neighbour in maze.GetNeighbours(current.Cell, (cell) => cell.Type != CellType.Visited))
                {
                    double WayFromStartToThisNeighbour = current.ShortestWay + 1;

                    DCell aNeighbourInOpen = openSet.FirstOrDefault(dcell => dcell.Cell == neighbour);

                    if (aNeighbourInOpen == null || aNeighbourInOpen.ShortestWay > WayFromStartToThisNeighbour)
                    {
                        aNeighbourInOpen = aNeighbourInOpen ?? new DCell(neighbour);// create if needed
                        openSet.Add(aNeighbourInOpen);// add if needed
                    
                        // update values
                        aNeighbourInOpen.ShortestWay = WayFromStartToThisNeighbour;
                        aNeighbourInOpen.Cell.Previous = current.Cell;
                    }
                    aNeighbourInOpen.Cell.Type = CellType.Current;
                    System.Threading.Thread.Sleep(delay);
                }

                // move cell from OpenSet to ClosedSet
                current.Cell.Type = CellType.Visited;
                openSet.Remove(current);
                closedSet.Add(current.Cell);
            }
            // no valid path
            return false;
        }
        private void Clear()
        {
            openSet.Clear();
            closedSet.Clear();
        }
    }
}
