using WriterTrainer.DataAccess.Context;

namespace WriterTrainer.DataAccess.Repositories
{
    class KeyWordRepository : GenericRepository<Entities.KeyWord>
    {
        public KeyWordRepository(AppContext context) 
            : base(context)  {  }
    }
}
