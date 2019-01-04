using WriterTrainer.DataAccess.Context;

namespace WriterTrainer.DataAccess.Repositories
{
    class UserRepository : GenericRepository<Entities.User>
    {
        public UserRepository(AppContext context) 
            : base(context) {   }
    }
}
