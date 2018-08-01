using System.Collections.Generic;

namespace AdvancedDictionary.Model
{

    [System.Serializable]
    internal class Emotions : DescriptionList<string>
    {
        // CONSTRUCTORS
        public Emotions()
            : base()
        {  }
        public Emotions(IEnumerable<string> range) 
            : base(range)
        {  }
        public Emotions(IEnumerable<string> rangeUnpicked, IEnumerable<string> rangePicked)
            : base(rangeUnpicked, rangePicked)
        {  }
        public Emotions(List<string> Unpicked, List<string> Picked)
            : base(Unpicked, Picked)
        { }
    }
}
