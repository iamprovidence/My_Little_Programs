using SearchAlgorithms.Model.Maze;
using SearchAlgorithms.Model.Containers;

namespace SearchAlgorithms.Model.Algorithms
{
    [Reflection.AttributeClasses.SearchAlgorithm(
       Name: "Best-First Search",
       Description: "Generally better suited to more open environments with fewer obstacles. From the FirstSearch algorithms family, uses PriorityQueue")]
    class BestFirstSearch : FirstSearch<PriorityQueue<Cell>>
    {
        public BestFirstSearch(Maze.Maze maze, byte delay = 25) : base(maze, delay)
        {
            container = new PriorityQueue<Cell>(new CellComparer(CellComparer.CellComparerType.pointDistance, goal));
        }
    }
}
