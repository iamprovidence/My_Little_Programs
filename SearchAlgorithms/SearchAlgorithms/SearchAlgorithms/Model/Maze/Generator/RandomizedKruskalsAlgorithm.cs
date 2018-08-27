using System.Collections.Generic;
using SearchAlgorithms.Model.Containers;

namespace SearchAlgorithms.Model.Maze.Generator
{
    [Reflection.AttributeClasses.Generator(Type: "Maze")]
    class RandomizedKruskalsAlgorithm : RandomStartEnd
    {
        // FIELDS
        DisjointSetUnion<CellIndices> sets;
        List<CellIndices> listSets;

        // CONSTRUCTORS
        public RandomizedKruskalsAlgorithm() : base()
        {
            sets = new DisjointSetUnion<CellIndices>();
            listSets = new List<CellIndices>(100);
        }
        // METHODS
        public override void CreateObstacles(Cell[,] canvas, int rowsAmount, int colsAmount, float cellWidth, float cellHeight)
        {
            sets.Clear();
            listSets.Clear();
            // save every odd indices in their own set
            for (int i = 1; i < rowsAmount - 1; i += 2)
            {
                for (int j = 1; j < colsAmount - 1; j += 2)
                {
                    CellIndices indices = new CellIndices(i, j);
                    sets.MakeSet(indices);
                    listSets.Add(indices);
                }
            }
            int index = 0;
            // while there is more than one set
            while (sets.SetsAmount != 1)
            {
                // take element
                CellIndices firstCell = listSets[index++ % listSets.Count];
                // take another one horizontal OR vertical with random chances
                int h;
                int v;

                double firstRand = random.NextDouble();

                if (firstRand < 0.3) h = -2;
                else if (firstRand > 0.6) h = 2;
                else h = 0;

                if (h == 0) v = random.NextDouble() < 0.5 ? 2 : -2;
                else v = 0;
                CellIndices secondCell = new CellIndices(firstCell.I + v, firstCell.J + h);

                // if they in difference sets -> combine them(make a path, combine sets)
                if (indexInRange(secondCell.I, secondCell.J, rowsAmount, colsAmount) && sets.InDifferentSets(firstCell, secondCell))
                {
                    // path with 2 cells
                    for (int i = firstCell.I, j = firstCell.J, count = 0; count != 3; i += v / 2, j += h / 2, ++count)
                    {
                            canvas[i, j] = new Cell(i, j, j * cellWidth, i * cellHeight);
                    }
                    // combine sets
                    sets.UnionSets(firstCell, secondCell);
                }
            }      
            // walls in null cell spots
            FillInAllWithWalls(canvas, rowsAmount, colsAmount, cellWidth, cellHeight, (cell) => cell == null);
        }

        private bool indexInRange(int i, int j, int rowsAmount, int colsAmount)
        {
            return i > 0 && i < rowsAmount - 1 && j > 0 && j < colsAmount - 1;
        }
    }
}
