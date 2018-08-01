using System.Collections.Generic;

namespace AdvancedDictionary.View
{
    class EmotionsView : Interfaces.IDescriptionWordView<string>
    {
        // FIELDS
        Model.Emotions data;
        // CONSTRUCTORS
        public EmotionsView(Model.Emotions emotions)
        {
            data = emotions;
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
