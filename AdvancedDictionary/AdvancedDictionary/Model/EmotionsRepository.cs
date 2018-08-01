using System.IO;
using System.Collections.Generic;
using AdvancedDictionary.Interfaces;
using System.Runtime.Serialization.Formatters.Binary;

namespace AdvancedDictionary.Model
{
    [System.Serializable]
    class EmotionsRepository : List<string>, IRepositoryCollection<string>, IRepository<string, EmotionsRepository>
    {
        string filePath;

        public EmotionsRepository repository => this;


        // METHODS
        public new bool Add(string item)
        {
            if (!base.Contains(item))
            {
                base.Add(item);
                return true;
            }
            return false;
        }
        // CONSTRUCTORS
        private EmotionsRepository(string filePath) : base(10)
        {
            this.filePath = filePath;
        }
        // STATIC
        public static void Create(out EmotionsRepository repos, string path)
        {
            if(File.Exists(path))
            {
                EmotionsRepository.Load(out repos, path);
            }
            else
            {
                repos = new EmotionsRepository(path);
                repos.Save();
            }
        }
        public static void Load(out EmotionsRepository repos, string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream s = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                repos = (EmotionsRepository)bf.Deserialize(s);
            }
        }
        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream s = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(s, this);
            }
        }
    }
}
