using System.Collections.Generic;

namespace WriterTrainer.DataAccess.Entities
{
    class Task
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int MinCharacters { get; set; }
        public int MaxCharacters { get; set; }
        public System.TimeSpan Time { get; set; }
        public int MinWord { get; set; }
        public int MaxWord { get; set; }
        public ICollection<KeyWord> KeyWords { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
