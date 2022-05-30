using System;

namespace Client.Contracts.ViewModels
{
    public class PlatformAddedEvent : EventArgs
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
