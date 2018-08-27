namespace SearchAlgorithms.Model.Maze
{
    enum CellType : byte
    {
        Regular, 
        Start,
        End,
        Wall,
        Border,
        Visited,
        Current,
        Path
    }
}
