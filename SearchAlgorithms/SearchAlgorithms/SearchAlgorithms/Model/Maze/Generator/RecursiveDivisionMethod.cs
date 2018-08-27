namespace SearchAlgorithms.Model.Maze.Generator
{
    [Reflection.AttributeClasses.Generator(Type: "Long Walls")]
    class RecursiveDivisionMethod : RandomStartEnd
    {
        public override void CreateObstacles(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight)
        {
            VerticalWall(canvas, 1 ,  colsAmount-1, 1, rowsAmount-1, cellWidth, cellHeight);
            
            // create passages on unfilled cell
            for (int i = 1; i < rowsAmount - 1; ++i)
            {
                for (int j = 1; j < colsAmount - 1; ++j)
                {
                    if (canvas[i, j] == null)// fill in only empty spots
                    {
                        canvas[i, j] = new Cell(i, j, j * cellWidth, i * cellHeight, CellType.Regular);
                    }
                }
            }
        }
        private void VerticalWall(Cell[,] canvas, int fromCol, int toCol, int fromRow, int toRow, float cellWidth, float cellHeight)
        {
            // terminate
            if (toCol - fromCol < 1 || toRow - fromRow < 0) return;

            // indices
            int col = random.Next(fromCol, toCol);
            int passageRow = random.Next(fromRow, toRow);

            // wall
            for(int i = fromRow; i < toRow; ++i)
            {
                canvas[i, col] = new Cell(i, col, col * cellWidth, i * cellHeight, CellType.Wall);
            }
            // passage
            canvas[passageRow, col] = new Cell(passageRow, col, col * cellWidth, passageRow * cellHeight);

            // +1, -1 cuz don't want walls to be close to each other
            HorizontalWall(canvas, fromRow+1, toRow-1, fromCol, col, cellWidth, cellHeight);
            HorizontalWall(canvas, fromRow+1, toRow-1, col +1, toCol, cellWidth, cellHeight);
        }
        private void HorizontalWall(Cell[,] canvas, int fromRow, int toRow, int fromCol, int toCol, float cellWidth, float cellHeight)
        {
            // terminate
            if (toCol - fromCol < 0 || toRow - fromRow < 1) return;

            // indices
            int row = random.Next(fromRow, toRow);
            int passageCol = random.Next(fromCol, toCol);

            // wall
            for (int j = fromCol; j < toCol; ++j)
            {
                canvas[row, j] = new Cell(row, j, j * cellWidth, row * cellHeight, CellType.Wall);
            }
            // passage 
            canvas[row, passageCol] = new Cell(row, passageCol, passageCol * cellWidth, row * cellHeight);

            // +1, -1 cuz don't want walls to be close to each other
            VerticalWall(canvas, fromCol+1, toCol-1, fromRow, row, cellWidth, cellHeight);
            VerticalWall(canvas, fromCol+1, toCol-1, row +1, toRow, cellWidth, cellHeight);
        }
    }
}
