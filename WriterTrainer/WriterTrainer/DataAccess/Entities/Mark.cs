namespace WriterTrainer.DataAccess.Entities
{
    class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public User Judge { get; set; }
    }
}
