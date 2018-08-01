namespace AdvancedDictionary.View
{
    class RepositoryView<T,C> : Interfaces.IRepositoryView<T, C> where C : Interfaces.IRepositoryCollection<T>
    {
        C repos;
        public C repository => repos;
        public string Name => "Emotions";

        public RepositoryView(C repos)
        {
            this.repos = repos;
        }
    }
}
