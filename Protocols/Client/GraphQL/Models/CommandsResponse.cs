using System.Collections.Generic;
using Client.Contracts.ViewModels;

namespace Client.GraphQL.Models
{
    internal class CommandsResponse
    {
        public IReadOnlyCollection<CommandViewModel> Commands { get; set; }
    }
}
