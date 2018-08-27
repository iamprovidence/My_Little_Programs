using SearchAlgorithms.Model.Maze;

namespace SearchAlgorithms.Model.Interfaces
{
    interface IGeneratable
    {
        void CreateBorder(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight);
        CellIndices SetStart(int rowsAmount, int colsAmount);
        CellIndices SetEnd(int rowsAmount, int colsAmount);
        void CreateObstacles(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight);
    }
}
