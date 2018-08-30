using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SearchAlgorithms.Model.EventArgs;
using SearchAlgorithms.Model.Interfaces;

namespace SearchAlgorithms.Model.Maze
{
    class Maze : IDisposable
    {
        // FIELDS
        private Cell[,] canvas;
        private int rowsAmount;
        private int colsAmount;
        private float cellWidth;
        private float cellHeight;
        private int width;
        private int height;
        private Cell start;
        private Cell end;
        private IGeneratable generator;

        private bool disposedValue;
        private Bitmap bitmap;
        private Graphics graphics;
        private Pen cellBorderPen;
        private SolidBrush cellFillBrush;
        // CONSTRUCTORS
        public Maze(int rows, int cols, int width, int height, IGeneratable generator)
        {
            this.generator = generator;
            this.rowsAmount = rows;
            this.colsAmount = cols;
            this.width = width;
            this.height = height;

            CreateNew();
            // initialize drawing tools
            this.disposedValue = false;            
            this.cellBorderPen = new Pen(Color.Black);
            this.cellFillBrush = new SolidBrush(Color.White);           

        }
        public void CreateNew()
        {
            this.cellWidth = width / colsAmount;
            this.cellHeight = height / rowsAmount;

            // build it
            BuildNew();           

            // dispose old, creare new
            this.bitmap?.Dispose();
            this.graphics?.Dispose();
            this.bitmap = new Bitmap(width, height);
            this.graphics = Graphics.FromImage(bitmap);
        }
        public void BuildNew()
        {
            this.canvas = new Cell[rowsAmount, colsAmount];
            // build it
            generator.CreateBorder(canvas, rowsAmount, colsAmount, cellWidth, cellHeight);
            generator.CreateObstacles(canvas, rowsAmount, colsAmount, cellWidth, cellHeight);
            this.SetStart(generator.SetStart(rowsAmount - 1, colsAmount - 1));
            this.SetEnd(generator.SetEnd(rowsAmount - 1, colsAmount - 1));

            // sign up for the event
            foreach (Cell cell in canvas) cell.TypeChanged += (obj, args) => OnCellColorChanged(args);
        }
        private void SetStart(CellIndices indices)
        {
            this.start = new Cell(indices.I, indices.J, indices.J * cellWidth, indices.I * cellHeight, CellType.Start);
            this.canvas[indices.I, indices.J] = start;
        }
        private void SetEnd(CellIndices indices)
        {
            this.end = new Cell(indices.I, indices.J, indices.J * cellWidth, indices.I * cellHeight, CellType.End);
            this.canvas[indices.I, indices.J] = end;
        }
        ~Maze()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    bitmap.Dispose();
                    graphics.Dispose();
                    cellBorderPen.Dispose();
                    cellFillBrush.Dispose();
                }

