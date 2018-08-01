using System.Collections.Generic;

namespace AdvancedDictionary.Model
{
    [System.Serializable]
    class Synonyms : DescriptionList<string>
    {
        // CONSTRUCTORS
        public Synonyms()
            : base()
        { }
        public Synonyms(IEnumerable<string> range) 
            : base(range)
        { }
        public Synonyms(IEnumerable<string> rangeUnpicked, IEnumerable<string> rangePicked)
            : base(rangeUnpicked, rangePicked)
        { }
        public Synonyms(List<string> Unpicked, List<string> Picked)
            : base(Unpicked, Picked)
        { }
    }
}
