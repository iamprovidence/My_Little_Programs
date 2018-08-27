namespace SearchAlgorithms.Model.Maze
{
    struct CellIndices : System.IEquatable<CellIndices>, System.IComparable<CellIndices>
    {
        public int I { get; private set; }
        public int J { get; private set; }

        public CellIndices(int i, int j)
        {
            this.I = i;
            this.J = j;
        }

        public bool Equals(CellIndices other)
        {
            return this.I == other.I && this.J == other.J;
        }

        public int CompareTo(CellIndices other)
        {
            int iComparer = I.CompareTo(other.I);
            return iComparer != 0 ? iComparer : J.CompareTo(other.J);
        }
    }
}
