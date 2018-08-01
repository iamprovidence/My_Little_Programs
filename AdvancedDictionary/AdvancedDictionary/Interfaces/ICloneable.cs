namespace AdvancedDictionary.Interfaces
{
    interface ICloneable<out T>
    {
        T Clone();
    }
}
