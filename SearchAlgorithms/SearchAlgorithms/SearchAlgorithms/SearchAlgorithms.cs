using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SearchAlgorithms.Model.Maze;
using SearchAlgorithms.Model.Interfaces;
using SearchAlgorithms.Model.Reflection.AttributeClasses;

namespace SearchAlgorithms
{
    public partial class MainForm : Form
    {
        // FIELDS
        object obj = new object();// sync root
        Thread algorithmThread;// cut down on Dispose

        Maze maze;
        Type[] algorithms;
        Type[] generators;
        ISearchable algorithm;

        bool drawMaze;
        bool eraseMaze;

        // CONSTRUCTORS
        public MainForm()
        {
            algorithmThread = new Thread(()=> { });// thread with no task
            this.DoubleBuffered = true;
            InitializeComponent();
            // get all classes with custom attributes
            var linq = from assemblies in AppDomain.CurrentDomain.GetAssemblies()
                           from classes in assemblies.GetTypes()
                           where classes.IsDefined(typeof(GeneratorAttribute), true) ||
                                 classes.IsDefined(typeof(SearchAlgorithmAttribute), true)
                           orderby classes.Name
                           select classes;
            // get generators type
            generators = linq.Where(type => type.IsDefined(typeof(GeneratorAttribute), true)).ToArray();
            // get Generators's name from Attribute
            generatorLb.BeginUpdate();
            foreach (Type type in generators)
            {
                generatorLb.Items.Add(((GeneratorAttribute)type.GetCustomAttributes(typeof(GeneratorAttribute), true).First()).Type);
            }
            generatorLb.EndUpdate();
            // get algorithms type
            algorithms = linq.Where(type => type.IsDefined(typeof(SearchAlgorithmAttribute), true)).ToArray();
            // get Algorithms's name from Attribute
            algorithmLb.BeginUpdate();
            foreach (Type type in algorithms)
            {
                algorithmLb.Items.Add(((SearchAlgorithmAttribute)type.GetCustomAttributes(typeof(SearchAlgorithmAttribute), true).First()).Name);
            }
            algorithmLb.EndUpdate();
            // generate maze
            maze = new Maze(rows: (int)rowsUpDown.Value, cols: (int)colsUpDown.Value, 
                width: mazePnl.Width, height: mazePnl.Height, generator: (IGeneratable)Activator.CreateInstance(generators.First()));
            
            maze.CellColorChanged += (o, args) => RedrawCell(args);
            maze.MazeChanged += (o, args) =>
            {
                // 30 % chance to clean up
                if (new Random().NextDouble() < 0.3) GC.Collect();

                algorithmThread.Abort();
                mazePnl.Invalidate();
            };

            drawMaze = false;
            eraseMaze = false;
        }
        // PAINT EVENT
        private void mazePnl_Paint(object sender, PaintEventArgs e)
        {
            lock (obj)
            {
                e.Graphics.DrawImage(maze.Show(), 0, 0);
            }
        }
        private void RedrawCell(Model.EventArgs.CellTypeChangedEventArgs cell)
        {
            lock(obj)
            {
                maze.RedrawCell(mazePnl.CreateGraphics(), cell);
            }
        }
        // RUN ALGORITHM
        private void Run()
        {
            algorithmThread = new Thread(() => RunSearch());

            algorithmThread.Start();            
        }
        private void RunSearch()
        {
            try
            {
                while (true)
                {
                    algorithm.Search();
                    algorithm.ShowPath();
                    Thread.Sleep(1000);
                    maze.ResetMaze();
                    mazePnl.Invalidate();
                }
            }
            catch (ThreadAbortException)
            {
                try
                {
                    maze.ResetMaze();
                }
                catch (NullReferenceException) { }// if maze cleaned, all cell equal to Null, happened when user change Generator
                mazePnl.Invalidate();
            }
        }
        // FORM EVENT
        private void mazePnl_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) drawMaze = true;
            else if (e.Button == MouseButtons.Right) eraseMaze = true;
        }
        private void mazePnl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) drawMaze = false;
            else if (e.Button == MouseButtons.Right) eraseMaze = false;
        }
        private void mazePnl_MouseMove(object sender, MouseEventArgs e)
        {            
            try
            {
                if (drawMaze)
                {
                    maze.CellTypeFromPixel(e.X, e.Y, CellType.Wall);
                }
                else if(eraseMaze)
                {
                    // only wall can be erased
                    maze.CellTypeFromPixel(e.X, e.Y, CellType.Regular, (cell) => cell.Type == CellType.Wall);
                }
            }
            catch (IndexOutOfRangeException)
            {
                // user still hold mouse but move out panel range
                // ignore
            }           
        }
        private void delayUpDown_ValueChanged(object sender, EventArgs e)
        {
            if(algorithm != null)
            {
                algorithm.Delay = (byte)delayUpDown.Value;
            }
        }
        private void rowsUpDown_ValueChanged(object sender, EventArgs e)
        {
            maze.Rows = (int)rowsUpDown.Value;
        }
        private void colsUpDown_ValueChanged(object sender, EventArgs e)
        {
            maze.Cols = (int)colsUpDown.Value;
        }
        private void mazePnl_SizeChanged(object sender, EventArgs e)
        {
            maze.Width = mazePnl.Width;
            maze.Height = mazePnl.Height;
        }
        private void generatorLb_SelectedIndexChanged(object sender, EventArgs e)
        {
            algorithmThread.Abort();
            if (generatorLb.SelectedIndex != -1)
            {
                maze.Generator = (IGeneratable)Activator.CreateInstance(generators[generatorLb.SelectedIndex]);
            }
        }
        private void algorithmLb_SelectedIndexChanged(object sender, EventArgs e)
        {
            algorithmThread.Abort();
            if (algorithmLb.SelectedIndex != -1)
            {
                // activate algorithm
                algorithm = (ISearchable)Activator.CreateInstance(algorithms[algorithmLb.SelectedIndex], new object[] { maze, (byte)delayUpDown.Value });
                Run();
            }
        }
    }
}
