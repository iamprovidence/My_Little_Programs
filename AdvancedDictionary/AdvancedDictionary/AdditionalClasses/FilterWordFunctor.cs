using System.Collections.Generic;

namespace AdvancedDictionary.AdditionalClasses
{
    internal class FilterWordFunctor
    {
        List<string> synonyms;
        List<string> emotions;

        public FilterWordFunctor()
        {
            synonyms = new List<string>();
            emotions = new List<string>();
        }

        public void AddSynonym(string synonym)
        {
            synonyms.Add(synonym);
        }
        public void AddEmotion(string emotion)
        {
            emotions.Add(emotion);
        }
        public bool RemoveSynonym(string synonym)
        {
           return synonyms.Remove(synonym);
        }
        public bool RemoveEmotion(string emotion)
        {
            return emotions.Remove(emotion);
        }
        public void Clear()
        {
            synonyms.Clear();
            emotions.Clear();
        }
        public bool Predicate(Interfaces.IWord word)
        {
            // if no filter set word is coorect
            if (emotions.Count + synonyms.Count == 0) return true;
            // filter
            foreach(string e in emotions)
            {
                if (!word.Emotions.Picked.Contains(e)) return false;
            }
            foreach (string s in synonyms)
            {
                if (!word.Synonyms.Picked.Contains(s)) return false;
            }

            return true;
        }

    }
}
