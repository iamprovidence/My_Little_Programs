using SearchAlgorithms.Model.Maze;

namespace SearchAlgorithms.Model.EventArgs
{
    class CellTypeChangedEventArgs : System.EventArgs
    {
        public Cell cell { get; private set; }

        public CellTypeChangedEventArgs(Cell cell)
        {
            this.cell = cell;
        }
    }
}
