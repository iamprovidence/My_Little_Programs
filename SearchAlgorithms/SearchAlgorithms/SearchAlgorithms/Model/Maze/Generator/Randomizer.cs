namespace SearchAlgorithms.Model.Maze.Generator
{
    [Reflection.AttributeClasses.Generator(Type: "Random Obstacle")]
    class Randomizer : RandomStartEnd
    {
        // FIELDS
        float chanceToBeObstacle;
        // CONSTRUCTORS
        public Randomizer() : base()
        {
            chanceToBeObstacle = 0.3F;
        }
        public Randomizer(float chanceToBeObstacle) : base()
        {
            this.chanceToBeObstacle = chanceToBeObstacle;
        }
        // METHODS
        public override void CreateObstacles(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight)
        {
            for (int i = 1; i < rowsAmount - 1; ++i)
            {
                for (int j = 1; j < colsAmount - 1; ++j)
                {
                    canvas[i, j] = new Cell(i, j, j * cellWidth, i * cellHeight);
                    // sometimes transform into obstacles
                    if (random.NextDouble() < chanceToBeObstacle) canvas[i, j].Type = CellType.Wall;                    
                }
            }
        }
        
    }
}
