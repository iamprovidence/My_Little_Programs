using SearchAlgorithms.Model.Containers;
using SearchAlgorithms.Model.Maze;

namespace SearchAlgorithms.Model.Algorithms
{
    [Reflection.AttributeClasses.SearchAlgorithm(
       Name: "Breadth-First Search",
       Description: " Traditionally used to navigate small, enclosed areas. From the FirstSearch algorithms family, uses Queue")]
    class BreadthFirstSearch : FirstSearch<Queue<Cell>>
    {
        public BreadthFirstSearch(Maze.Maze maze, byte delay = 25) : base(maze, delay)
        {
            container = new Queue<Cell>();
        }
    }
}
