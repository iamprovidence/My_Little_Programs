namespace WriterTrainer.DataAccess.Entities
{
    class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}
