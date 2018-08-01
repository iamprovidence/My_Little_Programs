namespace AdvancedDictionary.AdditionalClasses.EventArgsClasses
{
    public class PouringEventArgs : System.EventArgs
    {
        string toSide;
        string name;

        public string ToSide => toSide;
        public string Name => name;

        public PouringEventArgs(string toSide, string name)
        {
            this.toSide = toSide;
            this.name = name;
        }
    }
}
