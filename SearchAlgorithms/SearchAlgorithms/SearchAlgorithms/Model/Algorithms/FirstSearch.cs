using SearchAlgorithms.Model.Maze;

namespace SearchAlgorithms.Model.Algorithms
{
    abstract class FirstSearch<TContainer> : SearchAbstract where TContainer: Interfaces.IAdapter<Cell>
    {
        // FIELDS
        protected TContainer container;

        // CONSTRUCTORS
        public FirstSearch(Maze.Maze maze, byte delay)
            : base(maze, delay) { }

        // METHODS
        public override bool Search()
        {
            container.Clear();
            // Get node, push his neighbours to container, get rid of node
            // algorithm's work depending on container
            container.Add(start);

            while(!container.IsEmpty())
            {
                Cell current = container.Peek();

                if (current == goal)
                {
                    lastPos = current;
                    return true;
                }
                container.Remove();
                current.Type = CellType.Visited;

                foreach (Cell neighbour in maze.GetNeighbours(current))
                {
                    container.Add(neighbour);
                    neighbour.Type = CellType.Current;
                    neighbour.Previous = current;

                    System.Threading.Thread.Sleep(delay);
                }
            }

            return false;
        }
    }
}
