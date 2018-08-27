using SearchAlgorithms.Model.Maze;

namespace SearchAlgorithms.Model.Algorithms
{
    abstract class SearchAbstract : Interfaces.ISearchable
    {
        // FIELDS
        protected Maze.Maze maze;
        protected Cell start;
        protected Cell goal;
        protected byte delay;
        protected Cell lastPos;

        // CONSTRUCTORS
        public SearchAbstract(Maze.Maze maze, byte delay)
        {
            this.maze = maze;
            this.start = maze.Start;
            this.goal = maze.End;
            this.delay = delay;
            this.lastPos = null;
        }
        // PROPERTIES
        public byte Delay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
            }
        }
        public Cell LastPos => lastPos;

        // METHODS
        public abstract bool Search();
        public void ShowPath()
        {
            for (Cell prev = lastPos; prev != null; prev = prev.Previous)
            {
                prev.Type = CellType.Path;

                System.Threading.Thread.Sleep(delay);
            }
        }
    }
}