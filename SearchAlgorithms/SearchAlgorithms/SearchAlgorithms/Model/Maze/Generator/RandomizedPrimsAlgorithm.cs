using System.Linq;
using System.Collections.Generic;

namespace SearchAlgorithms.Model.Maze.Generator
{
    [Reflection.AttributeClasses.Generator(Type: "Closed area")]
    class RandomizedPrimsAlgorithm : RandomStartEnd
    {
        // FIELDS
        List<Cell> wallList;
        // CONSTRUCTORS
        public RandomizedPrimsAlgorithm() : base()
        {
            wallList = new List<Cell>(25);
        }
        // METHODS
        public override void CreateObstacles(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight)
        {
            // Start with a grid full of walls
            FillInAllWithWalls(canvas, rowsAmount, colsAmount, cellWidth, cellHeight);
            // Pick a cell, mark it as part of the maze. 
            Cell firstCell = canvas[random.Next(1, rowsAmount - 1), random.Next(1, colsAmount - 1)];
            firstCell.Type = CellType.Regular;
            // Add the walls of the cell to the wall list.
            wallList.AddRange(Maze.GetNeighbours(canvas, firstCell, (cell) => cell.Type == CellType.Wall));

            while(wallList.Count > 0)// While there are walls in the list:
            {
                // Pick a random wall from the list.
                int randomWallIndex = random.Next(wallList.Count);
                Cell wall = wallList[randomWallIndex];
                IEnumerable<Cell> neigbourWall = Maze.GetNeighbours(canvas, wall, (cell) => cell.Type == CellType.Wall);

                if (neigbourWall.Count() > 5) // if around only N walls
                {
                    wall.Type = CellType.Regular;// Make the wall a passage
                    wallList.AddRange(neigbourWall);// Add the neighboring walls of the cell to the wall list.

                }
                // Remove the wall from the list.
                wallList.RemoveAt(randomWallIndex);
            }

        }
        
    }
}
