using SearchAlgorithms.Model.Maze;
using SearchAlgorithms.Model.Containers;

namespace SearchAlgorithms.Model.Algorithms
{
    [Reflection.AttributeClasses.SearchAlgorithm(
       Name: "Depth-First Search",
       Description: "Traditionally used to large, closed areas. From the FirstSearch algorithms family, uses Stack")]
    class DepthFirstSearch : FirstSearch<Stack<Cell>>
    {
        public DepthFirstSearch(Maze.Maze maze, byte delay = 25) : base(maze, delay)
        {
            container = new Stack<Cell>();
        }
    }
}
