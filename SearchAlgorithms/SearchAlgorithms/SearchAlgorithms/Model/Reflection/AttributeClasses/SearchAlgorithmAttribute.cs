namespace SearchAlgorithms.Model.Reflection.AttributeClasses
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    class SearchAlgorithmAttribute: System.Attribute
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public SearchAlgorithmAttribute(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }
}
