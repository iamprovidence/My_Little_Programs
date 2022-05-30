namespace Client.Contracts.ViewModels
{
    internal class CommandViewModel
    {
        public int Id { get; set; }

        public string Help { get; set; }

        public string CommandLine { get; set; }

        public int PlatformId { get; set; }
    }
}
