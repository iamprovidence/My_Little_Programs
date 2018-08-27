using System;
using System.Linq;
using System.Collections.Generic;
using SearchAlgorithms.Model.Maze;

namespace SearchAlgorithms.Model.Algorithms
{
    [Reflection.AttributeClasses.SearchAlgorithm(
        Name:"A*",
        Description: "More complex algorithm, based on Dijkstra's algorithm, designed to intelligently dodge obstacle")]
    class AStar: SearchAbstract
    {
        class ACell: IComparable<ACell>
        {
            public Cell Cell { get; private set; }
            public double ShortestWay { get; set; }
            public double WayFromStartToThisCell { get; set; }

            public ACell(Cell cell)
            {
                this.Cell = cell;
                this.ShortestWay = 0;
                this.WayFromStartToThisCell = 0;
            }

            public int CompareTo(ACell other)
            {
                return this.ShortestWay.CompareTo(other.ShortestWay);
            }
        }
        
        // FIELDS
        private HashSet<ACell> openSet;
        private HashSet<Cell> closedSet;
        // CONSTRUCTORS
        public AStar(Maze.Maze maze, byte delay = 25)
            :base(maze, delay)
        {
            this.openSet = new HashSet<ACell>();
            this.closedSet = new HashSet<Cell>();     
        }

        // METHODS
        public override bool Search()
        {
            this.Clear();
            this.openSet.Add(new ACell(start));
            // while there are known cells ...
            while (openSet.Count != 0)
            {
                // ... get cell with shortest way
                ACell shortest = openSet.Min();

                // if it is goal...
                if (shortest.Cell == goal)
                {
                    // ... the path has been found
                    lastPos = shortest.Cell;
                    return true;
                }
                // if it is not goal, explore this cell
                Explore(shortest);

                // cell fully explored, move it from OpenSet to ClosedSet
                shortest.Cell.Type = CellType.Visited;
                openSet.Remove(shortest);
                closedSet.Add(shortest.Cell);
            }
            // no valid path
            return false;
        }

        /// <summary>
        ///  Check neighbours and add them to OpenSet if:
        /// <para>- met them for the first time</para>
        /// <para>- found bettter way to this cell</para>
        /// </summary>
        /// <param name="currentCell">The cell which heighbour should be checked</param>
        private void Explore(ACell currentCell)
        {
            // for each neighbours (event current), which are not in the ClosedSet(aka Visited)
            foreach (Cell neighbour in maze.GetNeighbours(currentCell.Cell, (cell) => cell.Type != CellType.Visited))
            {
                double WayFromStartToThisNeighbour = currentCell.WayFromStartToThisCell + 1;

                // check if neighbour is in OpenSet
                ACell aNeighbourInOpen = openSet.FirstOrDefault(acell => acell.Cell == neighbour);

                // if neighbour is not in OpenSet or his Way value is worse that we found...
                if (aNeighbourInOpen == null || aNeighbourInOpen.WayFromStartToThisCell > WayFromStartToThisNeighbour) 
                {
                    aNeighbourInOpen = aNeighbourInOpen ?? new ACell(neighbour);// create new if needed
                    openSet.Add(aNeighbourInOpen);// add if needed

                    // update values
                    aNeighbourInOpen.Cell.Type = CellType.Current;
                    aNeighbourInOpen.WayFromStartToThisCell = WayFromStartToThisNeighbour;
                    aNeighbourInOpen.ShortestWay = aNeighbourInOpen.WayFromStartToThisCell + CalcWayToEnd(aNeighbourInOpen.Cell);
                    aNeighbourInOpen.Cell.Previous = currentCell.Cell;

                    System.Threading.Thread.Sleep(delay);
                }                          
            }
        }

        private double CalcWayToEnd(Cell A)
        {
            return Cell.ManhattanDistance(A, goal);
            //return Cell.PointDistance(A, goal);
            //return Cell.ChebyshevDistance(A, goal);
        }
        private void Clear()
        {
            openSet.Clear();
            closedSet.Clear();
        }
    }
}
