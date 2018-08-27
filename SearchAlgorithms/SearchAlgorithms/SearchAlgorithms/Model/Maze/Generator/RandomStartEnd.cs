namespace SearchAlgorithms.Model.Maze.Generator
{
    abstract class RandomStartEnd : GeneratorAbstract
    {
        // INNER CLASSES
        internal class CellIndicesList : System.Collections.Generic.List<CellIndices>
        {
            public CellIndicesList() : base(2) { }

            public bool Contains(int i, int j)
            {
                foreach (CellIndices cell in this)
                {
                    if (cell.I == i && cell.J == j)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        // FIELDS
        protected System.Random random;
        protected CellIndicesList startEndIndices;
        // CONSTRUCTORS
        public RandomStartEnd(): base()
        {
            random = new System.Random();
            startEndIndices = new CellIndicesList();
        }
        // METHODS
        public override CellIndices SetEnd(int rowsAmount, int colsAmount)
        {
            int i, j;
            do
            {
                i = random.Next(1, rowsAmount);
                j = random.Next(1, colsAmount);
            } while (startEndIndices.Contains(i, j));

            startEndIndices.Add(new CellIndices(i, j));
            return startEndIndices[startEndIndices.Count - 1];
        }
        public override CellIndices SetStart(int rowsAmount, int colsAmount)
        {
            int i, j;
            do
            {
                i = random.Next(1, rowsAmount);
                j = random.Next(1, colsAmount);
            } while (startEndIndices.Contains(i, j));

            startEndIndices.Add(new CellIndices(i, j));
            return startEndIndices[startEndIndices.Count - 1];
        }    
    }
}
