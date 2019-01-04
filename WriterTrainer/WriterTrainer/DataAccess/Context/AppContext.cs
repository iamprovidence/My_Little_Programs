using System.Data.Entity;
using WriterTrainer.DataAccess.Entities;

namespace WriterTrainer.DataAccess.Context
{
    class AppContext : DbContext
    {
        DbSet<Answer> Answers { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<KeyWord> KeyWords { get; set; }
        DbSet<Mark> Marks { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<User> Users { get; set; }

    }
}
