using WriterTrainer.DataAccess.Context;

namespace WriterTrainer.DataAccess.Repositories
{
    class MarkRepository : GenericRepository<Entities.Mark>
    {
        public MarkRepository(AppContext context)
            : base(context)  {   }
    }
}
