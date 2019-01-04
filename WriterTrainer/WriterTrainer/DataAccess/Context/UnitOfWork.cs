using WriterTrainer.DataAccess.Repositories;

namespace WriterTrainer.DataAccess.Context
{
    class UnitOfWork
    {
        // FIELDS
        AppContext context;
        static UnitOfWork instance;
        AnswerRepository answerRepository;
        ImageRepository imageRepository;
        KeyWordRepository keyWordRepository;
        MarkRepository markRepository;
        TaskRepository taskRepository;
        UserRepository userRepository;
        // CONSTRUCTORS
        public UnitOfWork()
        {
            context = new AppContext();

            answerRepository = null;
            imageRepository = null;
            keyWordRepository = null;
            markRepository = null;
            taskRepository = null;
            userRepository = null;
        }
        static UnitOfWork()
        {
            instance = new UnitOfWork();
        }
        ~UnitOfWork()
        {
            context.Dispose();
        }
        // PROPERTIES
        public static UnitOfWork Instance => instance;

        public AnswerRepository AnswerRepository
        {
            get
            {
                if (answerRepository == null)
                {
                    answerRepository = new AnswerRepository(context);
                }
                return answerRepository;
            }
        }
        public ImageRepository ImageRepository
        {
            get
            {
                if (imageRepository == null)
                {
                    imageRepository = new ImageRepository(context);
                }
                return imageRepository;
            }
        }
        public KeyWordRepository KeyWordRepository
        {
            get
            {
                if (keyWordRepository == null)
                {
                    keyWordRepository = new KeyWordRepository(context);
                }
                return keyWordRepository;
            }
        }
        public MarkRepository MarkRepository
        {
            get
            {
                if (markRepository == null)
                {
                    markRepository = new MarkRepository(context);
                }
                return markRepository;
            }
        }
        public TaskRepository TaskRepository
        {
            get
            {
                if (taskRepository == null)
                {
                    taskRepository = new TaskRepository(context);
                }
                return taskRepository;
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }
    }
}
