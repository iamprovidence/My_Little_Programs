namespace AdvancedDictionary.Interfaces
{
    public interface IDescriptionWordView<T> : IDiscriptionWordBase<T>
    {
        void Pick(int index);
        void Unpick(int index);
    }
}
