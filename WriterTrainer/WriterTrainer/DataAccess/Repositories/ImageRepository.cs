using WriterTrainer.DataAccess.Context;

namespace WriterTrainer.DataAccess.Repositories
{
    class ImageRepository : GenericRepository<Entities.Image>
    {
        public ImageRepository(AppContext context) 
            : base(context) {  }
    }
}
