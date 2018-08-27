namespace SearchAlgorithms.Model.Maze.Generator
{
    abstract class GeneratorAbstract : Interfaces.IGeneratable
    {
        public GeneratorAbstract() { }

        public void CreateBorder(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight)
        {
            // border around
            // top and bottom
            for (int i = 0; i < rowsAmount; ++i)
            {
                canvas[i, 0] = new Cell(i, 0, 0, i * cellHeight, CellType.Border);
                canvas[i, colsAmount - 1] = new Cell(i, colsAmount - 1, (colsAmount - 1) * cellWidth, i * cellHeight, CellType.Border);
            }
            // left and right
            for (int j = 0; j < colsAmount; ++j)
            {
                canvas[0, j] = new Cell(0, j, j * cellWidth, 0, CellType.Border);
                canvas[rowsAmount - 1, j] = new Cell(rowsAmount - 1, j, j * cellWidth, (rowsAmount - 1) * cellHeight, CellType.Border);
            }
        }
        public abstract void CreateObstacles(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight);
        public abstract CellIndices SetEnd(int rowsAmount, int colsAmount);
        public abstract CellIndices SetStart(int rowsAmount, int colsAmount);
        protected void FillInAllWithWalls(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight, System.Predicate<Cell> restriction)
        {
            for (int i = 1; i < rowsAmount - 1; ++i)
            {
                for (int j = 1; j < colsAmount - 1; ++j)
                {
                    if(restriction(canvas[i, j]))
                    {
                        canvas[i, j] = new Cell(i, j, j * cellWidth, i * cellHeight, CellType.Wall);
                    }
                }
            }
        }
        protected void FillInAllWithWalls(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight)
        {
            for (int i = 1; i < rowsAmount - 1; ++i)
            {
                for (int j = 1; j < colsAmount - 1; ++j)
                {
                    canvas[i, j] = new Cell(i, j, j * cellWidth, i * cellHeight, CellType.Wall);
                }
            }
        }
    }
}
