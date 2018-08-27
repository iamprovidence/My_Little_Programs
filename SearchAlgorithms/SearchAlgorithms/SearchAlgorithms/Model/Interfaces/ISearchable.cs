namespace SearchAlgorithms.Model.Interfaces
{
    interface ISearchable
    {
        byte Delay { get; set; }
        Maze.Cell LastPos { get; }
        bool Search();
        void ShowPath();
    }
}
