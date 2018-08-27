namespace SearchAlgorithms.Model.Maze.Generator
{
    [Reflection.AttributeClasses.Generator(Type: "Empty Maze")]
    class Empty : GeneratorAbstract
    {
        public Empty() : base() { }
        public override void CreateObstacles(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight )
        {
            for (int i = 1; i < rowsAmount - 1; ++i)
            {
                for (int j = 1; j < colsAmount - 1; ++j)
                {
                    canvas[i, j] = new Cell(i, j, j * cellWidth, i * cellHeight);
                }
            }
        }
        public override CellIndices SetStart(int rowsAmount, int colsAmount)
        {
            return new CellIndices(1, 1);// top left corner
        }
        public override CellIndices SetEnd(int rowsAmount, int colsAmount)
        {
            return new CellIndices(rowsAmount - 1, colsAmount - 1);// bottom right corner     
        }        
    }
}
