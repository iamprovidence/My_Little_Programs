using AdvancedDictionary.Interfaces;

namespace AdvancedDictionary.Model
{
    [System.Serializable]
    internal class Word : Interfaces.IWord
    {
        // FIELDS
        string text;
        string description;
        Emotions emotions;
        Synonyms synonyms;

        // PROPERTIES
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
        public Emotions Emotions
        {
            get
            {
                return emotions;
            }

            set
            {
                emotions = value;
            }
        }
        public Synonyms Synonyms
        {
            get
            {
                return synonyms;
            }

            set
            {
                synonyms = value;
            }
        }
        
        // METHODS
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            return Equals(obj as IWord);
        }
        public bool Equals(IWord other) => this.text.Equals(other.Text);
        public override int GetHashCode() => base.GetHashCode();
    }
}
