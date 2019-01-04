using WriterTrainer.DataAccess.Context;

namespace WriterTrainer.DataAccess.Repositories
{
    class AnswerRepository : GenericRepository<Entities.Answer>
    {
        public AnswerRepository(AppContext context) 
            : base(context) {   }
    }
}
