using System;

namespace Server.Domain
{
    public class PlatformAddedEvent : EventArgs
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
