using WriterTrainer.DataAccess.Context;

namespace WriterTrainer.DataAccess.Repositories
{
    class TaskRepository : GenericRepository<Entities.Task>
    {
        public TaskRepository(AppContext context)
            : base(context)  {   }
    }
}
