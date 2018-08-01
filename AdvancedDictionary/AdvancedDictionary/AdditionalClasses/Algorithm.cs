using System.Collections.Generic;

namespace AdvancedDictionary.AdditionalClasses
{
    public static class Algorithm
    {
        public class FullSet<T>
        {
            public List<T> Intersect { get; private set; } = new List<T>();
            public List<T> Except { get; private set; } = new List<T>();

        }
        static public FullSet<T> DivideSetBySet<T>(IList<T> A, IList<T> B)
        {
            FullSet<T> result = new FullSet<T>();

            HashSet<T> SetA = new HashSet<T>(A);

            for (int i = 0; i < B.Count; ++i)
            {
                if (SetA.Contains(B[i]))
                {
                    result.Intersect.Add(B[i]);
                    SetA.Remove(B[i]);
                }
            }
            result.Except.AddRange(SetA);
            return result;
        }
        
    }
}
