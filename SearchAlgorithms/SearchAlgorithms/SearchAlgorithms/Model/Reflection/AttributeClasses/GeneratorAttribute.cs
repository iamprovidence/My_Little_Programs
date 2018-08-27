namespace SearchAlgorithms.Model.Reflection.AttributeClasses
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    class GeneratorAttribute: System.Attribute
    {
        public string Type { get; private set; }

        public GeneratorAttribute(string Type)
        {
            this.Type = Type;
        }
    }
}
