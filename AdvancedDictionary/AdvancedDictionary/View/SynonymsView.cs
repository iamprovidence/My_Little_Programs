using System.Collections.Generic;

namespace AdvancedDictionary.View
{
    class SynonymsView : Interfaces.IDescriptionWordView<string>
    {
        // FIELDS
        Model.Synonyms data;
        // CONSTRUCTORS
        public SynonymsView(Model.Synonyms synonyms)
        {
            data = synonyms;
        }

        // PROPERTIES
        public List<string> Picked => data.Picked;
        public List<string> Unpicked => data.Unpicked;

        public void Pick(int index)
        {
            data.Pick(index);
        }

        public void Unpick(int index)
        {
            data.Unpick(index);
        }
    }
}
