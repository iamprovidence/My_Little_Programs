using System.Collections.Generic;

namespace AdvancedDictionary.Interfaces
{
    public interface IDiscriptionWordBase<T>
    {
        List<T> Picked { get; }
        List<T> Unpicked { get; }
    }
}
