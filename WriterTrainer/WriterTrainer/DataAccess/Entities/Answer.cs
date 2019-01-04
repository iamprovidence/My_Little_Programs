namespace WriterTrainer.DataAccess.Entities
{
    class Answer
    {
        public int Id { get; set; }
        public int AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public System.Collections.Generic.ICollection<Mark> Marks { get; set; }
    }
}