                disposedValue = true;
            }
        }
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // PROPERTIES
        public Cell[,] Canvas => canvas;
        public Cell Start => start;
        public Cell End => end;
        public int Cols
        {
            get
            {
                return colsAmount;
            }
            set
            {
                if (colsAmount != value)
                {
                    colsAmount = value;
                    OnMazeChanged(System.EventArgs.Empty);
                }
            }
        }
        public int Rows
        {
            get
            {
                return rowsAmount;
            }
            set
            {
                if (rowsAmount != value)
                {
                    rowsAmount = value;
                    OnMazeChanged(System.EventArgs.Empty);
                }
            }
        }
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width != value)
                {
                    width = value;
                    OnMazeChanged(System.EventArgs.Empty);
                }
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    height = value;
                    OnMazeChanged(System.EventArgs.Empty);
                }
            }
        }
        public IGeneratable Generator
        {
            get
            {
                return generator;
            }
            set
            {
                if(generator != value)
                {
                    generator = value;
                    OnMazeChanged(System.EventArgs.Empty);
                }
            }
        }
        // INDEXERS
        public Cell this[int i, int j] => canvas[i, j];
        // EVENTS
        public event EventHandler<CellTypeChangedEventArgs> CellColorChanged;
        public event EventHandler MazeChanged;
        // METHODS
        public Bitmap Show()
        {
            graphics.Clear(Color.Black);
            foreach (Cell c in canvas)
            {
                cellFillBrush.Color = c.Color;

                graphics.FillRectangle(cellFillBrush, c.X, c.Y, cellWidth, cellHeight);
                graphics.DrawRectangle(cellBorderPen, c.X, c.Y, cellWidth, cellHeight);
            }
            return bitmap;
        }
        public void RedrawCell(Graphics g, CellTypeChangedEventArgs cell)
        {
            cellFillBrush.Color = cell.cell.Color;
            g.FillRectangle(cellFillBrush, cell.cell.X, cell.cell.Y, cellWidth, cellHeight);
            g.DrawRectangle(cellBorderPen, cell.cell.X, cell.cell.Y, cellWidth, cellHeight);
        }
        public void CellTypeFromPixel(int x, int y, CellType transformTo, Predicate<Cell> restrictions)
        {
            int i = (int)Math.Floor(x / cellWidth);
            int j = (int)Math.Floor(y / cellHeight);

            if(restrictions(canvas[j, i]))
            {
                canvas[j, i].Type = transformTo;
            }
        }
        public void CellTypeFromPixel(int x, int y, CellType transformTo)
        {
            CellTypeFromPixel(x, y, transformTo, (cell) => true);
        }
        // free for everybody
        public static IEnumerable<Cell> GetNeighbours(Cell[,] canvas, Cell current, Predicate<Cell> predicate)
        {
            Cell[] walls = new Cell[8];

            int i = current.I;
            int j = current.J;
            int index = 0;
            // get neighbour 

            // adding neighbours by clockwise
            // not checking for right indices cuz maze surrounded by walls

            // top
            if (predicate(canvas[i - 1, j - 1]))     walls[index++] = canvas[i - 1, j - 1];
            if (predicate(canvas[i - 1, j]))         walls[index++] = canvas[i - 1, j];
            if (predicate(canvas[i - 1, j + 1]))     walls[index++] = canvas[i - 1, j + 1];
            // right
            if (predicate(canvas[i, j + 1]))         walls[index++] = canvas[i, j + 1];
            // bottom
            if (predicate(canvas[i + 1, j + 1]))     walls[index++] = canvas[i + 1, j + 1];
            if (predicate(canvas[i + 1, j]))         walls[index++] = canvas[i + 1, j];
            if (predicate(canvas[i + 1, j - 1]))     walls[index++] = canvas[i + 1, j - 1];
            // left
            if (predicate(canvas[i, j - 1]))         walls[index++] = canvas[i, j - 1];


            return walls.Where(x => x != null);
        }
        // for algorithms
        public IEnumerable<Cell> GetNeighbours(Cell currentCell, Predicate<Cell> predicate)
        {
            return Maze.GetNeighbours(canvas, currentCell, cell => cell.Type != CellType.Wall && cell.Type != CellType.Border && cell.Type != CellType.Start && predicate(cell));
        }        
        public IEnumerable<Cell> GetNeighbours(Cell currentCell)
        {
            return GetNeighbours(currentCell, (cell) => cell.Type != CellType.Visited && cell.Type != CellType.Current);
        }

        public void ResetMaze(Predicate<Cell> predicate, CellType convertCellTo)
        {
            foreach (Cell cell in canvas)
            {
                if(predicate(cell))
                {
                    cell.Reset(convertCellTo);
                }
            }
        }
        public void ResetMaze()
        {
            this.ResetMaze((cell) => cell.Type == CellType.Current || cell.Type == CellType.Visited || cell.Type == CellType.Path, CellType.Regular);
        }

        protected void OnCellColorChanged(CellTypeChangedEventArgs args)
        {
            CellColorChanged?.Invoke(this, args);
        }
        protected void OnMazeChanged(System.EventArgs args)
        {
            MazeChanged?.Invoke(this, args);
            CreateNew();
        }
    }
}
