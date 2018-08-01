namespace AdvancedDictionary.Interfaces
{
    interface IWord: System.IEquatable<IWord>
    {
        string Text { get; set; }
        string Description { get; set; }
        Model.Emotions Emotions { get; set; }
        Model.Synonyms Synonyms { get; set; }
    }
}
