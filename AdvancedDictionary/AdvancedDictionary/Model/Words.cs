using System;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace AdvancedDictionary.Model
{
    [Serializable]
    internal class Words: ObservableCollection<Word>
    {
        public new bool Add(Word item)
        {
            if(!base.Contains(item))
            {
                base.Add(item);
                return true;
            }
            return false;
        }
        public bool Contains(string text)
        {
            return this.Select(x => x.Text).Contains(text);
        }
        // STATIC
        public static void Load(out Words verbs, string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream s = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                verbs = (Words)bf.Deserialize(s);
            }
        }
        public static void Save(Words verbs, string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream s = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(s, verbs);
            }
        }
    }
}
